using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : StageObject
{
    public List<GameObject> objects;
    public List<Vector3> resetObjectsPosition;
    public GameObject original;

    public override void Function()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] == null)
            {
                Instantiate(original, transform.position, transform.rotation);
            }
            else
            {
                objects[i].transform.position = resetObjectsPosition[i];
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            resetObjectsPosition[i] = objects[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}