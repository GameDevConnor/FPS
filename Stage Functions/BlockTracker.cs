using UnityEngine;

public class BlockTracker : CollectionTracker
{
    public override void SuccessAction()
    {
        Debug.Log("Yeah you got em all");
    }
}
