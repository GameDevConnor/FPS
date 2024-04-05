using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStateMachine : MonoBehaviour
{
    // Start is called before the first frame update


    public AIState currentState;

    public int health = 100;

    //public CharacterController controller;
    public Vector3 velocity;

    public bool isGrounded;

    public float Force;


    public Inventory inventory;

    public AIMove aimove;





    void Awake()
    {
        //controller = FindObjectOfType<CharacterController>();

        AIStateFactory factory = new AIStateFactory(this);

        currentState = factory.Guard();

        currentState.EnterState();

        aimove = GetComponent<AIMove>();

    }

    private void Update()
    {
        //isGrounded = controller.isGrounded;
        //velocity = controller.velocity;
        currentState.UpdateState();

    }




    public void SwitchState(AIState state)
    {
        currentState = state;
        currentState.EnterState();

    }


}
