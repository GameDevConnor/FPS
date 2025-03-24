using UnityEngine;

public class Walking : State
{
    public float gravity = 0f;
    public Camera camera;
    public Vector3 move;
    public float speed = 10f;
    public float acceleration = 12f;
    public CharacterController controller;
    public float yVel;



    public Walking(StateMachine machine, StateFactory factory) : base(machine, factory)
    {
        this.machine = machine;
        this.factory = factory;
    }

    public override void EnterState()
    {
        gravity = 10;
    }

    public override void UpdateState()
    {
        camera = FindObjectOfType<Camera>();
        controller = FindObjectOfType<CharacterController>();


        if (!controller.isGrounded && controller.velocity.y < 0)
        {
            SwitchState(factory.Falling());
        }

        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // Sometimes it's not leaving the walking state and not entering the jump state when I press space
            SwitchState(factory.Jumping());
        }


        move.y -= gravity;





        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        Vector2 input = new Vector2(horizontal, vertical);

        Vector3 camF = camera.transform.forward;
        Vector3 camR = camera.transform.right;

        camF.y = 0;
        camR.y = 0;

        camF = camF.normalized;
        camR = camR.normalized;


        Vector3 dir = camF * input.y + camR * input.x;


        Vector3 relativeVelocity = new Vector3(input.x, 0f, input.y);

        if (input.x > 0)
        {
            relativeVelocity.x = 1;
        }
        else if (input.x < 0)
        {
            relativeVelocity.x = -1;
        }
        else
        {
            relativeVelocity.x = 0;
        }

        if (input.y > 0)
        {
            relativeVelocity.z = 1;
        }
        else if (input.y < 0)
        {
            relativeVelocity.z = -1;
        }
        else
        {
            relativeVelocity.z = 0;
        }


        Vector2 dir2D = new Vector2(dir.x, dir.z);


        Vector3 relativeInputZ = (dir2D.normalized * relativeVelocity.z);
        Vector3 relativeInputZ3D = new Vector3(relativeInputZ.x * relativeVelocity.z, 0, relativeInputZ.y * relativeVelocity.z);

        Vector3 relativeInputX = (dir2D.normalized * relativeVelocity.x);
        Vector3 relativeInputX3D = new Vector3(relativeInputX.x * relativeVelocity.x, 0, relativeInputX.y * relativeVelocity.x);


        Vector3 relativeInput = relativeInputX3D + relativeInputZ3D;

        move = Vector3.Lerp(move, dir * speed, acceleration * Time.deltaTime);


        yVel -= gravity * Time.deltaTime;


        move.y = yVel;

        controller.Move(move * Time.deltaTime);


        if (player.health <= 0)
        {
            SwitchState(factory.Dead());
        }

    }


}
