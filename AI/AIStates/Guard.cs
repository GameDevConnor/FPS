using UnityEngine;

public class Guard : AIState
{

    //public GameObject player = GameObject.FindGameObjectWithTag("Player");
    public GameObject player;

    public Guard(AIStateMachine machine, AIStateFactory factory) : base(machine, factory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Guard");
    }

    public override void UpdateState()
    {
        if (machine.health <= 0)
        {
            SwitchState(factory.AIDead());
        }

        player = machine.playerObject;

        machine.aimove.setDestinationtoGuard();

        if (Vector3.Distance(machine.transform.position, machine.aimove.guardPosition) <= machine.aisensor.agent.stoppingDistance)
        {
            machine.completedRetreat = true;
        }

        if (machine.retreated == false)
        {
            if (machine.aisensor.inSphere(player) && !machine.aisensor.inFOV(player))
            {
                machine.completedRetreat = false;
                SwitchState(factory.Pursuit());
            }
        }

        if (machine.retreated == false)
        {
            if (machine.aisensor.inHearingSphere() && !machine.aisensor.inFOV(player) && !machine.aisensor.inSphere(player))
            {
                machine.completedRetreat = false;
                SwitchState(factory.Alert());
            }
        }

        if (machine.retreated == false)
        {
            if (machine.aisensor.inFOV(player))
            {
                machine.completedRetreat = false;
                SwitchState(factory.Attacking());
            }
        }

        if (machine.completedRetreat)
        {
            if (machine.aisensor.inFOV(player))
            {
                SwitchState(factory.Attacking());
            }
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
