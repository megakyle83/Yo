using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string mainMenu, levelSelect;
    private LevelManager manager;

    void Start()
    {
        manager = FindObjectOfType<LevelManager>();
    }
    void Update()
    {
        
    }
    public void Restart()
    {
        PlayerPrefs.SetInt("CoinCount", 0);
        PlayerPrefs.SetInt("LivesCount", manager.startingLives);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LevelSelect()
    {

        PlayerPrefs.SetInt("CoinCount", 0);
        PlayerPrefs.SetInt("LivesCount", manager.startingLives);
        SceneManager.LoadScene(levelSelect);
    }
    public void Quit()
    {
        SceneManager.LoadScene(mainMenu);
    }
}

