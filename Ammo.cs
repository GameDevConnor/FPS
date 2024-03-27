using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ammo : MonoBehaviour
{

    public Gun gun;
    public TextMeshProUGUI ammo;
    public Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gun = inventory.guns[inventory.selection];
        ammo.text = gun.magazine + "/" + gun.total;
    }
}
