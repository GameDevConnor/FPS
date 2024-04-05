using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{

    public NavMeshAgent enemy;

    public Transform objective;

    public Vertex lastVisited;
    public Vertex nextDestination;

    [SerializeField]
    private bool inPosition;

    public Hivemind hivemind;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {

        if (!inPosition)
        {
            enemy.SetDestination(objective.transform.position);
        }
        else
        {
            Vertex vertexObjective = objective.GetComponent<Vertex>();

            if (vertexObjective != null)
            {
                lastVisited = vertexObjective;
                nextDestination = hivemind.graph.returnShortestLength(lastVisited);
                objective = nextDestination.transform;
            }

        }



    }

    public void setPosition(bool position)
    {
        this.inPosition = position;
    }


}
