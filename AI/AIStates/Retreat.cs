using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retreat : AIState
{


    public GameObject player = GameObject.FindGameObjectWithTag("Player");


    public Retreat(AIStateMachine machine, AIStateFactory factory) : base(machine, factory)
    {

    }
    public override void EnterState()
    {
        Debug.Log("Retreat");
        machine.retreated = false;

    }

    public override void UpdateState()
    {

        if (!machine.retreated)
        {
            machine.aimove.objective = machine.aimove.hivemind.graph.returnShortestLengthYESOccupiedEnemy(machine.aimove.hivemind.graph.returnShortestLengthFromPosition(machine.transform)).transform;
            machine.aimove.setDestination();
        }

        
        if (!machine.aimove.enemy.pathPending)
        {
            if (machine.aimove.enemy.remainingDistance <= machine.aimove.enemy.stoppingDistance)
            {
                machine.aimove.objective = machine.aimove.hivemind.graph.returnShortestLengthFromPosition(machine.transform).transform;
                SwitchState(factory.Guard());
            }
        }

        if (machine.aimove.hivemind.lastManStanding)
        {
            SwitchState(factory.Pursuit());
        }



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
