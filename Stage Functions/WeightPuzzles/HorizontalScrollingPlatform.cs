using UnityEngine;

public class HorizontalScrollingPlatform : MonoBehaviour
{

    //public GameObject weightedEnd;
    //public GameObject nonWeightedEnd;
    public Transform rightEnd;
    public Transform leftEnd;
    public float threshold;
    public float speed;

    private Rigidbody tRb;

    public bool blocked;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Rigidbody weRb = weightedEnd.GetComponent<Rigidbody>();
        tRb = transform.GetComponent<Rigidbody>();

        if (blocked)
        {
            tRb.MovePosition(Vector3.MoveTowards(transform.position, transform.position, speed * Time.deltaTime));
        }
        else
        {

            if (tRb != null)
            {
                if (tRb.mass >= threshold)
                {
                    tRb.MovePosition(Vector3.MoveTowards(transform.position, leftEnd.position, speed * Time.deltaTime));
                }
                else
                {
                    tRb.MovePosition(Vector3.MoveTowards(transform.position, rightEnd.position, speed * Time.deltaTime));
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.transform.position.y > transform.position.y)
        //{
        blocked = true;
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.transform.position.y > transform.position.y)
        //{
        blocked = false;
        //}
    }
}
