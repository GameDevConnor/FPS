using UnityEngine;

public class StageCollectible : MonoBehaviour
{

    public string objectName;
    public InventoryCheck inventoryCheck;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventoryCheck.AddToList(this.objectName);

            Destroy(gameObject);
        }
    }
}
