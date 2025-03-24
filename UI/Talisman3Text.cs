using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Talisman3Text : MonoBehaviour
{
    public TextMeshProUGUI upText;
    public GameObject talisman3;
    public TalismanInventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<TalismanInventory>();

    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.getLength() > 0)
        {
            talisman3 = inventory.talismans[0];
        }

        Talisman3Fire talisman = talisman3.GetComponent<Talisman3Fire>();

        if (inventory.getLength() > 0 && (inventory.talismans[0].ToString().Equals("Talisman3Weapon(Clone) (UnityEngine.GameObject)") || inventory.talismans[0].ToString().Equals("Talisman3Weapon (UnityEngine.GameObject)")))
        {
            upText.text = talisman.up ? "Up" : "Down";
        }
        else
        {
            upText.text = "";
        }
    }
}
