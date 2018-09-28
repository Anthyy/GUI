using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public bool isPaused = false;
    public GameObject pauseMenu;

    /*void Start()
    {
        Time.timeScale = 1; // Game is running in realtime
        pauseMenu.SetActive(false);
        isPaused = false;
    }*/


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // == checking
            // = setting
            if (isPaused == false)
            {
                ActivateMenu();
            }           
            else if (isPaused) // Or isPaused == true;
            {
                DeactivateMenu();
            }
        }
    }

    void ActivateMenu()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
    }
    void DeactivateMenu()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }
}

