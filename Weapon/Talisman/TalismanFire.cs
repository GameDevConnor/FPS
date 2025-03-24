using System.Collections.Generic;
using UnityEngine;

public class TalismanFire : MonoBehaviour
{
    public Camera camera;

    public float range;
    public float rateOfFire;
    private float nextTimetoFire = 0f;

    public GameObject weaponCamera;
    public Camera mainCamera;

    [HideInInspector]
    public CameraControl playerCam;

    public GameObject gravitySphere;
    public Collider[] gravitySphereObjects = new Collider[20];
    public List<Collider> gravitySphereList = new List<Collider>();
    public int gravitySphereObjectCount;
    public float gravitySphereObjectRange;
    public float gravitySphereObjectDuration;
    public LayerMask layerMask;

    public LayerMask raycastLayermasks;


    Vector3 hitPosition;

    public float flySpeed;

    public Stack<GameObject> talismanStack = new Stack<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused && !StateMachine.dead)
        {
            playerCam = camera.transform.GetComponent<CameraControl>();

            if (playerCam.grabbing)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }


            if (Input.GetKeyDown(KeyCode.F) && !playerCam.grabbing && Time.time >= nextTimetoFire)
            {
                nextTimetoFire = Time.time + 1 / rateOfFire;

                CreateSphere();

                System.Array.Sort(gravitySphereObjects, ColliderArrayComparer);

            }

            foreach (Collider collider in gravitySphereList)
            {
                Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
                rb.velocity = (hitPosition - rb.position) * (flySpeed * Time.deltaTime);

            }

        }

    }

    void CreateSphere()
    {

        System.Array.Clear(gravitySphereObjects, 0, gravitySphereObjects.Length);
        gravitySphereList.Clear();

        if (talismanStack.Count > 0)
        {
            Destroy(talismanStack.Pop());
        }

        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range, raycastLayermasks))
        {
            hitPosition = hit.point;
        }
        else
        {
            hitPosition = camera.transform.position + (camera.transform.forward * range);
        }

        gravitySphereObjectCount = Physics.OverlapSphereNonAlloc(hitPosition, gravitySphereObjectRange, gravitySphereObjects, layerMask, QueryTriggerInteraction.Collide);

        GameObject impact = Instantiate(gravitySphere, hitPosition, Quaternion.LookRotation(hitPosition));

        talismanStack.Push(impact);

        for (int i = 0; i < gravitySphereObjectCount; i++)
        {
            if (gravitySphereObjects[i] != null)
            {
                Rigidbody rb = gravitySphereObjects[i].gameObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    gravitySphereList.Add(gravitySphereObjects[i]);
                }
            }
        }

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
            return Vector3.Distance(hitPosition, A.transform.position).CompareTo(Vector3.Distance(hitPosition, B.transform.position));
        }

    }

}