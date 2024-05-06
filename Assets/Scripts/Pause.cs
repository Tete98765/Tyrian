using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseText; 

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0; 
        pauseText.SetActive(true); 
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; 
        pauseText.SetActive(false); 
        isPaused = false;
    }
}
