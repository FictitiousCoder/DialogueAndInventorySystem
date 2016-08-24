using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryItem : InventoryElement {

    public InventoryItem(string name, string description, Sprite image)
    {
        this.name = name;
        this.description = description;
        this.image = image;
    }

}
