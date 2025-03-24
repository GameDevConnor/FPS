using UnityEngine;

public class Alert : AIState
{

    //public GameObject player = GameObject.FindGameObjectWithTag("Player");

    public GameObject player;


    public override void EnterState()
    {
        Debug.Log("Alert");
    }

    public override void UpdateState()
    {
        player = machine.playerObject;

        if (!machine.aisensor.inHearingSphere() && !machine.aisensor.inFOV(player))
        {
            SwitchState(factory.Guard());
        }

        if (machine.health <= 0)
        {
            SwitchState(factory.AIDead());
        }

        if (machine.aisensor.inSphere(player) && !machine.aisensor.inFOV(player))
        {
            SwitchState(factory.Pursuit());
        }


        if (machine.aisensor.inFOV(player))
        {
            SwitchState(factory.Attacking());
        }


        if (machine.health <= (machine.maxHealth / 2) && !(machine.aimove.enemy.remainingDistance <= machine.aimove.enemy.stoppingDistance) && machine.retreated == false)
        {

            SwitchState(factory.Retreat());
        }


        machine.aimove.setDestination(player.transform.position);

    }

    public Alert(AIStateMachine machine, AIStateFactory factory) : base(machine, factory)
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
