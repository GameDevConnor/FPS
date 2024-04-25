using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStateMachine : MonoBehaviour
{
    // Start is called before the first frame update


    public AIState currentState;

    public int maxHealth = 100;
    public int health;

    [HideInInspector]
    public AIMove aimove;
    [HideInInspector]
    public AISensor aisensor;


    public bool retreated;



    void Awake()
    {
        //health = maxHealth;
        //controller = FindObjectOfType<CharacterController>();

        AIStateFactory factory = new AIStateFactory(this);

        currentState = factory.Guard();

        currentState.EnterState();

        //aimove = GetComponent<AIMove>();
        //aisensor = GetComponent<AISensor>();

}

    private void Start()
    {
        aimove = GetComponent<AIMove>();
        aisensor = GetComponent<AISensor>();
    }

    private void Update()
    {
        if (!PauseMenu.isPaused)
        {
            //isGrounded = controller.isGrounded;
            //velocity = controller.velocity;
            currentState.UpdateState();
        }


    }




    public void SwitchState(AIState state)
    {
        currentState = state;
        currentState.EnterState();

    }


}
