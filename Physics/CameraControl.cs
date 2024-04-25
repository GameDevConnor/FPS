using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public float sensitivity = 100f;
    public Transform player;
    public float xRotation = 0f;
    public float range = 5f;
    public bool grabbing;
    public bool canPressE;

    public float carrySpeed;

    public Rigidbody rb;

    public float mouseY;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = null;
    }

    // Update is called once per frame
    void Update()
    {

        if (!PauseMenu.isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;

        }

        if (!PauseMenu.isPaused) {
            canPressE = true;
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            //float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
            mouseY = Input.GetAxis("Mouse Y") * sensitivity;


            player.Rotate(Vector3.up * mouseX);

            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -89f, 89f);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

            RaycastHit grab;
            bool grabTarget = Physics.Raycast(transform.position, transform.forward, out grab, range);
            //Debug.DrawRay(transform.position, transform.forward * range, Color.red, range);
            Ray ray = new Ray(transform.position, transform.forward);
            Vector3 endOfRay = ray.GetPoint(range);



            if (Input.GetKeyDown(KeyCode.E) && grabbing == false && canPressE)
            {

                if (grabTarget)
                {
                    Pickup pickUp = grab.transform.GetComponent<Pickup>();

                    if (pickUp != null)
                    {
                        grabbing = true;
                        rb = grab.transform.GetComponent<Rigidbody>();
                        rb.constraints = RigidbodyConstraints.FreezeRotation;
                        canPressE = false;
                    }
                }
            }



            if (Input.GetKeyDown(KeyCode.E) && grabbing && canPressE)
            {
                grabbing = false;
                rb.constraints = RigidbodyConstraints.None;
                rb = null;
            }


            if (rb != null)
            {
                rb.velocity = (endOfRay - rb.position) * (carrySpeed * Time.deltaTime);
            }



            if (Input.GetMouseButtonDown(0) && grabbing)
            {
                grabbing = false;
                rb.AddForce(transform.forward * CalculateForce.calculateForceHard(rb.mass));
                rb = null;

            }

            if (Input.GetMouseButtonDown(1) && grabbing)
            {
                grabbing = false;
                rb.AddForce(transform.forward * CalculateForce.calculateForceSoft(rb.mass));
                rb = null;

            }

        }
    }
}
