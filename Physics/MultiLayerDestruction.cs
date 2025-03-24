using UnityEngine;

public class MultiLayerDestruction : Prop
{

    public BrokenPieces destroyedVersion;
    public float health;
    public float maxHealth;
    [HideInInspector]
    public CameraControl playerCam;
    public Camera camera;

    public delegate void ObjectDestroyed();
    public static event ObjectDestroyed destroyed;

    [HideInInspector]
    public Vector3 velocityAtCollision;

    private bool instantiated;

    public int layers;

    public int pieces;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        playerCam = camera.transform.GetComponent<CameraControl>();
        rb = GetComponent<Rigidbody>();
        instantiated = false;
        destroyedVersion.totalPieces = pieces;
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0 && layers <= 1)
        {
            if (destroyed != null)
            {
                destroyed();
            }

            if (!instantiated)
            {
                instantiated = true;

                for (int i = 0; i < destroyedVersion.totalPieces; i++)
                {

                    BrokenPieces piece = Instantiate(destroyedVersion, transform.position, transform.rotation);
                    Rigidbody pieceRb = piece.GetComponent<Rigidbody>();
                    pieceRb.velocity = velocityAtCollision;

                }
            }

            Destroy(gameObject);


        }
        else if (health <= 0 && layers > 1)
        {
            layers--;
            health = maxHealth;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
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
