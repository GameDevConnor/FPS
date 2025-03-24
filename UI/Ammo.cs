using TMPro;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    public Weapons weapon;
    public TextMeshProUGUI ammo;
    public Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.getLength() == 0)
        {
            weapon = null;
        }
        else
        {
            weapon = inventory.guns[inventory.selection];
        }

        //weapon = inventory.guns[inventory.selection];

        Gun gun;
        Thrower thrower;

        if (weapon != null)
        {
            gun = weapon.GetComponent<Gun>();
            thrower = weapon.GetComponent<Thrower>();
        }
        else
        {
            gun = null;
            thrower = null;
        }

        if (gun != null)
        {
            ammo.text = gun.magazine + "/" + gun.total;
        }
        else if (thrower != null)
        {
            ammo.text = "" + thrower.total;
        }
        else
        {
            ammo.text = "";
        }
    }
}
