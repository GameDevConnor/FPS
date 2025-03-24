using UnityEngine;

public class Attacking : AIState
{

    public GameObject fpsPlayer;

    public float range;
    float spread = 0.08f;

    float shootingRange = 100f;
    public int damage = 1;

    public float rateOfFire = 3f;
    private float nextTimetoFire = 0f;


    LineRenderer laserLine;


    public override void EnterState()
    {
        Debug.Log("Attacking");
        machine.aimove.hivemind.moveIn(machine.transform);

        fpsPlayer = machine.playerObject;

        laserLine = machine.GetComponent<LineRenderer>();
    }

    public override void UpdateState()
    {
        if (machine.health <= 0)
        {
            SwitchState(factory.AIDead());
        }

        if (machine.retreated == false)
        {

            if (!machine.aisensor.inFOV(fpsPlayer))
            {
                SwitchState(factory.Pursuit());
            }

            if (machine.health <= 0)
            {
                SwitchState(factory.AIDead());
            }

            if (machine.health <= (machine.maxHealth / 2) && machine.retreated == false)
            {
                SwitchState(factory.Retreat());
            }

            machine.aimove.setDestination(machine.transform.position);

        }

        if (machine.completedRetreat)
        {
            if (!machine.aisensor.inFOV(fpsPlayer))
            {
                SwitchState(factory.Guard());
            }
        }

        //if (machine.magazine <= 0)
        //{

        //}
        //else
        //{
        //    Shoot();
        //}

        if (machine.magazine > 0)
        {
            Shoot();
        }

    }



    void Shoot()
    {
        //Debug.Log("Shooting");
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        float z = Random.Range(-spread, spread);

        Vector3 shootDirection = (fpsPlayer.transform.position - machine.transform.position).normalized + new Vector3(x, y, z);


        RaycastHit hit;
        if (Time.time >= nextTimetoFire)
        {

            laserLine.SetPosition(0, machine.transform.position);

            if (Physics.Raycast(machine.transform.position, shootDirection, out hit, shootingRange))
            {
                laserLine.SetPosition(1, hit.point);
                StateMachine player = hit.transform.GetComponent<StateMachine>();
                Destructable destructable = hit.transform.GetComponent<Destructable>();

                if (player != null)
                {
                    player.health -= damage;
                }

                if (destructable != null)
                {
                    destructable.health -= damage;
                }

            }
            else
            {
                laserLine.SetPosition(1, machine.transform.position + (shootingRange * shootDirection));
            }
            nextTimetoFire = Time.time + 1 / rateOfFire;
            machine.magazine--;

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
