using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public bool bossActive, bossOnRight, takeDamage, waitToRespawn;
    public int startingHealth;
    public float timeBetweenDrops, waitForPlatforms;
    public Transform leftPoint, rightPoint, dropSawSpawnPoint;
    public GameObject dropSaw, boss, rightPlatforms, leftPlatforms, levelExit;

    private int currentHealth;
    private float dropCount, platformCount, storeDropTime;
    private SpriteRenderer bossSprite;
    private CameraController theCamera;
    private LevelManager manager;

    void Start()
    {
        storeDropTime = timeBetweenDrops;
        manager = FindObjectOfType<LevelManager>();
        theCamera = FindObjectOfType<CameraController>();
        dropCount = timeBetweenDrops;
        platformCount = waitForPlatforms;
        currentHealth = startingHealth;
        boss.transform.position = rightPoint.position;
        bossOnRight = true;

    }
    void Update()
    {
        if (manager.respawnCoActive)
        {
            bossActive = false;
            waitToRespawn = true;
        }
        if(waitToRespawn && !manager.respawnCoActive)
        {
            boss.SetActive(false);
            leftPlatforms.SetActive(false);
            rightPlatforms.SetActive(false);

            timeBetweenDrops = storeDropTime;

            platformCount = waitForPlatforms;
            dropCount = timeBetweenDrops;
            currentHealth = startingHealth;

            boss.transform.position = rightPoint.position;
            bossOnRight = true;

            theCamera.followTarget = true;

            waitToRespawn = false;
        }
        if (bossActive)
        {
            theCamera.followTarget = false;
            theCamera.transform.position = Vector3.Lerp(theCamera.transform.position, new Vector3
                (transform.position.x, theCamera.transform.position.y, theCamera.transform.position.z),theCamera.smoothing * Time.deltaTime);
            boss.SetActive(true);

            if (dropCount > 0)
            {
                dropCount -= Time.deltaTime;//makes the int count down to 0 from it's value
            }
            else
            {
                dropSawSpawnPoint.position = new Vector3
                    (Random.Range(leftPoint.position.x, rightPoint.position.x), dropSawSpawnPoint.position.y, dropSawSpawnPoint.position.z);
                Instantiate(dropSaw, dropSawSpawnPoint.position, dropSawSpawnPoint.rotation);
                dropCount = timeBetweenDrops;
            }
            if (bossOnRight)
            {
                if (platformCount > 0)
                {
                    platformCount -= Time.deltaTime;
                }
                else
                {
                    rightPlatforms.SetActive(true);
                }
            }
            else
            {
                if (platformCount > 0)
                {
                    platformCount -= Time.deltaTime;
                }
                else
                {
                    leftPlatforms.SetActive(true);
                }
            }
            if (takeDamage)
            {
                currentHealth -= 1;
                if(currentHealth <= 0)
                {
                        levelExit.SetActive(true);
                    theCamera.followTarget = true;
                        gameObject.SetActive(false);
                }
                if (bossOnRight)
                {
                    boss.transform.position = leftPoint.position;
                    boss.transform.localScale = new Vector3(-3f, 4f, 1f);

                }
                else
                {
                    boss.transform.position = rightPoint.position;
                    boss.transform.localScale = new Vector3(3f, 4f, 1f);
                }
                bossOnRight = !bossOnRight;
                rightPlatforms.SetActive(false);
                leftPlatforms.SetActive(false);
                platformCount = waitForPlatforms;
                timeBetweenDrops = timeBetweenDrops / 2f;
                takeDamage = false;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            bossActive = true;
        }
    }
}
