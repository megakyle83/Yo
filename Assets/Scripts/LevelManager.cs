using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float respawnWait;
    public int maxHealth, maxLives, startingLives, healthCount, livesCount, coinCount;
    
    public Text coinDisplay;
    public Image heart1, heart2, heart3, men1, men2, men3;
    public Sprite heartFull, heartHalf, heartEmpty;
    public GameObject deathParticles, gameOverScreen;
    public AudioSource coinSound, levelMusic, gameOverMusic;
    public PlayerController thePlayer;

    public bool iFrame, respawnCoActive;
    private bool respawning;
    private int coinBonusCount;
    private ResetOnRespawn[] objectsToReset;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        objectsToReset = FindObjectsOfType<ResetOnRespawn>();
        healthCount = maxHealth;
        if (PlayerPrefs.HasKey("CoinCount"))
        {
            coinCount = PlayerPrefs.GetInt("CoinCount");
        }
        coinDisplay.text = "Coins: " + coinCount;
        if (PlayerPrefs.HasKey("LivesCount"))
        {
            livesCount = PlayerPrefs.GetInt("LivesCount");
        }
        else
        {
            livesCount = startingLives;
        }
        UpdateLivesDisplay();
    }

    void Update()
    {
        if(healthCount <= 0)
        {
            Respawn();
        }
        if(coinBonusCount >= 100)
        {
            livesCount += 1;
            UpdateLivesDisplay();
            coinBonusCount -= 100;
        }
    }

    public void Respawn()
    {
        if (!respawning)
        {
            livesCount -= 1;
            UpdateLivesDisplay();
        }
        if (livesCount > 0)
        {
            respawning = true;
            StartCoroutine("RespawnCo");
        }
        else
        {
            thePlayer.gameObject.SetActive(false);
            levelMusic.volume /= 4f;
            gameOverScreen.SetActive(true);
        }
    }

    public IEnumerator RespawnCo()
    {
        respawnCoActive = true;
        thePlayer.gameObject.SetActive(false);
        Instantiate(deathParticles, thePlayer.transform.position, thePlayer.transform.rotation);
        yield return new WaitForSeconds(respawnWait);
        respawnCoActive = false;
        healthCount = maxHealth;
        respawning = false;
        UpdateHeartDisplay();
        coinCount = 0;
        coinDisplay.text = "Coins: " + coinCount;
        coinBonusCount = 0;
        thePlayer.transform.position = thePlayer.respawnPos;
        thePlayer.gameObject.SetActive(true);

        for (int i=0; i<objectsToReset.Length; i++)
        {
            objectsToReset[i].gameObject.SetActive(true);
            objectsToReset[i].ResetObject();
        }
    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        coinBonusCount += coinsToAdd;
        coinSound.Play();
        coinDisplay.text =  "Coins: " + coinCount;
    }

    public void Damage(int damage)
    {
        if(!iFrame)
        {
            healthCount -= damage;
            UpdateHeartDisplay();

            if(healthCount > 0)
             {
                 thePlayer.Knockback();
                 thePlayer.hitSound.Play();
             }
        }
    }

    public void AddHealth(int healthToAdd)
    {
        healthCount += healthToAdd;
        if(healthCount > maxHealth)
        {
            healthCount = maxHealth;
        }
        coinSound.Play();
        UpdateHeartDisplay();
    }

    public void AddLives(int livesToAdd)
    {
        if(livesCount < maxLives)
        {
            livesCount += livesToAdd;
        }
        if(livesCount >= maxLives)
        {
            livesCount = maxLives;
        }
        coinSound.Play();
        UpdateLivesDisplay();
    }

    public void UpdateLivesDisplay()
    {
        switch (livesCount)
        {
            case 3:
                men1.color = new Color(1, 1, 1, 1);
                men2.color = new Color(1, 1, 1, 1);
                men3.color = new Color(1, 1, 1, 1);
                return;
            case 2:
                men1.color = new Color(1, 1, 1, 1);
                men2.color = new Color(1, 1, 1, 1);
                men3.color = new Color(1, 1, 1, .3f);
                return;
            case 1:
                men1.color = new Color(1, 1, 1, 1);
                men2.color = new Color(1, 1, 1, .3f);
                men3.color = new Color(1, 1, 1, .3f);
                return;
            case 0:
                men1.color = new Color(1, 1, 1, .3f);
                men2.color = new Color(1, 1, 1, .3f);
                men3.color = new Color(1, 1, 1, .3f);
                return;
            default:
                men1.color = new Color(1, 1, 1, 1);
                men2.color = new Color(1, 1, 1, 1);
                men3.color = new Color(1, 1, 1, 1);
                return;
        }
    }

    public void UpdateHeartDisplay()
    {
        switch (healthCount)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                return;

            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                return;

            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                return;

            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                return;

            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
        }
    }
}
