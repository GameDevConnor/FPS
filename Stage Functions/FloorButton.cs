using UnityEngine;

public class FloorButton : MonoBehaviour
{

    public StageObject stageObject;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.GetComponent<Rigidbody>() != null)
    //    {
    //        stageObject.Function();
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>() != null)
        {
            stageObject.Function();
        }
    }
}
