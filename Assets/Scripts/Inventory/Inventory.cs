using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Variables
    public static List<Item> inv = new List<Item>(); // List of items
    public static bool showInv; // Show or hide inventory
    public Item selectedItem; // The item we are interacting with
    public static int money; // How much moula we have

    public Vector2 scr = Vector2.zero; // 16:9
    public Vector2 scrollPos = Vector2.zero; // Scroll bar position 

    public string sortType = "All";

    public Transform dropLocation;
    public Transform[] equippedLocation;
    public GameObject curWeapon;
    public GameObject curHelm;

    // 0 = Right Hand // Weapon
    // 1 = Head // Helmet

    #endregion
    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inv.Add(ItemData.CreateItem(0)); // And 'inv.Remove...' is to remove the item
        inv.Add(ItemData.CreateItem(2)); 
        inv.Add(ItemData.CreateItem(102)); 
        inv.Add(ItemData.CreateItem(201)); 
        inv.Add(ItemData.CreateItem(202)); 
        inv.Add(ItemData.CreateItem(302)); 
       // inv.Add(ItemData.CreateItem(404)); // This will default to the Apple since case 404 doesn't exist and apple is the default

        for (int i = 0; i < inv.Count; i++)
        {
            Debug.Log(inv[i].Name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!PauseMenu.isPaused)
            {
                ToggleInv();
            }          
        }
       /* if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (showInv)
            {
                inv.Add(ItemData.CreateItem(0)); 
                inv.Add(ItemData.CreateItem(2));
                inv.Add(ItemData.CreateItem(102));
                inv.Add(ItemData.CreateItem(201));
                inv.Add(ItemData.CreateItem(202));
                inv.Add(ItemData.CreateItem(302));
                inv.Add(ItemData.CreateItem(404));
            }          
        }*/
    }
    public bool ToggleInv()
    {
        if (showInv)
        {
            showInv = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            return (false);
        }
        else
        {
            showInv = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return (true);
        }
    }
    private void OnGUI()
    {
        if (!PauseMenu.isPaused) // Only display if not paused
        {
            if (showInv) // And it toggled on
            {
                if(scr.x != Screen.width/16 || scr.y != Screen.height / 9) // Update screen when needed
                {
                    scr.x = Screen.width / 16;
                    scr.y = Screen.height / 9;
                }
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Inventory");
                if(GUI.Button(new Rect(5.5f*scr.x, 0.25f*scr.y, scr.x, 0.25f*scr.y), "All"))
                {
                    sortType = "All";
                }
                if (GUI.Button(new Rect(6.5f*scr.x, 0.25f*scr.y, scr.x, 0.25f*scr.y), "Consumables"))
                {
                    sortType = "Consumables";
                }
                if (GUI.Button(new Rect(7.5f*scr.x, 0.25f*scr.y, scr.x, 0.25f*scr.y), "Craftables"))
                {
                    sortType = "Craftable";
                }
                if (GUI.Button(new Rect(8.5f*scr.x, 0.25f*scr.y, scr.x, 0.25f*scr.y), "Weapons"))
                {
                    sortType = "Weapon";
                }
                if (GUI.Button(new Rect(9.5f*scr.x, 0.25f*scr.y, scr.x, 0.25f*scr.y), "Armour"))
                {
                    sortType = "Armour";
                }
                if (GUI.Button(new Rect(10.5f*scr.x, 0.25f*scr.y, scr.x, 0.25f*scr.y), "Misc"))
                {
                    sortType = "Misc";
                }
                if (GUI.Button(new Rect(11.5f*scr.x, 0.25f*scr.y, scr.x, 0.25f*scr.y), "Quest"))
                {
                    sortType = "Quest";
                }
                // Function goes here
                DisplayInv(sortType);

                if(selectedItem != null) // so if we have selected an item
                {
                    // show the icon
                    GUI.DrawTexture(new Rect(11 * scr.x, 1.5f*scr.y, 2*scr.x, 2*scr.y), selectedItem.Icon);
                    // Discarding Items
                    if (selectedItem.Type != ItemTypes.Quest) // If the selected item is not a quest item (cause most games don't allow you to discard quest items)...
                    {
                        if (GUI.Button(new Rect(14 * scr.x, 8.75f * scr.y, scr.x, 0.25f * scr.y), "Discard"))
                        {
                            if(curWeapon != null & selectedItem.MeshName == curWeapon.name)
                            {
                                Destroy(curWeapon);
                            }
                            else if(curHelm != null && selectedItem.MeshName == curHelm.name)
                            {
                                Destroy(curHelm);
                            }

                            // spawn item on ground
                            GameObject clone = Instantiate(Resources.Load("Prefab/" + selectedItem.MeshName) as GameObject, dropLocation.position, Quaternion.identity);
                            clone.AddComponent<Rigidbody>().useGravity = true;

                            if (selectedItem.Amount > 1)
                            {
                                selectedItem.Amount--;
                            }
                            else
                            {
                                inv.Remove(selectedItem);
                                selectedItem = null;
                            }
                            return; // ALWAYS RETURN AFTER NULLIFYING SOMETHING
                        }
                    }
                    // Using items
                    switch (selectedItem.Type)
                    {
                        case ItemTypes.Consumables:

                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nHeal: " + selectedItem.Heal + "\nAmount: " + selectedItem.Amount);
                            if (CharacterHandler.curHealth < CharacterHandler.maxHealth)
                            {
                                if (GUI.Button(new Rect(15 * scr.x, 8.75f * scr.y, scr.x, 0.25f * scr.y), "Eat"))
                                {
                                    CharacterHandler.curHealth += selectedItem.Heal;
                                    if (selectedItem.Amount > 1)
                                    {
                                        selectedItem.Amount--;
                                    }
                                    else
                                    {
                                        inv.Remove(selectedItem);
                                        selectedItem = null;
                                    }
                                    return; // ALWAYS RETURN AFTER NULLIFYING SOMETHING
                                }                                                                                              
                            }                                                                                                               
                            break;
                        case ItemTypes.Craftable:
                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value);
                            if (GUI.Button(new Rect(15 * scr.x, 8.75f * scr.y, scr.x, 0.25f * scr.y), "Use"))
                            {
                                // craft system a + b = c
                            }
                            break;
                        case ItemTypes.Armour:
                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nArmour: " + selectedItem.Armour);
                            if (GUI.Button(new Rect(15 * scr.x, 8.75f * scr.y, scr.x, 0.25f * scr.y), "Wear"))
                            {
                                // attach and wear on character
                            }
                            break;
                        case ItemTypes.Weapon:
                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nDamage: " + selectedItem.Damage);
                            if(curWeapon == null || selectedItem.MeshName != curWeapon.name)
                                // selectedItem.Id != curWeapon.GetComponent<ItemHandler>().itemId
                            {
                                if (GUI.Button(new Rect(15 * scr.x, 8.75f * scr.y, scr.x, 0.25f * scr.y), "Equip"))
                                {
                                    // use and spawn to character 
                                    if(curWeapon != null)
                                    {
                                        Destroy(curWeapon);
                                    }
                                    curWeapon = Instantiate(Resources.Load("Prefab/" + selectedItem.MeshName) as GameObject, equippedLocation[0]);
                                 
                                    curWeapon.GetComponent<ItemHandler>().enabled = false;
                                    curWeapon.name = selectedItem.MeshName;
                                }
                            }                           
                            break;
                        case ItemTypes.Misc:
                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value);
                            break;
                        case ItemTypes.Quest:
                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value);
                            break;
                    }
                }
            }
        }
    }
    void DisplayInv(string sortType)
    {
        if (!(sortType == "All" || sortType == "")) // if we're not all of our items or no items, then we are an item of a category
        {
            // Use sortType string to connect to our enum type
            ItemTypes type = (ItemTypes)System.Enum.Parse(typeof(ItemTypes), sortType); // Converting our button into an enum using a string
            int a = 0; // Amount of that type
            int s = 0; // Slot position of UI item
            for (int i = 0; i < inv.Count; i++) // Loops through all items
            {
                if (inv[i].Type == type) // If it is of type...
                {
                    a++; // increase the amount of this type
                }
            }
            if (a <= 35) // if the amount of this type is less than 35...
            {
                for (int i = 0; i < inv.Count; i++) // we filter through all items
                {
                    if (inv[i].Type == type) // If it is of the type...
                    {
                        // we display a button that is of this type and slot it under the last using s as our height
                        if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + s * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                        {
                            selectedItem = inv[i]; // This button is our selected item from our inventory
                            Debug.Log(selectedItem.Name); // Tell us its name (in the console)
                        }
                        s++; // once added, increase our s
                        // each new thing goes under the last
                    }
                }
            }
            else // Otherwise, if we have more than 35 of this type...
            {
                // we need a scroll view
                // remove the previous 35 from our type amount a
                scrollPos = GUI.BeginScrollView(new Rect(0, 0.25f * scr.y, 3.75f * scr.x, 8.75f * scr.y), scrollPos, new Rect(0, 0, 0, 8.75f * scr.y + ((a - 35) * (0.25f * scr.y))), false, true);
                #region Items in Viewing Area

                for (int i = 0; i < inv.Count; i++) // Loops through all items
                {
                    if (inv[i].Type == type) // If it is of type...
                    {
                        // we display a button that is of this type and slot it under the last using s as our height
                        if (GUI.Button(new Rect(0.5f * scr.x, 0 * scr.y + s * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                        {
                            selectedItem = inv[i]; // This button is our selected item from our inventory
                            Debug.Log(selectedItem.Name); // Tell is its name (in the console)
                        }
                        s++; // Once added, increase our s
                        // Each new thing goes under the last
                    }
                }
                #endregion
                GUI.EndScrollView(); // End of elements inside view
            }
        }
        else
        {
            #region Non-scroll Inventory
            if (inv.Count <= 35) // If we 35 or less items on the screen/zone...
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name)) // show GUI button on the screen for each item
                    {
                        selectedItem = inv[i]; // If this item is selected set it to the selected item
                        Debug.Log(selectedItem.Name);
                    }
                }
            }
            #endregion
            #region Scroll Inventory
            else // we are greater than 35
            {
                /* all of the code below could've been written in one line, but for the sake of the code comments it's split up */

                scrollPos =
                // the start of our viewing area
                GUI.BeginScrollView(
                // our view window
                new Rect(0, 0.25f * scr.y, 3.75f * scr.x, 8.75f * scr.y), // making the window smaller (but with the same height) as the main screen
                                                                          // our current position in the scrolling
                scrollPos,
                // the viewable area
                new Rect(0, 0, 0, 8.75f * scr.y + ((inv.Count - 35) * (0.25f * scr.y))),
                // can we see the horizontal bar?
                false,
                // can we see the vertical bar?
                true);
                #region Items in Viewing Area
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scr.x, 0 * scr.y + i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name)) // show GUI button on the screen for each item
                    {
                        selectedItem = inv[i]; // If this item is selected set it to the selected item
                        Debug.Log(selectedItem.Name);
                    }
                }
                #endregion
                // the end of our viewing area
                GUI.EndScrollView();
            }
            #endregion
        }
    }
}
