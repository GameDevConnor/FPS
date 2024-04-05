using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : AIState
{
    public AIMove aimove;

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    public Pursuit(AIStateMachine machine, AIStateFactory factory) : base(machine, factory)
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        aimove = GetComponent<AIMove>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
