using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour
{
    public bool unlocked;
    public string levelToLoad;
    public Sprite doorBottomOpen, doorBottomClosed, doorTopOpen, doorTopClosed;
    public SpriteRenderer doorBottom, doorTop;

    void Start()
    {
        PlayerPrefs.SetInt("Level 1", 1);
        if(PlayerPrefs.GetInt(levelToLoad) == 1)
        {
            unlocked = true;
        }
        else
        {
            unlocked = false;
        }
        if (unlocked)
        {
            doorBottom.sprite = doorBottomOpen;
            doorTop.sprite = doorTopOpen;
        }
        else
        {
            doorBottom.sprite = doorBottomClosed;
            doorTop.sprite = doorTopClosed;
        }
    }
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {

            if (Input.GetButtonDown("Jump") && unlocked)
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
        
    }
}
