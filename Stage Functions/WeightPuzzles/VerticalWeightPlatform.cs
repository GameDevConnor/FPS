using UnityEngine;

public class VerticalWeightPlatform : MonoBehaviour
{
    public Transform upperEnd;
    public Transform lowerEnd;
    public float speed;

    public bool up;

    private float slowSpeed = 5f;
    private float fastSpeed = 10f;

    public float threshold;

    public bool blocked;

    public bool blockedWithBlock;

    Quaternion startingRotation;

    private void OnEnable()
    {
        PlatformBlock.Destroyed += TurnUp;
    }

    private void OnDisable()
    {
        PlatformBlock.Destroyed -= TurnUp;
    }

    // Start is called before the first frame update
    void Start()
    {
        startingRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Rigidbody rb = GetComponent<Rigidbody>();
        if (blocked)
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, transform.position, speed * Time.deltaTime));

            if (blockedWithBlock)
            {
                transform.rotation = Quaternion.Euler(30f, 0f, 0f);
            }
        }
        else
        {
            if (up)
            {
                speed = fastSpeed;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                //rb.MoveRotation(Vector3.RotateTowards(transform.rotation, new Vector3(0f, 0f, 0f)));
                rb.MovePosition(Vector3.MoveTowards(transform.position, upperEnd.position, speed * Time.deltaTime));
            }
            else
            {
                speed = slowSpeed;
                transform.rotation = Quaternion.Euler(10f, 0f, 0f);
                rb.MovePosition(Vector3.MoveTowards(transform.position, lowerEnd.position, speed * Time.deltaTime));
            }
        }

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && other.GetComponent<Rigidbody>().mass > threshold)
        {
            up = false;
        }
        else
        {
            up = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.position.y < transform.position.y)
        {
            blocked = true;
        }

        if (blocked)
        {
            blockedWithBlock = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        up = true;

        if (other.gameObject.transform.position.y < transform.position.y)
        {
            blocked = false;
        }
    }

    private void TurnUp()
    {
        up = true;
    }
}
