using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryElement : MonoBehaviour {


    protected string name;
    protected string description;
    protected Sprite image;





    public string GetName()
    {
        return name;
    }
    public string GetDescription()
    {
        return description;
    }
    public Sprite GetImage()
    {
        return image;
    }

}
