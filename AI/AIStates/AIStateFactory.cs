using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateFactory
{
    AIStateMachine machine;
    public AIStateFactory(AIStateMachine machine)
    {
        this.machine = machine;
    }


    public AIState Guard()
    {
        return new Guard(machine, this);
    }

    public AIState Pursuit()
    {
        return new Pursuit(machine, this);
    }

    public AIState Attacking()
    {
        return new Attacking(machine, this);
    }

    public AIState Alert()
    {
        return new Alert(machine, this);
    }

    public AIState InRange()
    {
        return new InRange(machine, this);
    }

    public AIState AIDead()
    {
        return new AIDead(machine, this);
    }
}
