using UnityEngine;

public class Button : MonoBehaviour, Interactable
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
}
