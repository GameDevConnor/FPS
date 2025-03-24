using UnityEngine;

public class Thrower : Weapons
{

    public GameObject grenadePrefab;

    public float throwForce;

    public Camera camera;

    public float rateOfFire;
    private float nextTimetoFire = 0f;

    public int total;
    public bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        canShoot = false;
        canShoot = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused && !StateMachine.dead)
        {
            if (total <= 0)
            {
                canShoot = false;
            }

            CameraControl playerCam = camera.transform.GetComponent<CameraControl>();

            if (playerCam.grabbing)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }

            if (Input.GetButtonDown("Fire1") && !playerCam.grabbing && Time.time >= nextTimetoFire && canShoot)
            {
                nextTimetoFire = Time.time + 1 / rateOfFire;

                GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);

                Rigidbody rigidbody = grenade.GetComponent<Rigidbody>();

                rigidbody.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
                // ForceMode.VelocityChange adds instant velocity ignoring mass of the object, instead of applying a force over time

                total--;
            }

        }

    }
}
