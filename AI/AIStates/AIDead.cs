using UnityEngine;

public class AIDead : AIState
{
    public float gravity = 0f;
    public Camera camera;
    public Vector3 move;
    public float speed = 10f;
    public float acceleration = 12f;
    //public CharacterController controller;
    public float yVel;

    public bool dropped;



    public AIDead(AIStateMachine machine, AIStateFactory factory) : base(machine, factory)
    {
        this.machine = machine;
        this.factory = factory;
    }

    public override void EnterState()
    {
        gravity = 10;
        Debug.Log("yo that guy died");
        dropped = true;
    }

    public override void UpdateState()
    {

        if (dropped && machine.deathSpawn != null)
        {
            Instantiate(machine.deathSpawn, machine.transform.position, machine.transform.rotation);
            dropped = false;
        }

        machine.aisensor.agent.SetDestination(machine.transform.position);
    }
}
