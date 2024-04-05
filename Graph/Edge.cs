using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{

    Vertex vertex1;
    Vertex vertex2;
    float weight;

    public Edge(Vertex vertex1, Vertex vertex2)
    {
        this.vertex1 = vertex1;
        this.vertex2 = vertex2;
        this.weight = (vertex1.transform.position - vertex2.transform.position).magnitude;
    }

    public Edge(float weight)
    {
        this.weight = weight;
    }

    public Vertex getVertex1()
    {
        return vertex1;
    }
    public Vertex getVertex2()
    {
        return vertex2;
    }
    public float getWeight()
    {
        return weight;
    }

    public bool Equals(Edge edge1)
    {
        if (this.vertex1.Equals(edge1.getVertex1()) || this.vertex1.Equals(edge1.getVertex2()))
        {
            if (this.vertex2.Equals(edge1.getVertex1()) || this.vertex2.Equals(edge1.getVertex2()))
            {
                if (this.weight == edge1.getWeight())
                {
                    return true;
                }
            }

        }
        return false;
    }


    public string toString()
    {
        return "Edge: " + vertex1.ToString() + " to " + vertex2.ToString() + " weight = " + getWeight();
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
