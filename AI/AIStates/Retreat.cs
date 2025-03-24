using UnityEngine;

public class Retreat : AIState
{


    //public GameObject player = GameObject.FindGameObjectWithTag("Player");
    public GameObject player;


    public Retreat(AIStateMachine machine, AIStateFactory factory) : base(machine, factory)
    {

    }
    public override void EnterState()
    {
        Debug.Log("Retreat");
        machine.aisensor.agent.speed *= 3f;
        machine.retreated = false;
        machine.completedRetreat = false;
        machine.aisensor.Hide(machine.player.transform);

    }

    public override void UpdateState()
    {


        player = machine.playerObject;

        if (machine.health <= 0)
        {
            //StopAllCoroutines();
            SwitchState(factory.AIDead());
        }

        if (machine.retreated == false)
        {
            //machine.aimove.objective = machine.aimove.hivemind.graph.returnShortestLengthYESOccupiedEnemy(machine.transform).transform;
            //machine.aimove.setDestination();
            //StartCoroutine(machine.aisensor.Hide(machine.transform));
            //machine.aisensor.Hide(machine.player.transform);
        }

        if (!machine.aimove.enemy.pathPending)
        {
            //if (Vector3.Distance(machine.transform.position, machine.aimove.guardPosition) <= machine.aimove.enemy.stoppingDistance)
            //{
            machine.retreated = true;
            //machine.aimove.objective = machine.aimove.hivemind.graph.returnShortestLengthFromPosition(machine.transform).transform;
            SwitchState(factory.Guard());
            //SwitchState(factory.Attacking());
            //}
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
