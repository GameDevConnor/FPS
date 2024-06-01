using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : StageObject
{
    bool finished;
    bool white;
    // Start is called before the first frame update
    void Start()
    {
        finished = false;
        white = true;
    }

    //// Update is called once per frame
    void Update()
    {
        if (white)
        {
            transform.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            transform.GetComponent<MeshRenderer>().material.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        }

    }


    override
    public void Function()
    {
        white = !white;
        //transform.GetComponent<MeshRenderer>().material.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        finished = true;
    }
}
