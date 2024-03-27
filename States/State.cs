using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{

    public StateMachine machine;
    public StateFactory factory;

    public StateMachine player = FindObjectOfType<StateMachine>();


    public void Start()
    {

    }

    public void Update()
    {
        if (player.health <= 0)
        {
            SwitchState(factory.Dead());
        }
    }

    public State(StateMachine machine, StateFactory factory)
    {
        this.machine = machine;
        this.factory = factory;
    }
    public abstract void EnterState();

    public abstract void UpdateState();

    public void SwitchState(State newState)
    {
        machine.currentState = newState;
        newState.EnterState();
    }

}
