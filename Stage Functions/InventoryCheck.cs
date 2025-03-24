using System.Collections.Generic;
using UnityEngine;

public class InventoryCheck : MonoBehaviour, Interactable
{

    public List<string> collectables;
    public string[] necessary;

    public StageObject stageObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool AllTrue()
    {
        foreach (string collectable in necessary)
        {
            if (!collectables.Contains(collectable))
            {
                return false;
            }
        }

        return true;
    }

    public void AddToList(string collectable)
    {
        collectables.Add(collectable);
    }

    public void RemoveFromList(string collectable)
    {
        collectables.Remove(collectable);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (AllTrue())
            {
                stageObject.Function();
            }
        }
    }

    public void Action()
    {
        if (AllTrue())
        {
            stageObject.Function();
        }
    }
}
