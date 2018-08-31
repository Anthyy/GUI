using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public bool isPaused;
    public GameObject pauseMenu;
    void Start()
    {
        Time.timeScale = 1; // Game is running in realtime
        pauseMenu.SetActive(false);
        isPaused = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Time.timeScale = 1; // Game is running in realtime
                pauseMenu.SetActive(false);
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0; // Game is frozen
                pauseMenu.SetActive(true);

                isPaused = true;
            }
        }
    }
}
