using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public float waitTime, waitToMove;
    public string levelToLoad, levelToUnlock;
    private bool movePlayer;
    private Animator myAnim;
    private Collider2D collin;
    private PlayerController thePlayer;
    private CameraController theCamera;
    private LevelManager manager;

    void Start()
    {
        collin = GetComponent<BoxCollider2D>();
        myAnim = GetComponent<Animator>();
        myAnim.SetBool("IsTriggered",false);
        thePlayer = FindObjectOfType<PlayerController>();
        theCamera = FindObjectOfType<CameraController>();
        manager = FindObjectOfType<LevelManager>();
    }
    void Update()
    {
        if(movePlayer)
        {
            thePlayer.myRigidbody.velocity = new Vector3(thePlayer.moveSpeed, thePlayer.myRigidbody.velocity.y, 0f);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            myAnim.SetBool("IsTriggered",true);
            collin.enabled= false;
            StartCoroutine("LoadNext");
        }
    }
    public IEnumerator LoadNext()
    {
        thePlayer.canMove = false;
        theCamera.followTarget = false;
        manager.iFrame = true;
        manager.levelMusic.Stop();
        manager.gameOverMusic.Play();
        thePlayer.myRigidbody.velocity = Vector3.zero;
        PlayerPrefs.SetInt("CoinCount", manager.coinCount);
        PlayerPrefs.SetInt("LivesCount", manager.livesCount);
        PlayerPrefs.SetInt(levelToUnlock, 1);
        yield return new WaitForSeconds(waitToMove);
        movePlayer = true;
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(levelToLoad);
    }
}
