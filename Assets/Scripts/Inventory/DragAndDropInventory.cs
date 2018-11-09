using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropInventory : MonoBehaviour
{
    #region Variables
    [Header("Inventory")]
    public bool showInv; // Toggle Inventory UI
    public static List<Item> inventory = new List<Item>(); // Items
    public int slotX = 8, slotY = 5; // Size x and size y
    private Rect inventorySize;
    [Header("Dragging")]
    public bool isDragging; // Are we dragging an item
    public Item draggedItem;
    public int draggedFrom;
    public GameObject droppedItem;
    [Header("Tool Tip")]
    public int toolTipItem; // Index reference to the item that we are needing the toolTip for
    public bool showToolTip;
    private Rect toolTipRect;
    [Header("Other References")]
    private Vector2 scr;
    #endregion

    #region ClampToScreen
    private Rect ClampToScreen(Rect r)
    {                   
                                  // Width of the screen - width of the box
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
        return r;
    }
    #endregion
    #region AddItem
    public static void AddItem(int itemID)
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].Name == null)
            {
                inventory[i] = ItemData.CreateItem(itemID);
                Debug.Log("Add Item: " + inventory[i].Name);
                return; // Return is just a safety precaution here
            }
        }
    }
    #endregion
    #region Drop Item
    public void DropItem(int itemID)
    {
        // Finding the item in the relevant file (in Prefab)
        droppedItem = Resources.Load("Prefab/" + ItemData.CreateItem(itemID).MeshName) as GameObject;
        // Spawn the item into the world and remember what item that was 
        droppedItem = Instantiate(droppedItem, transform.position + transform.forward * 3, Quaternion.identity); // Instantiating the item in front of us (dropping it)
        // Adding RigidBody (you need to apply RigidBody so gravity can be applied to it)
        droppedItem.AddComponent<Rigidbody>().useGravity = true;
        // Empty the dropped item...we are done
        droppedItem = null;
    }
    #endregion
    #region DrawItem
    void DrawItem(int windowID)
    {
        if(draggedItem.Icon != null)
        {
            GUI.DrawTexture(new Rect(0, 0, scr.x * 0.5f, scr.y * 0.5f), draggedItem.Icon);
        }
    }
    #endregion
    #region ToolTip
    #region ToolTipContent
    private string ToolTipText(int ItemID)
    {
        string toolTipText = "Name: "+inventory[ItemID].Name +
            " Description: " + inventory[ItemID].Description +
            " Type: " + inventory[ItemID].Type +
            " Value: " + inventory[ItemID].Value;

        return toolTipText;
    }
    #endregion
    #region ToolTipWindow
    void DrawToolTip(int windowID)
    {
        GUI.Box(new Rect(0, 0, scr.x * 2, scr.y * 3), ToolTipText(toolTipItem));
    }
    #endregion
    #endregion
    #region ToggleInventory
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
    #endregion
    #region Drag Inventory
    void InventoryDrag(int WindowID)
    {
        GUI.Box(new Rect(0, scr.y * 0.25f, scr.x*6, scr.y * 0.5f), "Banner");
        GUI.Box(new Rect(0, scr.y * 4.25f, scr.x*6, scr.y * 0.5f), "Gold & EXP");
        showToolTip = false;
        #region Nested for loop 
        Event e = Event.current;
        int i = 0; 
        for(int y = 0; y < slotY; y++)
        {
            for (int x = 0; x < slotX; x++)
            {
                Rect slotLocation = new Rect(scr.x * 0.125f + x * (scr.x * 0.75f), scr.y * 0.75f + y * (scr.y * 0.65f), scr.x * 0.75f, scr.y * 0.65f);
                GUI.Box(slotLocation, "");
                #region Pickup Item
                /*
                 * if we are interacting with leftmouse button and the interaction is click down
                 * and the mouse cursor is over an item slot while we are not already holding an item and the item slot isn't empty
                 * as well as we aren't hitting the change inventory key Left Shift
                 * */
                if(e.button == 0 && e.type == EventType.MouseDown && slotLocation.Contains(e.mousePosition) && !isDragging && inventory[i].Name != null && !Input.GetKey(KeyCode.LeftShift))
                {
                    // we pick up item
                    draggedItem = inventory[i];
                    // the inventory slot is now empty
                    inventory[i] = new Item();
                    // we are holding an item 
                    isDragging = true;
                    // we remember where this item came from 
                    draggedFrom = 1;
                    // debug 
                    Debug.Log("Dragging: "+draggedItem.Name);
                }

                #endregion
                #region Swap Item
                /*
                 * If we lift up left mouse button...
                 * and we have a draggable item over a slot that isn't empty...*/
                if(e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && isDragging && inventory[i].Name != null)
                {
                    Debug.Log("Swapping: " + draggedItem.Name + " With: " + inventory[i].Name);
                    // The slot that is full now moves to where our dragged item came from
                    inventory[draggedFrom] = inventory[i];
                    // The slot we are dropping into is now filled with our dragged item
                    inventory[i] = draggedItem;
                    // The dragged item is now empty 
                    draggedItem = new Item();
                    // We are no longer dragging 
                    isDragging = false;
                }
                #endregion
                #region Place Item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && isDragging && inventory[i].Name != null)
                {
                    Debug.Log("Place: " + draggedItem.Name + " Into: " + i);
                    // The slot we are dropping the item into is now filled with the draggedItem
                    inventory[i] = draggedItem;
                    // The item we use to drag is empty
                    draggedItem = new Item();
                    // We are no longer holding an item
                    isDragging = false;
                }
                #endregion
                #region Return Item
                if(e.button == 0 && e.type == EventType.MouseUp && i == ((slotX*slotY) - 1) && isDragging)
                {
                    // put the item back where you got it from
                    inventory[draggedFrom] = draggedItem;
                    // dragged item is now empty
                    draggedItem = new Item();
                    // we are no longer dragging
                    isDragging = false;
                }
                #endregion
                #region Draw Item Icon
                if(inventory[i].Name != null)
                {
                    GUI.DrawTexture(slotLocation, inventory[i].Icon);
                    #region Set ToolTip on Mouse Hover
                    if(slotLocation.Contains(e.mousePosition) && !isDragging && showInv)
                    {
                        toolTipItem = i;
                        showToolTip = true;
                    }
                    #endregion
                }
                #endregion
                i++;
            }
        }
        #endregion
        #region Drag Points
        GUI.DragWindow(new Rect(0, 0, scr.x*6, scr.y * 0.5f)); // Top drag
        GUI.DragWindow(new Rect(0, scr.y * 0.5f, scr.x * 0.25f, scr.y * 3.5f)); // Left drag
        GUI.DragWindow(new Rect(scr.x * 5.75f, scr.y*0.5f, scr.x*0.25f, scr.y*3.5f)); // Right drag 
        GUI.DragWindow(new Rect(0, scr.y * 4, scr.x * 6, scr.y * 0.5f)); // Bottom drag
        #endregion
    }
    #endregion
    #region Start
    private void Start()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;
        inventorySize = new Rect(scr.x, scr.y, 6 * scr.x, 4.5f * scr.y);
        for (int i = 0; i < (slotX * slotY); i++)
        {
            inventory.Add(new Item());
        }
        AddItem(0);
        AddItem(2);
        AddItem(102);
        AddItem(201);
        AddItem(202);
        AddItem(302);
    }
    #endregion
    #region Update
    private void Update()
    {
        if(scr.x != Screen.width / 16 || scr.y != Screen.height / 9)
        {
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInv();
        }
    }
    #endregion
    #region OnGUI
    private void OnGUI()
    {
        Event e = Event.current;
        #region Inventory if show Inv is true 
        if (showInv)
        {
            inventorySize = ClampToScreen(GUI.Window(1, inventorySize, InventoryDrag, "Drag Inventory")); // Windows have a window ID
        }
        #endregion
        #region ToolTip
        if(showToolTip && showInv)
        {
            toolTipRect = new Rect(e.mousePosition.x + 0.01f, e.mousePosition.y + 0.001f, scr.x * 2, scr.y * 3);
            GUI.Window(15, toolTipRect, DrawToolTip, "");
        }

        #endregion
        #region Drop Item (MouseUp || !showInv)
        if((e.button == 0 && e.type == EventType.MouseUp && isDragging) || (isDragging && !showInv))
        {
            DropItem(draggedItem.Id);
            Debug.Log("Dropped: " +draggedItem.Name);
            draggedItem = new Item();
            isDragging = false;
        }
        #endregion
        #region Draw Item on Mouse
        if (isDragging)
        {
            if (draggedItem != null)
            {
                Rect mouseLocation = new Rect(e.mousePosition.x + 0.125f, e.mousePosition.y + 0.125f, scr.x * 0.5f, scr.y * 0.5f);
                GUI.Window(2, mouseLocation, DrawItem, "");                
            }
        }
        #endregion
    }
    #endregion
}