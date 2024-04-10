using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : AIState
{

    public GameObject player = GameObject.FindGameObjectWithTag("Player");


    public Guard(AIStateMachine machine, AIStateFactory factory) : base(machine, factory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Guard");
        machine.retreated = true;


    }

    public override void UpdateState()
    {


        if (machine.aisensor.inSphere(player) && !machine.aisensor.inFOV(player))
        {
            SwitchState(factory.Pursuit());
        }

        if (machine.aisensor.inHearingSphere(player) && !machine.aisensor.inFOV(player) && !machine.aisensor.inSphere(player))
        {
            SwitchState(factory.Alert());
        }

        if (machine.aisensor.inFOV(player))
        {
            SwitchState(factory.Attacking());
        }
        
        if (machine.health <= (machine.maxHealth / 2) && !(machine.aimove.enemy.remainingDistance <= machine.aimove.enemy.stoppingDistance))
        {
            SwitchState(factory.Retreat());
        }

        if (machine.aimove.hivemind.lastManStanding)
        {
            SwitchState(factory.Pursuit());
        }

        machine.aimove.setDestination();
        
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
