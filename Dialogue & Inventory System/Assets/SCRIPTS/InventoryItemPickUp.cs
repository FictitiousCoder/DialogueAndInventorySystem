using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Place onto game object to allow player to pick up the new item
public class InventoryItemPickUp : MonoBehaviour {

    public InvenctoryMenu inventoryScript;

    public string name;
    public string description;
    public Sprite image;

    public TriggerType triggerType;

    private bool inRange = false;

    void Update()
    {
        if (inRange == true)
        {
            if (triggerType == TriggerType.ButtonPress && Input.GetKeyDown(KeyCode.R))
            {
                AddItemToInventory();
                this.enabled = false;
            }
            else if (triggerType == TriggerType.Automatic)
            {
                AddItemToInventory();
                this.enabled = false;
            }
        }
    }

    public void AddItemToInventory ()
    {
        inventoryScript.AddNewItem(new InventoryItem(name, description, image));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }


}
