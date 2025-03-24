using UnityEngine;

public class ObjectLauncher : StageObject
{

    public Rigidbody[] objects;
    public Transform target;
    public float force;

    public override void Function()
    {
        Launch();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Launch()
    {
        Vector3 launchVector;


        foreach (Rigidbody rb in objects)
        {
            launchVector = (target.position - rb.position);

            rb.AddForce(launchVector * force);
        }
    }
}
