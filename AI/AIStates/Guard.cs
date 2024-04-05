using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : AIState
{

    public AIMove aimove;



    public override void EnterState()
    {
        aimove = GetComponent<AIMove>();

    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    public Guard(AIStateMachine machine, AIStateFactory factory) : base(machine, factory)
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
