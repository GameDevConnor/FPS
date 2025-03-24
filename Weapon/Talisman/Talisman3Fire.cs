using UnityEngine;
using System.Collections.Generic;

public class Talisman3Fire : MonoBehaviour
{
    public Camera camera;

    public float range;
    public float rateOfFire;
    private float nextTimetoFire = 0f;

    public GameObject weaponCamera;
    public Camera mainCamera;

    [HideInInspector]
    public CameraControl playerCam;

    public bool up;

    public Stack<GameObject> talismanStack = new Stack<GameObject>();
    public LayerMask raycastLayerMasks;

    public float gravityForce;

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

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                up = !up;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                up = !up;
            }

            if (Input.GetKeyDown(KeyCode.F) && !playerCam.grabbing && Time.time >= nextTimetoFire)
            {
                nextTimetoFire = Time.time + 1 / rateOfFire;

                RaycastHit hit;

                if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range, raycastLayerMasks))
                {
                    FlyingPlatform fp = hit.transform.GetComponent<FlyingPlatform>();
                    if (fp != null)
                    {
                        if (up)
                        {
                            //cf.relativeForce = new Vector3(0f, gravityForce, 0f);
                            fp.up = true;
                        }
                        else
                        {
                            //cf.relativeForce = new Vector3(0f, -gravityForce, 0f);
                            fp.up = false;
                        }
                    }
                }
            }

        }
    }
}
