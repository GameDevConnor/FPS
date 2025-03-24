using TMPro;
using UnityEngine;

public class Talisman2Text : MonoBehaviour
{
    public TextMeshProUGUI massText;
    public GameObject talisman2;
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
            talisman2 = inventory.talismans[0];
        }

        Talisman2Fire talisman = talisman2.GetComponent<Talisman2Fire>();

        if (inventory.getLength() > 0 && inventory.talismans[0].ToString().Equals("Talisman2Weapon(Clone) (UnityEngine.GameObject)"))
        {
            massText.text = "Mass: " + talisman.newMass;
        }
        else
        {
            massText.text = "";
        }

    }
}
