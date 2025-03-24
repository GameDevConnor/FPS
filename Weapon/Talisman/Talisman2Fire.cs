using System.Collections.Generic;
using UnityEngine;

public class Talisman2Fire : MonoBehaviour
{

    public Camera camera;

    public float range;
    public float rateOfFire;
    private float nextTimetoFire = 0f;

    public GameObject weaponCamera;
    public Camera mainCamera;

    [HideInInspector]
    public CameraControl playerCam;

    public Stack<GameObject> talismanStack = new Stack<GameObject>();
    public Stack<GameObject> talismanInstantiateStack = new Stack<GameObject>();

    public int newMass;
    public float originalMass;

    public GameObject massSphere;

    public LayerMask raycastLayermasks;


    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        newMass = Mathf.Clamp(newMass, 1, 100);

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

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                newMass--;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                newMass++;
            }


            if (Input.GetKeyDown(KeyCode.F) && !playerCam.grabbing && Time.time >= nextTimetoFire)
            {
                nextTimetoFire = Time.time + 1 / rateOfFire;

                if (talismanInstantiateStack.Count > 0)
                {
                    Destroy(talismanInstantiateStack.Pop());
                }

                if (talismanStack.Count > 0)
                {
                    talismanStack.Pop().GetComponent<Rigidbody>().mass = originalMass;
                }

                RaycastHit hit;
                if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range, raycastLayermasks))
                {
                    //Debug.Log(hit.transform.gameObject);
                    GameObject impact = Instantiate(massSphere, hit.point, Quaternion.LookRotation(hit.point));
                    talismanInstantiateStack.Push(impact);

                    Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        originalMass = rb.mass;
                        talismanStack.Push(rb.gameObject);
                        rb.mass = newMass;
                    }
                }

            }

        }
    }
}
