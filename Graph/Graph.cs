using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    int size;
    List<Vertex> vertices;
    //Dictionary<Transform, List<Transform>> map;
    Dictionary<Vertex, List<Edge>> map;

    public Graph(int size, List<Vertex> vertices)
    {

        this.vertices = vertices;

        this.size = size;
        //map = new Dictionary<Transform, List<Transform>>();
        map = new Dictionary<Vertex, List<Edge>>();


        foreach (Vertex vertex in vertices)
        {
            this.AddVertex(vertex);
            //vertices.Remove(vertex);
            foreach (Vertex otherVertices in vertices)
            {
                if (new Edge(vertex, otherVertices).getWeight() != 0)
                {
                    this.AddEdge(vertex, otherVertices);
                }
        }
            //vertices.Add(vertex);

        }


    }

    void AddVertex(Vertex vertex)
    {
        //map.Add(vertex, new List<Transform>());
        if (!map.ContainsKey(vertex))
        {
            map.Add(vertex, new List<Edge>());
        }

    }
    void AddEdge(Vertex vertex1, Vertex vertex2)
    {
        Edge edge = new Edge(vertex1, vertex2);
        
        map[vertex1].Add(edge);
        //map[vertex2].Add(edge);
    }

    public string toString()
    {
        string mapString = "";

        foreach (Vertex vertex in map.Keys)
        {
            mapString += vertex.ToString() + ": ";
            foreach (Edge edge in map[vertex])
            {
                mapString += edge.toString() + ", ";
            }
            mapString += "\n";
        }

        return mapString;
    }

    public Vertex returnShortestLength(Vertex source)
    {
        Edge shortestEdge = new Edge(float.PositiveInfinity);
        foreach (Edge edge in map[source])
        {
            if (edge.getWeight() < shortestEdge.getWeight())
            {
                shortestEdge = edge;
            }
        }

        if (shortestEdge.getWeight() >= float.PositiveInfinity)
        {
            return source;
        }
        else
        {
            return shortestEdge.getVertex2();
        }

    }

    public Vertex returnShortestLengthNotOccupiedPlayer(Vertex source)
    {
        Edge shortestEdge = new Edge(float.PositiveInfinity);
        foreach (Edge edge in map[source])
        {
            if (edge.getWeight() < shortestEdge.getWeight() && !source.getOccupiedPlayer())
            {
                shortestEdge = edge;
            }
        }

        if (shortestEdge.getWeight() >= float.PositiveInfinity)
        {
            return source;
        }
        else
        {
            return shortestEdge.getVertex2();
        }
    }

    public Vertex returnShortestLengthNotOccupiedEnemy(Vertex source)
    {
        Edge shortestEdge = new Edge(float.PositiveInfinity);
        foreach (Edge edge in map[source])
        {
            if (edge.getWeight() < shortestEdge.getWeight() && !source.getOccupiedEnemy())
            {
                shortestEdge = edge;
            }
        }

        if (shortestEdge.getWeight() >= float.PositiveInfinity)
        {
            return source;
        }
        else
        {
            return shortestEdge.getVertex2();
        }
    }
}
