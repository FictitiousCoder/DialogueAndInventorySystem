using UnityEngine;
using System.Collections;

// Place onto game object to allow player to pick up the new profile
public class InventoryProfilePickUp : MonoBehaviour {

    public InvenctoryMenu inventoryScript;

    public string name;
    public int age;
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
                inventoryScript.AddNewProfile(new InventoryProfile(name, age, description, image));
                this.enabled = false;
            }
            else if (triggerType == TriggerType.Automatic)
            {
                inventoryScript.AddNewProfile(new InventoryProfile(name, age, description, image));
                this.enabled = false;
            }
        }
    }

    public void AddprofileToInventory()
    {
        inventoryScript.AddNewProfile(new InventoryProfile(name, age, description, image));
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
