using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    // Start is called before the first frame update
    public string type;
    public int amount;
    Inventory inventory;
    public Weapons newWeapon;
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Weapons weapon = null;

            for (int i = 0; i < inventory.getLength(); i++)
            {
                if (inventory.guns[i].name.Equals(type + "(Clone)"))
                {
                    Gun gun = inventory.guns[i].GetComponent<Gun>();
                    if (gun != null)
                    {
                        weapon = gun;
                        gun.total += amount;
                        Destroy(gameObject);
                    }

                    Thrower thrower = inventory.guns[i].GetComponent<Thrower>();
                    if (thrower != null)
                    {
                        weapon = thrower;
                        thrower.total += amount;
                        Destroy(gameObject);

                    }

                    Melee melee = inventory.guns[i].GetComponent<Melee>();
                    if (melee != null)
                    {
                        Destroy(gameObject);
                    }

                }

                
            }

            if (weapon == null)
            {
                int inventorySize = inventory.guns.Length;
                Weapons[] newInventory = new Weapons[inventorySize + 1];
                inventory.guns.CopyTo(newInventory, 0);
                inventory.guns = newInventory;


                Weapons newnewWeapon = Instantiate(newWeapon, inventory.transform);
                // Now that it instantiates new objects, instead of adding up to the prefab so that the total accumulates every time I play the scene, it is a copy, so it doesn't affect the prefab

                inventory.guns[inventory.guns.Length - 1] = newnewWeapon;

                Destroy(gameObject);


                if (newWeapon.name.Equals("Pistol"))
                {
                    
                    newWeapon.transform.position = new Vector3(2.79f, -0.76f, 2.55f);
                }
                if (newWeapon.name.Equals("SMG"))
                {
                    newWeapon.transform.position = new Vector3(2.79f, -0.85f, 2.56f);

                }
                if (newWeapon.name.Equals("Shotgun"))
                {
                    newWeapon.transform.position = new Vector3(2.79f, -0.88f, 5.06f);

                }
                if (newWeapon.name.Equals("Melee"))
                {
                    newWeapon.transform.position = new Vector3(4.5f, 0.17f, 3.99f);

                }



            }
        }
    }
}
