using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hivemind : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Vertex> vertices;

    public List<AIStateMachine> squad;

    public bool lastManStanding;

    public Graph graph;
    void Start()
    {
        graph = new Graph(vertices.Count, vertices);
        //Debug.Log(graph.toString());

    }

    // Update is called once per frame
    void Update()
    {
        if (squadCount() > 1)
        {
            lastManStanding = false;
        }
        else
        {
            lastManStanding = true;
        }
    }

    public bool isOccupiedPlayer(Vertex vertex)
    {
        return vertex.getOccupiedPlayer();
    }

    public bool isOccupiedEnemy(Vertex vertex)
    {
        return vertex.getOccupiedEnemy();
    }

    public int squadCount()
    {
        int count = 0;
        foreach (AIStateMachine enemy in squad)
        {
            if (enemy.isActiveAndEnabled)
            {
                count++;
            }
        }
        return count;
    }
}
