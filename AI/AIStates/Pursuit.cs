using UnityEngine;

public class Pursuit : AIState
{

    //public GameObject player = GameObject.FindGameObjectWithTag("Player");

    public GameObject player;



    public override void EnterState()
    {
        Debug.Log("Pursuit");

    }

    public override void UpdateState()
    {

        player = machine.playerObject;


        if (machine.aisensor.inFOV(player))
        {
            SwitchState(factory.Attacking());
        }


        if (machine.health <= (machine.maxHealth / 2) && !(machine.aimove.enemy.remainingDistance <= machine.aimove.enemy.stoppingDistance) && machine.retreated == false)
        {

            SwitchState(factory.Retreat());
        }


        if (machine.health <= 0)
        {
            SwitchState(factory.AIDead());
        }


        machine.aisensor.chooseDestinationApproach();

    }

    public Pursuit(AIStateMachine machine, AIStateFactory factory) : base(machine, factory)
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
