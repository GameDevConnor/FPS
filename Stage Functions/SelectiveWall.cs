using UnityEngine;

public class SelectiveWall : MonoBehaviour
{
    public LayerMask allowedLayers;
    public Collider wallCollider;
    public Collider physicalCollider;

    // Start is called before the first frame update
    void Start()
    {
        wallCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        bool allowed = (allowedLayers.value & (1 << other.gameObject.layer)) != 0;
        // Check if the object's layer is in the allowedLayers
        if (allowed)
        {
            Physics.IgnoreCollision(other, physicalCollider, true); // Allow object to pass through
        }
        else
        {
            Physics.IgnoreCollision(other, physicalCollider, false); // Block the object
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Reset collision ignore state when object exits the trigger
        //Physics.IgnoreCollision(other, physicalCollider, false);
    }
}
