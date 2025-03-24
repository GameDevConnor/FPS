using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[ExecuteInEditMode]
public class AISensor : MonoBehaviour
{

    public float distance = 30f;
    public float hearingDistance = 90f;
    public float angle = 80;
    public float height = 10.0f;
    public Color color = Color.red;
    public float hidingSpotCheck = 20f;


    public int scanFrequency;
    public LayerMask layers;
    public Collider[] colliders = new Collider[20]; // Store the results from the Physics operation */
    public int count; // Counting how many objects
    float scanInterval;
    float scanTimer;
    public List<GameObject> objects = new List<GameObject>(); // This will store objects within the sector rather than the whole sphere
    public LayerMask occlusionLayers; // Make sure that when something is behind a wall, it doesn't see the object. Which layers block the FOV

    Mesh mesh;

    public GameObject sensor;


    //public float lineCastOffset;

    //public bool inFov;

    public AIStateMachine machine;

    [HideInInspector]
    public Transform closestHidingSpot;

    public NavMeshAgent agent;


    public bool checkOutsideOnce;


    public Collider playerHitPoint;

    public LayerMask hidingSpotMask;

    private NavMeshHit hitDraw;
    private Transform targetDraw;
    private Vector3 vectorDraw;

    [Range(1, 10)]
    public float minimumHidingSpotDistance = 5f;

    public float minimumHeightForCover = 2f;

    private void OnEnable()
    {
        Destructable.destroyed += Scan;
        DisappearingWall.disappear += Scan;
    }

    private void OnDisable()
    {
        Destructable.destroyed -= Scan;
        DisappearingWall.disappear -= Scan;

    }


    // Start is called before the first frame update
    void Start()
    {
        scanInterval = 1.0f / scanFrequency;

        machine = GetComponent<AIStateMachine>();

        agent = GetComponent<NavMeshAgent>();

        checkOutsideOnce = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scanTimer -= Time.deltaTime; // Time.deltaTime - The interval in seconds from the last frame to the current one. Subtraction will happen each frame
        if (scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();
        }
    }

    private void Scan()
    {
        if (Vector3.Distance(transform.position, machine.player.transform.position) <= distance && checkOutsideOnce)
        {
            checkOutsideOnce = false;
        }

        if (Vector3.Distance(transform.position, machine.player.transform.position) <= distance || (Vector3.Distance(transform.position, machine.player.transform.position) > distance && !checkOutsideOnce))
        {

            if (Vector3.Distance(transform.position, machine.player.transform.position) > distance && !checkOutsideOnce)
            {
                checkOutsideOnce = true;
            }


            // Physics.OverlapSphere() returns an array with all the colliders touching or inside the sphere
            // Physics.OverlapSphereNonAlloc() returns the number of colliders stored in results buffer, a parameter in the method
            count = Physics.OverlapSphereNonAlloc(transform.position, distance, colliders, layers, QueryTriggerInteraction.Collide);

            objects.Clear();


            //machine.player.left = 0;
            //machine.player.right = 0;




            if (Physics.Linecast(sensor.transform.position, playerHitPoint.transform.position, occlusionLayers))
            {
                // We want to shoot a line from the enemy to the object it is looking at, if it is obscured by an object, if the Physics.Linecast returns true, then the we don't want to add the object to our list of viewable objects
                objects.Remove(machine.playerObject);
            }
            else
            {
                if (Vector3.Distance(transform.position, machine.player.transform.position) <= distance)
                {
                    objects.Add(machine.playerObject);
                }
                else
                {
                    objects.Remove(machine.playerObject);
                }
            }

            //for (int i = 0; i < count; i++) // How many of the objects in the sphere are actually in the line of sight
            //{
            //    if (colliders[i].ToString().Contains("CharacterController"))
            //    {
            //        // Positive (from forward -> counterclockwise) will go to left/item 1
            //        if (Vector3.SignedAngle(machine.player.transform.forward, machine.player.GetComponent<Collider>().ClosestPoint(transform.position) - transform.position, Vector3.up) >= 0)
            //        {
            //            machine.player.left++;
            //        }
            //        else
            //        {
            //            machine.player.right++;
            //        }



            //        GameObject obj = colliders[i].gameObject; // The gameObject of the collider that is in the colliders array
            //        if (isInSight(obj))
            //        {
            //            objects.Add(obj);
            //        }
            //        else
            //        {
            //            objects.Remove(obj);
            //        }
            //    }

            //}



        }

    }

    //public bool isInSight(GameObject obj)
    //{
    //    Vector3 origin = transform.position;
    //    Vector3 destination = obj.transform.position;
    //    Vector3 direction = destination - origin;


    //    if (Vector3.Distance(origin, destination) > distance)
    //    {
    //        return false;
    //    }



    //    if (direction.y < -height || direction.y > height) // Make sure it is between the slice height wise
    //    {
    //        return false;
    //    }

    //    direction.y = 0;
    //    float deltaAngle = Vector3.Angle(direction, transform.forward); // The angle between looking straight and the lateral direction of the object
    //    if (deltaAngle > angle)
    //    {
    //        return false; // If it is wider than the given angle then return false
    //    }

    //    origin.y = height / 2; // We wanna make sure the ray is cast from the center of the wedge
    //                           //destination.y = origin.y;

    //    Collider destinationCollider = obj.GetComponent<Collider>();

    //    if (Physics.Linecast(sensor.transform.position, playerHitPoint.transform.position, occlusionLayers))
    //    {
    //        // We want to shoot a line from the enemy to the object it is looking at, if it is obscured by an object, if the Physics.Linecast returns true, then the we don't want to add the object to our list of viewable objects
    //        return false;
    //    }


    //    return true;
    //}

    public bool inSphere(GameObject compare)
    {

        for (int i = 0; i < count; i++)
        {
            Collider collider = colliders[i];
            if (collider == null)
            {
                return false;
            }

            if (collider == compare)
            {
                return true;
            }
        }

        return false;
    }

    public bool inFOV(GameObject compare)
    {
        if (objects != null)
        {
            foreach (GameObject collider in objects)
            {

                if (collider == null)
                {
                    return false;
                }

                if (compare == null)
                {
                    return false;
                }


                if (collider == compare)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool inHearingSphere()
    {

        // What is happening is that when OverlapSphereNonAlloc() rescans, it doesn't clear the hearing colliders array, so it returns an integer amount, that integer being the number of objects it is scanning at that time. So when you do the i=0 for loop, it only goes up to what it is currently scanning. However, with the foreach loop, it's doing the whole array, but the player hasn't been overwritten from the array, so it's still seeing the player because it was a leftover from before. By replacing it with a regular for loop, we only go to what it sees in the moment.

        if ((machine.playerObject.transform.position - machine.transform.position).magnitude > hearingDistance)
        {
            return false;
        }
        else
        {
            return true;
        }

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

        for (int i = 0; i < segments; i++)
        {

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
        //// Implement OnDrawGizmos if you want to draw gizmos that are also pickable and always drawn
        //if (mesh)
        //{
        //    Gizmos.color = color;
        //    Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        //}

        Gizmos.DrawWireSphere(transform.position, distance); // Draw a sphere around the object

        //Gizmos.color = Color.white;
        //Gizmos.DrawWireSphere(transform.position, hearingDistance); // Draw a sphere around the object


        //Gizmos.color = color;
        //for (int i = 0; i < count; i++)
        //{
        //    Scan();
        //    Gizmos.DrawSphere(colliders[i].transform.position, 1.0f); // Draw a sphere around every object that is colliding with the sphere, as it is in the colliders array

        //    Vector3 origin = new Vector3(transform.position.x, height / 2, transform.position.z);
        //    Vector3 destination = colliders[i].transform.position;
        //    Vector3 direction = destination - origin;
        //    //destination -= direction.normalized * lineCastOffset;


        //    Gizmos.DrawLine(origin, destination);
        //}

        //Gizmos.color = Color.green;
        //foreach (GameObject obj in objects)
        //{
        //    Gizmos.DrawSphere(obj.transform.position, 1.0f);
        //}

        //Gizmos.DrawSphere(hitDraw.position, 1.0f);


        //if (Physics.Linecast(hitDraw.position, machine.playerObject.transform.position))
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawSphere(machine.playerObject.transform.position, 1.0f);
        //}
        //else
        //{
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawSphere(machine.playerObject.transform.position, 1.0f);
        //}

        //Collider targetCollider = machine.playerObject.GetComponent<Collider>();

        //if (Physics.Linecast(transform.position, targetCollider.ClosestPoint(transform.position), occlusionLayers))
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawSphere(targetCollider.ClosestPoint(transform.position), 1.0f);
        //}
        //else
        //{
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawSphere(targetCollider.ClosestPoint(transform.position), 1.0f);
        //}


        if (inFOV(machine.playerObject))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 1.0f);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 1.0f);
        }

        //Gizmos.DrawLine(machine.player.GetComponent<Collider>().ClosestPoint(transform.position), transform.position);
        //Gizmos.DrawLine(machine.player.transform.position, vectorDraw);


        //Debug.Log(Vector3.Angle(transform.position - machine.player.transform.position, vectorDraw - machine.player.transform.position));

        UnityEditor.SceneView.RepaintAll();
    }

    //public IEnumerator Hide(Transform target)
    public void Hide(Transform target)

    {
        targetDraw = target;

        Scan();

        System.Array.Sort(colliders, ColliderArrayComparer);

        //while (true)
        //{
        for (int i = 0; i < count; i++)
        {
            if (NavMesh.SamplePosition(colliders[i].transform.position, out NavMeshHit hit, hidingSpotCheck, agent.areaMask) && Vector3.Distance(target.position, colliders[i].transform.position) > minimumHidingSpotDistance && (colliders[i].GetComponent<MeshRenderer>() != null) && (colliders[i].bounds.size.y >= minimumHeightForCover) && (colliders[i].GetComponent<CharacterController>() == null))
            {
                if (!NavMesh.FindClosestEdge(hit.position, out hit, agent.areaMask))
                {
                    Debug.LogError("What the heck! No edges");
                }


                if (Physics.Linecast(hit.position, playerHitPoint.transform.position, hidingSpotMask))
                {
                    if (machine.retreated == false)
                    {

                        Debug.Log("Hiding Object: " + colliders[i]);
                        machine.aimove.guardPosition = hit.position;
                        agent.SetDestination(hit.position);
                        break;
                    }
                }
                else
                {
                    if (NavMesh.SamplePosition(colliders[i].transform.position - (target.position - hit.position).normalized * 20, out NavMeshHit hit2, 50f, agent.areaMask) && Vector3.Distance(target.position, colliders[i].transform.position) > minimumHidingSpotDistance && (colliders[i].GetComponent<MeshRenderer>() != null) && (colliders[i].bounds.size.y >= minimumHeightForCover) && (colliders[i].GetComponent<CharacterController>() == null))
                    {
                        if (!NavMesh.FindClosestEdge(hit2.position, out hit2, agent.areaMask))
                        {
                            Debug.LogError("SERIOUSLY! NO EDGES");
                        }

                        if (Physics.Linecast(hit2.position, playerHitPoint.transform.position, hidingSpotMask))
                        {

                            if (machine.retreated == false)
                            {
                                Debug.Log("Hiding Object: " + colliders[i]);

                                machine.aimove.guardPosition = hit2.position;
                                agent.SetDestination(hit2.position);
                                break;
                            }
                        }

                    }
                }
            }
            else
            {
                //Debug.LogError("I CAN'T EVEN FIND A NAVMESH!!!!");
            }
        }
        //yield return null;
        //}
    }

    public int ColliderArrayComparer(Collider A, Collider B)
    {
        if (A == null && B != null)
        {
            return 1;
        }
        else if (A != null && B == null)
        {
            return -1;
        }
        else if (A == null && B == null)
        {
            return 0;
        }
        else
        {
            return Vector3.Distance(agent.transform.position, A.transform.position).CompareTo(Vector3.Distance(agent.transform.position, B.transform.position));
        }

    }


    public int leftOrRightCompare()
    {

        // More left than right, return 1

        if (machine.player.left > machine.player.right)
        {
            return 1;
        }
        else if (machine.player.left < machine.player.right)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }


    public void chooseDestinationApproach()
    {

        Vector3 checkPosition;

        if (leftOrRightCompare() == 1)
        {
            // More left, so go in clockwise direction, < 0

            checkPosition = transform.position - machine.player.GetComponent<Collider>().ClosestPoint(transform.position);


            if (Vector3.SignedAngle(machine.player.transform.forward, machine.player.GetComponent<Collider>().ClosestPoint(transform.position) - transform.position, Vector3.up) < 0)
            {
                // GO TO OTHER SIDE
                Vector3 newPosition = Vector3.Cross(checkPosition, machine.player.transform.up);
                vectorDraw = machine.player.transform.position - newPosition;

                NavMeshHit hit;
                if (NavMesh.SamplePosition(vectorDraw, out hit, 10f, agent.areaMask) && machine.player.committed <= machine.player.nonCommitted)
                {
                    agent.SetDestination(hit.position);
                    machine.player.committed++;
                }
                else
                {
                    vectorDraw = transform.position;
                    agent.SetDestination(machine.player.transform.position);
                    machine.player.nonCommitted++;
                }
            }
            else 
            {
                agent.SetDestination(machine.player.transform.position);
                machine.player.nonCommitted++;
            }
        }
        else if (leftOrRightCompare() == -1)
        {
            if (Vector3.SignedAngle(machine.player.transform.forward, machine.player.GetComponent<Collider>().ClosestPoint(transform.position) - transform.position, Vector3.up) >= 0)
            {

                checkPosition = transform.position - machine.player.GetComponent<Collider>().ClosestPoint(transform.position);

                //GO TO OTHER SIDE
                Vector3 newPosition = Vector3.Cross(checkPosition, machine.player.transform.up);
                vectorDraw = machine.player.transform.position - newPosition;

                NavMeshHit hit;
                if (NavMesh.SamplePosition(vectorDraw, out hit, 10f, agent.areaMask) && machine.player.committed <= machine.player.nonCommitted)
                {
                    agent.SetDestination(hit.position);
                    machine.player.committed++;

                }
                else
                {
                    vectorDraw = transform.position;
                    agent.SetDestination(machine.player.transform.position);
                    machine.player.nonCommitted++;

                }
            }
            else
            {
                agent.SetDestination(machine.player.transform.position);
                machine.player.nonCommitted++;
            }

        }
        else
        {
            agent.SetDestination(machine.player.transform.position);
            machine.player.nonCommitted++;
        }

    }
}
