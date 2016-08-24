using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InvenctoryMenu : MonoBehaviour {

    public enum MenuType {ItemMenu, ProfileMenu};
    public GameObject[] menus; // Stores the UI objects containing each menu

    // Game Objects related to the item selected at the time
    public Image selectedItemImage;
    public Text selectedItemName;
    public Text selectedItemDescription;
    public Image[] itemImages;

    // Game Objects related to the profile selected at the time
    public Image selectedProfileImage;
    public Text selectedProfileName;
    public Text selectedProfileAge;
    public Text selectedProfileDescription;
    public Image[] profileImages;

    // Key-bindings
    public KeyCode toggleInventory;
    public KeyCode nextElement;
    public KeyCode previousElement;
    public KeyCode nextPage;
    public KeyCode previousPage;
    public KeyCode swapMenu;

    // Keeps track of current menu, element-index, and menu page
    private MenuType currentMenu;
    private int itemIndex;
    private int profileIndex;
    private int itemMenuIndex;
    private int profileMenuIndex;

    int elementsPerPage; // Make static? Currently same for all menus

    private Animator menuAnimator;
    private bool menuIsOpen = false;

    // Holds all elements currently in player's possession
    private List<InventoryItem> itemList = new List<InventoryItem>();
    private List<InventoryProfile> profileList = new List<InventoryProfile>();

    void Start()
    {
        int elementsPerPage = itemImages.Length;
        menuAnimator = gameObject.GetComponent<Animator>();
        currentMenu = MenuType.ItemMenu;
        itemIndex = 0;
        profileIndex = 0;
        profileMenuIndex = 1;
        itemMenuIndex = 1;
        UpdateItems();
        UpdateProfiles();
    }

    void Update () {

        if (Input.GetKeyDown(toggleInventory))
        {
            OpenCloseInvenctory();
        }
        else if (menuIsOpen)
        {
            if (currentMenu == MenuType.ItemMenu && itemList.Count > 0)
            {
                if (Input.GetKeyDown(nextElement))
                {
                    if (itemIndex < itemList.Count - 1)
                    {
                        if (itemIndex == ((itemMenuIndex * itemImages.Length) - 1))
                        {
                            NextPage();
                        }
                        else
                        {
                            SelectItem(itemIndex + 1);
                        }
                    }
                    else
                    {
                        if (itemMenuIndex > 1)
                        {
                            NextPage();
                        }
                        else
                        {
                            SelectItem(0);
                        }
                    }
                }
                else if (Input.GetKeyDown(previousElement))
                {
                    if (itemIndex > 0)
                    {
                        if (itemIndex == itemImages.Length * (itemMenuIndex - 1))
                        {
                            PreviousPage();
                        }
                        else
                        {
                            SelectItem(itemIndex - 1);
                        }
                    }
                    else
                    {
                        if (itemList.Count > itemImages.Length)
                        {
                            PreviousPage();
                        }
                        else
                        {
                            SelectItem(itemList.Count - 1);
                        }
                    }
                }
                else if (Input.GetKeyDown(swapMenu))
                {
                    SwapMenu(MenuType.ProfileMenu);
                }
                else if (Input.GetKeyDown(nextPage))
                {
                    NextPage();
                }
                else if (Input.GetKeyDown(previousPage))
                {
                    PreviousPage();
                }
            }
            else if (currentMenu == MenuType.ProfileMenu && profileList.Count > 0)
            {
                if (Input.GetKeyDown(nextElement))
                {
                    if (profileIndex < profileList.Count - 1)
                    {
                        if (profileIndex == ((profileMenuIndex * profileImages.Length) - 1))
                        {
                            NextPage();
                        }
                        else
                        {
                            SelectProfile(profileIndex + 1);
                        } 
                    }
                    else
                    {
                        if (profileMenuIndex > 1)
                        {
                            NextPage();
                        }
                        else
                        {
                            SelectProfile(0);
                        }
                    }
                }
                else if (Input.GetKeyDown(previousElement))
                {
                    if (profileIndex > 0)
                    {
                        if (profileIndex == profileImages.Length * (profileMenuIndex - 1))
                        {
                            PreviousPage();
                        }
                        else
                        {
                            SelectProfile(profileIndex - 1);
                        }
                    }
                    else
                    {
                        if (profileList.Count > profileImages.Length)
                        {
                            PreviousPage();
                        }
                        else
                        {
                            SelectProfile(profileList.Count - 1);
                        }
                    }
                }
                else if (Input.GetKeyDown(swapMenu))
                {
                    SwapMenu(MenuType.ItemMenu);
                }
                else if (Input.GetKeyDown(nextPage))
                {
                    NextPage();
                }
                else if (Input.GetKeyDown(previousPage))
                {
                    PreviousPage();
                }
            }
        }
    }

    public void AddNewItem(InventoryItem newItem)
    {
        itemList.Add(newItem);
        UpdateItems();

        if (itemList.Count == 1)
        {
            SelectItem(0);
        }
    }

    public void RemoveItem(InventoryItem removeItem)
    {
        if (removeItem == itemList[itemIndex])
        {
            SelectItem(itemIndex - 1);
        }

        itemList.Remove(removeItem);
        UpdateItems();
    }

    public void AddNewProfile(InventoryProfile newProfile)
    {
        profileList.Add(newProfile);
        UpdateProfiles();

        if (profileList.Count == 1)
        {
            SelectProfile(0);
        }
    }

    public void RemoveProfile(InventoryProfile removeProfile)
    {
        if (removeProfile == profileList[profileIndex])
        {
            SelectProfile(profileIndex - 1);
        }

        profileList.Remove(removeProfile);
        UpdateProfiles();
    }


    // Updates the images in the item menu
    void UpdateItems()
    {
        int pageOffset = (itemMenuIndex - 1) * itemImages.Length;
        int i = 0;

        while ((i + pageOffset) < itemList.Count)
        {
            if (i >= itemImages.Length)
            {
                return;
            }

            itemImages[i].sprite = itemList[i + pageOffset].GetImage();
            itemImages[i].color = new Color(1f, 1f, 1f, 1f);
            i++;
        }

        while (i < itemImages.Length)
        {
            itemImages[i].color = new Color(1f, 1f, 1f, 0f);
            i++;
        }
    }

    // Updates the images in the profile menu
    void UpdateProfiles()
    {
        Debug.Log("PageIndex:" + profileMenuIndex);

        int pageOffset = (profileMenuIndex - 1) * profileImages.Length;
        int i = 0;

        while ((i + pageOffset) < profileList.Count)
        {
            if (i >= profileImages.Length)
            {
                return;
            }

            profileImages[i].sprite = profileList[i + pageOffset].GetImage();
            profileImages[i].color = new Color(1f, 1f, 1f, 1f);
            i++;
        }

        while (i < profileImages.Length)
        {
            profileImages[i].color = new Color(1f, 1f, 1f, 0f);
            i++;
        }
    }

    void SelectItem(int index)
    {
        itemIndex = index;
        selectedItemImage.sprite = itemList[itemIndex].GetImage();
        selectedItemName.text = itemList[itemIndex].GetName();
        selectedItemDescription.text = itemList[itemIndex].GetDescription();
    }
    public void SelectItemOnPage(int index)
    {
        itemIndex = index + (itemImages.Length * (itemMenuIndex - 1));
        selectedItemImage.sprite = itemList[itemIndex].GetImage();
        selectedItemName.text = itemList[itemIndex].GetName();
        selectedItemDescription.text = itemList[itemIndex].GetDescription();
    }

    void SelectProfile(int index)
    {
        profileIndex = index;
        selectedProfileImage.sprite = profileList[profileIndex].GetImage();
        selectedProfileAge.text = "Age: " + profileList[profileIndex].GetAge().ToString();
        selectedProfileName.text = profileList[profileIndex].GetName();
        selectedProfileDescription.text = profileList[profileIndex].GetDescription();
    }

    public void SelectProfileOnPage(int index)
    {
        profileIndex = index + (profileImages.Length * (profileMenuIndex - 1));
        selectedProfileImage.sprite = profileList[profileIndex].GetImage();
        selectedProfileAge.text = "Age: " + profileList[profileIndex].GetAge().ToString();
        selectedProfileName.text = profileList[profileIndex].GetName();
        selectedProfileDescription.text = profileList[profileIndex].GetDescription();
    }

    public void OpenCloseInvenctory()
    {
        if (menuIsOpen == false)
        {
            menuAnimator.Play("InventoryOpen");
        }
        else
        {
            menuAnimator.Play("InventoryClose");
        }

        menuIsOpen = !menuIsOpen;
    }

    // Flips to the next page of the current menu, or skips to the first one if at the last page
    public void NextPage()
    {
        if (currentMenu == MenuType.ItemMenu)
        {
            if (itemList.Count > (itemImages.Length * itemMenuIndex))
            {
                itemMenuIndex++;
                SelectItem((itemMenuIndex - 1) * itemImages.Length);
                UpdateItems();
            }
            else if (itemList.Count > itemImages.Length)
            {
                itemMenuIndex = 1;
                SelectItem(0);
                UpdateItems();
            }
        }
        else if (currentMenu == MenuType.ProfileMenu)
        {
            if (profileList.Count > (profileImages.Length * profileMenuIndex))
            {
                profileMenuIndex++;
                SelectProfile((profileMenuIndex - 1) * profileImages.Length);
                UpdateProfiles();
            }
            else if (profileList.Count > profileImages.Length)
            {
                profileMenuIndex = 1;
                SelectProfile(0);
                UpdateProfiles();
            }
        }
    }

    // Flips to the previous page of the current menu, or skips to the final one if at the first page
    public void PreviousPage()
    {
        if (currentMenu == MenuType.ItemMenu)
        {
            if (itemList.Count > itemImages.Length)
            {
                if (itemMenuIndex > 1)
                {
                    itemMenuIndex--;
                    SelectItem((itemMenuIndex * itemImages.Length) - 1);
                }
                else
                {
                    itemMenuIndex = Mathf.CeilToInt((float)itemList.Count / (float)itemImages.Length);
                    SelectItem(itemList.Count - 1);
                }

                UpdateItems();
            }
        }
        else if (currentMenu == MenuType.ProfileMenu)
        {
            if (profileList.Count > profileImages.Length)
            {
                if (profileMenuIndex > 1)
                {
                    profileMenuIndex--;
                    SelectProfile((profileMenuIndex * profileImages.Length) - 1);
                }
                else
                {
                    profileMenuIndex = Mathf.CeilToInt((float)profileList.Count / (float)profileImages.Length);
                    SelectProfile(profileList.Count - 1);
                }
                
                UpdateProfiles(); 
            }
        }
    }

    public void SwapMenu(MenuType menu)
    {
        menus[(int)currentMenu].SetActive(false);
        menus[(int)menu].SetActive(true);
        currentMenu = menu;
    }


}
