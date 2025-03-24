using UnityEngine;

public class TalismanPickup : MonoBehaviour
{
    public string type;
    TalismanInventory inventory;
    public GameObject newTalisman;
    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<TalismanInventory>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            int inventorySize = inventory.talismans.Length;
            GameObject[] newInventory = new GameObject[inventorySize + 1];
            inventory.talismans.CopyTo(newInventory, 0);
            inventory.talismans = newInventory;

            GameObject newnewTalisman = Instantiate(newTalisman, inventory.transform);

            inventory.talismans[inventory.talismans.Length - 1] = newnewTalisman;

            Destroy(gameObject);

            newTalisman.transform.position = new Vector3(-1.19f, -0.76f, 2.78f);

        }
    }
}
