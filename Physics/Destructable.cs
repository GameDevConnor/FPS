using UnityEngine;

public class Destructable : Prop
{
    public BrokenPieces destroyedVersion;
    public float health;
    [HideInInspector]
    public CameraControl playerCam;
    public Camera camera;


    public delegate void ObjectDestroyed();
    public static event ObjectDestroyed destroyed;

    [HideInInspector]
    public Vector3 velocityAtCollision;

    private bool instantiated;
    public int pieces;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        rb = GetComponent<Rigidbody>();
        instantiated = false;
        destroyedVersion.totalPieces = pieces;

    }

    // Update is called once per frame
    void Update()
    {
        playerCam = camera.transform.GetComponent<CameraControl>();


        if (health <= 0)
        {
            if (destroyed != null)
            {
                destroyed();
            }

            if (!instantiated)
            {
                instantiated = true;

                //Debug.Log("Total Pieces: " + destroyedVersion.totalPieces);

                for (int i = 0; i < destroyedVersion.totalPieces; i++)
                {

                    BrokenPieces piece = Instantiate(destroyedVersion, transform.position, transform.rotation);
                    Rigidbody pieceRb = piece.GetComponent<Rigidbody>();
                    pieceRb.velocity = velocityAtCollision;

                }
            }

            Destroy(gameObject);


        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Object's Velocity: " + rb.velocity);
        velocityAtCollision = rb.velocity;

        if (collision.relativeVelocity.magnitude >= thresholdVelocity && !playerCam.grabbing)
        {
            health -= 5f;
        }

    }

    public void Hit(float damage)
    {
        health -= damage;
    }
}
