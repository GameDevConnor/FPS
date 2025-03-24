using UnityEngine;

public class TeleportBlock : MonoBehaviour
{
    public Transform teleportPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Teleport(GameObject player)
    {

        player.transform.position = teleportPosition.position;
    }

}
