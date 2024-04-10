using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRange : AIState
{

    public GameObject player = GameObject.FindGameObjectWithTag("Player");

    public float range;


    public override void EnterState()
    {

        Debug.Log("InRange");
    }

    public override void UpdateState()
    {
        if (machine.aisensor.inSphere(player) && !machine.aisensor.inFOV(player))
        {
            SwitchState(factory.Pursuit());
        }

        if (!machine.aisensor.inSphere(player) && !machine.aisensor.inFOV(player))
        {
            SwitchState(factory.Guard());
        }

        if ((transform.position - player.transform.position).magnitude >= range)
        {
            SwitchState(factory.Attacking());
        }


        if (machine.health <= (machine.maxHealth / 2))
        {

            SwitchState(factory.Retreat());
        }
    }

    public InRange(AIStateMachine machine, AIStateFactory factory) : base(machine, factory)
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
