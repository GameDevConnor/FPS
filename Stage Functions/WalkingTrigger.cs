using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingTrigger : MonoBehaviour, Interactable
{
    public StageObject stageObject;
    public void Action()
    {
        stageObject.Function();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Action();
        }
    }
}
