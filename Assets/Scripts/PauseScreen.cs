using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public string mainMenu, levelSelect;
    public GameObject pauseScreen;
    private LevelManager manager;
    private PlayerController player;

    void Start()
    {
        manager = FindObjectOfType<LevelManager>();
        player = FindObjectOfType<PlayerController>();
        pauseScreen.SetActive(false);
    }
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if(Time.timeScale == 0f)
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
        pauseScreen.SetActive(true);
        player.canMove = false;
        manager.levelMusic.Pause();
    }
    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        player.canMove = true;
        Time.timeScale = 1;
        manager.levelMusic.Play();
    }
    public void LevelSelect()
    {
        PlayerPrefs.SetInt("CoinCount", manager.coinCount);
        PlayerPrefs.SetInt("LivesCount", manager.livesCount);
        Time.timeScale = 1;
        SceneManager.LoadScene(levelSelect);
    }
    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenu);
    }
}

