using UnityEngine;

public class ShootingTarget : MonoBehaviour, Interactable
{

    public StageObject stageObject;

    public void Action()
    {
        if (stageObject != null)
        {
            stageObject.Function();
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
