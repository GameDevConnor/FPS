using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        weapon = inventory.guns[inventory.selection];
        Gun gun = weapon.GetComponent<Gun>();
        Thrower thrower = weapon.GetComponent<Thrower>();
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
