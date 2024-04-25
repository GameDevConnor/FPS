using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Prop : Pickup
    {

    public float thresholdVelocity = 15f;
    private Rigidbody rb;
    public float cooldownTime = 0.3f;
    public bool canHit = true;

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

    private void OnCollisionEnter(Collision hit)
    {
        StateMachine thrownPlayer = hit.collider.GetComponent<StateMachine>();
        if (thrownPlayer != null && canHit)
        {
            Debug.Log(hit.relativeVelocity.magnitude);

            thrownPlayer.health -= CalculateForce.calculateDamage(hit.relativeVelocity.magnitude, (int)rb.mass);

            StartCoroutine(cooldown());

            //if (Mathf.Abs(hit.relativeVelocity.magnitude) >= thresholdVelocity)
            //        {
            //            Debug.Log("YOWCH!!" + hit.gameObject.name);
            //        }
        }

        Target thrownTarget = hit.collider.GetComponent<Target>();
        if (thrownTarget != null && canHit)
        {
            Debug.Log(hit.relativeVelocity.magnitude);

            thrownTarget.health -= CalculateForce.calculateDamage(hit.relativeVelocity.magnitude, (int)rb.mass);

            StartCoroutine(cooldown());

            //if (Mathf.Abs(hit.relativeVelocity.magnitude) >= thresholdVelocity)
            //        {
            //            Debug.Log("YOWCH!!" + hit.gameObject.name);
            //        }
        }

    }

    private IEnumerator cooldown()
    {
        canHit = false;
        yield return new WaitForSeconds(cooldownTime);
        canHit = true;
    }
}
