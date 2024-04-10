using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : AIState
{

    public GameObject player = GameObject.FindGameObjectWithTag("Player");

    public float range;


    public override void EnterState()
    {
        Debug.Log("Attacking");
        machine.retreated = true;


    }

    public override void UpdateState()
    {
        if (!machine.aisensor.inFOV(player))
        {
            SwitchState(factory.Pursuit());
        }


        if ((machine.transform.position - player.transform.position).magnitude <= range)
        {
            SwitchState(factory.InRange());
        }


        if (machine.health <= (machine.maxHealth / 2) && !(machine.aimove.enemy.remainingDistance <= machine.aimove.enemy.stoppingDistance))
        {

            SwitchState(factory.Retreat());
        }

        machine.aimove.setDestination(player.transform);

    }

    public Attacking(AIStateMachine machine, AIStateFactory factory) : base(machine, factory)
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
