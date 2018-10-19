using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Variables
    public List<Item> inv = new List<Item>(); // List of items
    public static bool showInv; // Show or hide inventory
    public Item selectedItem; // The item we are interacting with
    public int money; // How much moula we have

    public Vector2 scr = Vector2.zero; // 16:9
    public Vector2 scrollPos = Vector2.zero; // Scroll bar position 
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
        inv.Add(ItemData.CreateItem(404)); // This will default to the Apple since case 404 doesn't exist and apple is the default

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
                #region Non-scroll Inventory
                if(inv.Count <= 35) // If we 35 or less items on the screen/zone...
                {
                    for (int i = 0; i < inv.Count; i++)
                    {
                        if(GUI.Button(new Rect(0.5f*scr.x, 0.25f*scr.y + i*(0.25f*scr.y), 3*scr.x, 0.25f*scr.y), inv[i].Name)) // show GUI button on the screen for each item
                        {
                            selectedItem = inv[i]; // If this item is selected set it to the selected item
                            Debug.Log(selectedItem.Name);
                        }
                    }
                }
                #endregion
                #region Scroll Inventory

                #endregion
            }
        }
    }
}
