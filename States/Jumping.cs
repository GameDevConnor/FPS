using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : State
{

    public float gravity = 10f;
    public Camera camera;
    public Vector3 move;
    public float speed = 10f;
    public float acceleration = 12f;
    public CharacterController controller;
    public float maxJumpHeight = 9f;
    public float yVel;

    public Jumping(StateMachine machine, StateFactory factory) : base(machine, factory)
    {
        this.machine = machine;
        this.factory = factory;
    }

    public override void EnterState()
    {
        controller = FindObjectOfType<CharacterController>();
        yVel = maxJumpHeight;

    }

    public override void UpdateState()
    {

        camera = FindObjectOfType<Camera>();
        controller = FindObjectOfType<CharacterController>();


        if (controller.velocity.y < 0 && controller.isGrounded == false)
        {
            SwitchState(factory.Falling());
        }
        //if (controller.isGrounded == true && controller.velocity.y < 0)
        //{
        //    SwitchState(factory.Walking());
        //}


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
