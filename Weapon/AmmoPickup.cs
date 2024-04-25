using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    // Start is called before the first frame update
    public string type;
    public int amount;
    public Inventory inventory;
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
            for (int i = 0; i < inventory.getLength(); i++)
            {
                if (type.Equals(inventory.guns[i].name))
                {
                    Gun gun = inventory.guns[i].GetComponent<Gun>();
                    if (gun != null)
                    {
                        gun.total += amount;
                        Destroy(gameObject);
                    }
                    
                }
            }
        }
    }
}
