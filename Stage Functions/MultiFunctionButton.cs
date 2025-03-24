using System.Collections.Generic;
using UnityEngine;

public class MultiFunctionButton : MonoBehaviour, Interactable
{
    public List<StageObject> stageObjects;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Action()
    {
        foreach (StageObject stageObject in stageObjects)
        {
            stageObject.Function();
        }
    }
}
