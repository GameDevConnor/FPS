using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : AIState
{

    public GameObject player = GameObject.FindGameObjectWithTag("Player");

    public float range;
    //float spread = 0.08f;
    float spread = 0.0f;

    float shootingRange = 100f;
    public int damage = 1;

    public float rateOfFire = 15f;
    private float nextTimetoFire = 0f;

    public override void EnterState()
    {
        Debug.Log("Attacking");
        machine.retreated = true;
        machine.aimove.hivemind.moveIn(machine.transform);

    }

    public override void UpdateState()
    {
        if (!machine.aisensor.inFOV(player))
        {
            SwitchState(factory.Pursuit());
        }

        if (machine.health <= 0)
        {
            SwitchState(factory.AIDead());
        }


        //if ((machine.transform.position - player.transform.position).magnitude <= range)
        //{
        //    SwitchState(factory.InRange());
        //}


        if (machine.health <= (machine.maxHealth / 2) && !(machine.aimove.enemy.remainingDistance <= machine.aimove.enemy.stoppingDistance))
        {

            SwitchState(factory.Retreat());
        }

        //if (machine.aimove.hivemind.lastManStanding)
        //{
        //    SwitchState(factory.Pursuit());
        //}

        

        //machine.aimove.setDestination(player.transform);
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        float z = Random.Range(-spread, spread);

        Vector3 shootDirection = machine.transform.forward + new Vector3(x, y, z);

        RaycastHit hit;
        if (Time.time >= nextTimetoFire) {
            nextTimetoFire = Time.time + 1 / rateOfFire;
            if (Physics.Raycast(machine.transform.position, shootDirection, out hit, shootingRange))
        {
            StateMachine player = hit.transform.GetComponent<StateMachine>();
            if (player != null)
            {
                player.health -= damage;
            }
        }
        }

    }

    public Attacking(AIStateMachine machine, AIStateFactory factory) : base(machine, factory)
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
