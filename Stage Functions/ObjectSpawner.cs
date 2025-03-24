using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : StageObject
{

    public GameObject spawnObject;

    public bool replace;

    public Stack<GameObject> objectStack = new Stack<GameObject>();

    public GameObject original;

    public override void Function()
    {

        if (original != null)
        {
            objectStack.Push(original);
        }

        if (replace)
        {
            if (objectStack.Count > 0)
            {
                Destroy(objectStack.Pop());
            }

            objectStack.Push(Instantiate(spawnObject, transform.position, transform.rotation));
        }
        else
        {
            Instantiate(spawnObject, transform.position, transform.rotation);
        }
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
