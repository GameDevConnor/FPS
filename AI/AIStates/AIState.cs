using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState : MonoBehaviour
{

    public AIStateMachine machine;
    public AIStateFactory factory;

    public AIStateMachine player = FindObjectOfType<AIStateMachine>();


    public void Start()
    {

    }

    public void Update()
    {
        if (player.health <= 0)
        {
            SwitchState(factory.AIDead());
        }
    }

    public AIState(AIStateMachine machine, AIStateFactory factory)
    {
        this.machine = machine;
        this.factory = factory;
    }
    public abstract void EnterState();

    public abstract void UpdateState();

    public void SwitchState(AIState newState)
    {
        Destroy(machine.currentState);
        machine.currentState = newState;
        newState.EnterState();
    }

}
