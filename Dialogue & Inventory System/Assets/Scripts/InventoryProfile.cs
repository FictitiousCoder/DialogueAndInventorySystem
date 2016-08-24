using UnityEngine;
using System.Collections;

public class InventoryProfile : InventoryElement {

    private int age;

    public InventoryProfile(string name, int age, string description, Sprite image)
    {
        this.name = name;
        this.age = age;
        this.description = description;
        this.image = image;
    }

    public int GetAge()
    {
        return age;
    }
}
