using System.Collections.Generic;
using UnityEngine;

public class CollectionTracker : MonoBehaviour
{
    public List<GameObject> objects;
    public bool acted = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        objects.RemoveAll(item => item == null);

        if (objects.Count <= 0 && acted == false)
        {
            acted = true;
            SuccessAction();
        }
    }

    public virtual void SuccessAction()
    {
    }
}
