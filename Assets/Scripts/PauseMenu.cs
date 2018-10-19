using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("Skyrim2.0/Menus/Pause")]

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused; // Show or hide pause menu.
    public GameObject pauseMenu; // Allows for a scene GameObject to be selected in the inspector that will be called using this script (in this case, this will be the 'PauseMenu' GameObject Canvas elements).

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // If the Esc key is pressed...
        {
            if (isPaused == false) // and if the game is not paused...
            {
                ActivateMenu(); // call this function (activate the pause menu).
            }
            else if (isPaused == true) // Otherwise, if the game is paused, then...
            {
                DeactivateMenu(); // call this function (deactivate the pause menu).
            }
        }
    }

    void ActivateMenu() // Function for activating the pause menu.
    {
        isPaused = true; // Game is paused.
        pauseMenu.SetActive(true); // Bring up the 'PauseMenu' GameObject Canvas elements.
        Time.timeScale = 0; // Game is frozen.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void DeactivateMenu() // Function for deactivating the pause menu.
    {
       if(!Inventory.showInv) // Referencing the 'Inventory' script and grabbing the 'showInv' bool, then setting if that's true as a condition here (if the inventory menu is open)
        {
            isPaused = false; // Game is not paused.
            pauseMenu.SetActive(false); // Close the 'PauseMenu' GameObject Canvas elements.
            Time.timeScale = 1; // Game is running in realtime. 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            isPaused = false; // Game is not paused.
            pauseMenu.SetActive(false); // Close the 'PauseMenu' GameObject Canvas elements.
        }
    }
}

