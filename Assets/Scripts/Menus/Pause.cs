using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused) Resume();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        paused = true;
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        paused = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("Clicked");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);

    }

    public void QuitGame()
    {
        Application.Quit();

    }

}

