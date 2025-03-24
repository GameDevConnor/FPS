using System.Collections;
using UnityEngine;

public class Prop : Pickup
{

    public float thresholdVelocity = 15f;
    protected Rigidbody rb;
    public float cooldownTime = 0.3f;
    public bool canHit = true;
    public int baseDamage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    override
    public void Grab()
    {

    }


    public override void Throw()
    {

    }

    protected void OnCollisionEnter(Collision hit)
    {

        StateMachine thrownPlayer = hit.collider.GetComponent<StateMachine>();
        if (thrownPlayer != null && canHit && (hit.relativeVelocity.magnitude >= thresholdVelocity))
        {
            //Debug.Log(hit.relativeVelocity.magnitude);

            thrownPlayer.health -= (CalculateForce.calculateDamage(hit.relativeVelocity.magnitude, (int)rb.mass) + baseDamage);

            StartCoroutine(cooldown());

            //if (Mathf.Abs(hit.relativeVelocity.magnitude) >= thresholdVelocity)
            //        {
            //            Debug.Log("YOWCH!!" + hit.gameObject.name);
            //        }
        }

        Target thrownTarget = hit.collider.GetComponent<Target>();
        if (thrownTarget != null && canHit && (hit.relativeVelocity.magnitude >= thresholdVelocity))
        {
            //Debug.Log(hit.relativeVelocity.magnitude);

            thrownTarget.health -= (CalculateForce.calculateDamage(hit.relativeVelocity.magnitude, (int)rb.mass) + baseDamage);

            StartCoroutine(cooldown());

            //if (Mathf.Abs(hit.relativeVelocity.magnitude) >= thresholdVelocity)
            //        {
            //            Debug.Log("YOWCH!!" + hit.gameObject.name);
            //        }
        }

        AIStateMachine npc = hit.collider.GetComponent<AIStateMachine>();

        if (npc != null && canHit && (hit.relativeVelocity.magnitude >= thresholdVelocity) && (rb.velocity.magnitude > 0))
        {
            Debug.Log(hit.relativeVelocity.magnitude);

            //npc.health -= (CalculateForce.calculateDamage(hit.relativeVelocity.magnitude, (int)rb.mass) + baseDamage);
            npc.health -= baseDamage;

            StartCoroutine(cooldown());
        }

    }

    private IEnumerator cooldown()
    {
        canHit = false;
        yield return new WaitForSeconds(cooldownTime);
        canHit = true;
    }
}
