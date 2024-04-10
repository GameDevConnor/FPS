using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AISensor : MonoBehaviour
{

    public float distance = 10f;
    public float hearingDistance;
    public float angle = 30;
    public float height = 1.0f;
    public Color color = Color.red;


    public int scanFrequency;
    public LayerMask layers;
    public Collider[] colliders = new Collider[50]; // Store the results from the Physics operation */
    public Collider[] hearingColliders = new Collider[50];
    int count; // Counting how many objects
    int hearingCount;
    float scanInterval;
    float scanTimer;
    public List<GameObject> objects = new List<GameObject>(); // This will store objects within the sector rather than the whole sphere
    public LayerMask occlusionLayers; // Make sure that when something is behind a wall, it doesn't see the object. Which layers block the FOV

    Mesh mesh;


    public float lineCastOffset;


    public List<GameObject> hearingObjects = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        scanInterval = 1.0f / scanFrequency;

        //colliders = new Collider[50];

    }

    // Update is called once per frame
    void Update()
    {

        hearingDistance = distance * 3f;

        scanTimer -= Time.deltaTime; // Time.deltaTime - The interval in seconds from the last frame to the current one. Subtraction will happen each frame
        if (scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();
        }


       
    }

    private void Scan()
    {
        // Physics.OverlapSphere() returns an array with all the colliders touching or inside the sphere
        // Physics.OverlapSphereNonAlloc() returns the number of colliders stored in results buffer, a parameter in the method
        count = Physics.OverlapSphereNonAlloc(transform.position, distance, colliders, layers, QueryTriggerInteraction.Collide);

        hearingCount = Physics.OverlapSphereNonAlloc(transform.position, hearingDistance, hearingColliders, layers, QueryTriggerInteraction.Collide);


        objects.Clear();
        for (int i = 0; i < count; i++) // How many of the objects in the sphere are actually in the line of sight
        {
            GameObject obj = colliders[i].gameObject; // The gameObject of the collider that is in the colliders array
            if (isInSight(obj))
            {
                objects.Add(obj);
            }
            
        }


        hearingObjects.Clear();
        for (int i = 0; i < hearingCount; i++) // How many of the objects in the sphere are actually in the line of sight
        {
            GameObject obj = hearingColliders[i].gameObject; // The gameObject of the collider that is in the colliders array
            if (isInSight(obj))
            {
                hearingObjects.Add(obj);
            }

        }

    }

    public bool isInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 destination = obj.transform.position;
        Vector3 direction = destination - origin;

        if (direction.y < -height || direction.y > height) // Make sure it is between the slice height wise
        {
            return false;
        }

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward); // The angle between looking straight and the lateral direction of the object
        if (deltaAngle > angle)
        {
            return false; // If it is wider than the given angle then return false
        }

        origin.y = height / 2; // We wanna make sure the ray is cast from the center of the wedge
        //destination.y = origin.y;

        destination -= direction.normalized * lineCastOffset;

        if (Physics.Linecast(origin, destination, occlusionLayers))
        {
            // We want to shoot a line from the enemy to the object it is looking at, if it is obscured by an object, if the Physics.Linecast returns true, then the we don't want to add the object to our list of viewable objects
            return false;
        }


        return true;
    }


    public bool exists()
    {
        return true;
    }

    public bool inSphere(GameObject compare)
    {

        foreach (Collider collider in colliders)
        {
            if (collider == null)
            {
                return false;
            }

            if (collider.CompareTag(compare.tag))
            {
                return true;
            }
        }

        return false;
    }

    public bool inFOV(GameObject compare)
    {
        foreach (GameObject collider in objects)
        {
            if (compare == null)
            {
                return false;
            }

            if (collider.CompareTag(compare.tag))
            {
                return true;

            }
        }

        return false;
    }

    public bool inHearingSphere(GameObject compare)
    {

        foreach (Collider collider in hearingColliders)
        {
            if (collider == null)
            {
                return false;
            }

            if (collider.CompareTag(compare.tag))
            {
                return true;
            }
        }

        return false;
    }

    Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();

        // We're gonna split the mesh into segments where each segment is like a pizza slice
        // Subdivide a single wedge into multiple wedges to give a curved edge

        int segments = 10;
        int numberOfTriangles = (segments * 4) + 2 + 2; // Each segment has got 4 triangles, 2 for the far side and 1 for the top and bottom each. + 2 for each side of the wedge (+4 total)




        //int numberOfTriangles = 8; // 8 triangles will construct the entire wedge
        int numberOfVertices = numberOfTriangles * 3; // 3 vertices per triangle

        Vector3[] vertices = new Vector3[numberOfVertices]; // Allocate an array for our vertices

        int[] triangles = new int[numberOfVertices]; // An array for the number of triangles, which is equal to the number of vertices as he is ignoring index

        // To build the triangles, it's easiest to define the position of the 6 points
        Vector3 bottomCenter = Vector3.zero;


        // To get the bottom right and left of the triangle, we need to extend the point from the origin by our distance parameter. Then we can rotate it around the y axis by our angle parameter using Quaternions

        // Unity uses Quaternion class to store the 3D orientation of objects
        // Quaternion represents rotation
        // You can use quaternion to represent a series of rotations with a single rotation

        // Quaternion.Euler() - Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis; applied in that order.

        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance; // Rotating the vector by the quaternion and also increasing its scale by a scalar
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;


        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;

        int verticesIndex = 0;

        // left side of wedge will have 2 triangles

        // First Triangle
        vertices[verticesIndex++] = bottomCenter;
        vertices[verticesIndex++] = bottomLeft;
        vertices[verticesIndex++] = topLeft;
        // Second Triangle
        vertices[verticesIndex++] = topLeft;
        vertices[verticesIndex++] = topCenter;
        vertices[verticesIndex++] = bottomCenter;


        // right side of wedge will have 2 triangles
        // First Triangle. We need to go in the opposite direction
        vertices[verticesIndex++] = bottomCenter;
        vertices[verticesIndex++] = topCenter;
        vertices[verticesIndex++] = topRight;
        // Second Triangle
        vertices[verticesIndex++] = topRight;
        vertices[verticesIndex++] = bottomRight;
        vertices[verticesIndex++] = bottomCenter;



        float currentAngle = -angle; // Left side of the segment
        float deltaAngle = (angle * 2) / segments; // angle of the segment. Total angle of wedge / number of segments

        for(int i = 0; i < segments; i++) {

            // Bottom center and top center don't change so we don't put them in the loop

            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance; // Rotating the vector by the quaternion and also increasing its scale by a scalar
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;



            topLeft = bottomLeft + Vector3.up * height;
            topRight = bottomRight + Vector3.up * height;


            // far side of wedge will have 2 triangles
            vertices[verticesIndex++] = bottomLeft;
            vertices[verticesIndex++] = bottomRight;
            vertices[verticesIndex++] = topRight;

            // Second Triangle
            vertices[verticesIndex++] = topRight;
            vertices[verticesIndex++] = topLeft;
            vertices[verticesIndex++] = bottomLeft;


            // top side of wedge will have 1 triangle
            vertices[verticesIndex++] = topCenter;
            vertices[verticesIndex++] = topLeft;
            vertices[verticesIndex++] = topRight;

            // bottom side of wedge will have 1 triangle
            vertices[verticesIndex++] = bottomCenter;
            vertices[verticesIndex++] = bottomRight;
            vertices[verticesIndex++] = bottomLeft;
            // Opposite order ensures normals always point outwards

            currentAngle += deltaAngle;

        }




        // No vertex sharing will make this easier
        for (int i = 0; i < numberOfVertices; i++)
        {
            triangles[i] = i;
        }
        // Loop over number of vertices which is the same as the number of indices we have for the triangle

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    private void OnValidate()
    {
        // Unity calls when the script is loaded or a value changes in the inspector
        mesh = CreateMesh();
        scanInterval = 1.0f / scanFrequency;

    }

    private void OnDrawGizmos()
    {
        // Implement OnDrawGizmos if you want to draw gizmos that are also pickable and always drawn
        if (mesh)
        {
            Gizmos.color = color;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }

        Gizmos.DrawWireSphere(transform.position, distance); // Draw a sphere around the object

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, hearingDistance); // Draw a sphere around the object


        Gizmos.color = color;
        for (int i = 0; i < count; i++)
        {
            Gizmos.DrawSphere(colliders[i].transform.position, 1.0f); // Draw a sphere around every object that is colliding with the sphere, as it is in the colliders array

            Vector3 origin = new Vector3(transform.position.x, height / 2, transform.position.z);
            Vector3 destination = colliders[i].transform.position;
            Vector3 direction = destination - origin;
            destination -= direction.normalized * lineCastOffset;


            Gizmos.DrawLine(origin, destination);
        }

        Gizmos.color = Color.green;
        foreach (GameObject obj in objects)
        {
            Gizmos.DrawSphere(obj.transform.position, 1.0f);
        }

        UnityEditor.SceneView.RepaintAll();
    }
}
