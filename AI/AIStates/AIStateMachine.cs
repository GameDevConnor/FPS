using System.Collections;
using UnityEngine;

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


    public bool retreated = false;
    public bool completedRetreat = false;


    public int magazine;
    public float reloadTime;
    public int fullMagazine;


    public GameObject deathSpawn;

    public GameObject playerObject;
    public StateMachine player;

    public AIStateFactory factory;


    void Awake()
    {
        health = maxHealth;
        //controller = FindObjectOfType<CharacterController>();

        //AIStateFactory factory = new AIStateFactory(this);
        factory = new AIStateFactory(this);

        currentState = factory.Guard();

        currentState.EnterState();

        aimove = GetComponent<AIMove>();
        aisensor = GetComponent<AISensor>();

    }

    private void Start()
    {
        //aimove = GetComponent<AIMove>();
        //aisensor = GetComponent<AISensor>();

        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<StateMachine>();

    }

    private void Update()
    {
        if (!PauseMenu.isPaused)
        {
            //isGrounded = controller.isGrounded;
            //velocity = controller.velocity;

            //currentState.UpdateState();


            //if (magazine <= 0)
            //{
            //    StartCoroutine(reload());
            //}

            //StartCoroutine(aisensor.Hide(playerObject.transform));

        }

        UnityEditor.SceneView.RepaintAll();


    }

    private void FixedUpdate()
    {
        if (!PauseMenu.isPaused)
        {
            //isGrounded = controller.isGrounded;
            //velocity = controller.velocity;

            currentState.UpdateState();


            if (magazine <= 0)
            {
                StartCoroutine(reload());
            }
            //StartCoroutine(aisensor.Hide(playerObject.transform));
            //aisensor.chooseDestinationApproach();
        }

    }




    public void SwitchState(AIState state)
    {
        currentState = state;
        currentState.EnterState();

    }


    public IEnumerator reload()
    {


        // There is a bit of a transition in delay. By default it's 0.25 seconds
        yield return new WaitForSeconds(reloadTime - 0.25f);
        // There is a bit of a transition in delay. By default it's 0.25 seconds
        // This is a workaround
        yield return new WaitForSeconds(0.25f);

        magazine = fullMagazine;
    }

}
