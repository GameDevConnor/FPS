using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // Start is called before the first frame update


    public State currentState;

    public int health = 100;

    [HideInInspector]
    public CharacterController controller;
    public Vector3 velocity;

    public bool isGrounded;

    public float Force;


    public Inventory inventory;

    public static bool dead;


    private bool inputPaused = false;



    void Awake()
    {
        controller = FindObjectOfType<CharacterController>();

        StateFactory factory = new StateFactory(this);

        currentState = factory.Falling();

        currentState.EnterState();

    }

    void InputOn()
    {
        this.inputPaused = true;
    }

    void InputOff()
    {
        this.inputPaused = false;
    }

    void SwitchInput()
    {
        this.inputPaused = !inputPaused;
    }

    private void Update()
    {

        if (!PauseMenu.isPaused && !dead && !inputPaused)
        {



            isGrounded = controller.isGrounded;
            velocity = controller.velocity;
            currentState.UpdateState();

        }


        if (health <= 0)
        {
            dead = true;
        }
        else
        {
            dead = false;
        }
    }




    public void SwitchState(State state)
    {
        currentState = state;
        currentState.EnterState();

    }


}
