using UnityEngine;

public class ConditionalScale : MonoBehaviour
{
    public HingeJoint hinge;

    public bool tooHeavy;

    public float threshold;

    public bool blockOnIt;
    JointLimits limits;

    public float limitMax;
    public float limitMin;

    public GameObject block;
    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        limits = hinge.limits;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (blockOnIt)
        {
            if (block.GetComponent<Rigidbody>() != null)
            {
                if (block.GetComponent<Rigidbody>().mass > threshold)
                {
                    tooHeavy = true;
                }
            }
        }

        if (blockOnIt && tooHeavy)
        {
            limits.min = limitMin;
            limits.max = limitMax;
            hinge.limits = limits;
        }
        else
        {
            limits.min = 0f;
            limits.max = 0f;
            hinge.limits = limits;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            blockOnIt = true;
            block = other.gameObject;
        }
        else
        {
            blockOnIt = false;
            block = null;
        }

        if (other.GetComponent<Rigidbody>() != null && other.GetComponent<Rigidbody>().mass > threshold)
        {
            tooHeavy = true;
        }
        else
        {
            tooHeavy = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        blockOnIt = false;
        tooHeavy = false;
        block = null;
    }
}
