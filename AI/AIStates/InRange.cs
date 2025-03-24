using UnityEngine;

public class InRange : AIState
{

    public GameObject player = GameObject.FindGameObjectWithTag("Player");

    public float range;


    public override void EnterState()
    {

        Debug.Log("InRange");
    }

    public override void UpdateState()
    {
        if (machine.aisensor.inSphere(player) && !machine.aisensor.inFOV(player))
        {
            SwitchState(factory.Pursuit());
        }

        if (!machine.aisensor.inSphere(player) && !machine.aisensor.inFOV(player))
        {
            SwitchState(factory.Guard());
        }

        if ((transform.position - player.transform.position).magnitude >= range)
        {
            SwitchState(factory.Attacking());
        }


        if (machine.health <= (machine.maxHealth / 2) && !(machine.aimove.enemy.remainingDistance <= machine.aimove.enemy.stoppingDistance) && machine.retreated == false)
        {

            SwitchState(factory.Retreat());
        }

        //if (machine.aimove.hivemind.lastManStanding)
        //{
        //    SwitchState(factory.Pursuit());
        //}

        if (machine.health <= 0)
        {
            SwitchState(factory.AIDead());
        }
    }

    public InRange(AIStateMachine machine, AIStateFactory factory) : base(machine, factory)
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
