using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel, levelSelect;
    public string [] levelNames;
    public int startingLives;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void NewGame()
    {
        SceneManager.LoadScene(firstLevel);
        for(int butt = 0; butt < levelNames.Length; butt++)
        {
            PlayerPrefs.SetInt(levelNames[butt], 0);
        }
        PlayerPrefs.SetInt("CoinCount", 0);
        PlayerPrefs.SetInt("LivesCount", startingLives);
    }
    public void Continue()
    {
        SceneManager.LoadScene(levelSelect);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
