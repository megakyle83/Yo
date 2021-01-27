using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed, jumpSpeed, knockbackForce, knockbackLength, iFrameLength, groundCheckRadius, onMovingSpeed;
    public bool isGrounded, canMove;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Vector3 respawnPos;
    public GameObject stompBox;
    public LevelManager manager;
    public AudioSource jumpSound, hitSound;

    private float iFrameTime, knockbackTime, activeMoveSpeed;
    private bool onMoving;
    private Animator myAnim;
    private SpriteRenderer playerSprite;
    private Shake shaker;
    public Rigidbody2D myRigidbody;

    void Start()
    {
        shaker = FindObjectOfType<Shake>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        respawnPos = transform.position;
        manager = FindObjectOfType<LevelManager>();
        playerSprite = GetComponent<SpriteRenderer>();
        activeMoveSpeed = moveSpeed;
        canMove = true;
    }
 
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        if (knockbackTime <= 0 && canMove)
        {
            if (onMoving)
            {
                activeMoveSpeed = moveSpeed * onMovingSpeed;
            }
            else
            {
                activeMoveSpeed = moveSpeed;
            }
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                myRigidbody.velocity = new Vector3(activeMoveSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                myRigidbody.velocity = new Vector3(-activeMoveSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
            }
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
                jumpSound.Play();
            }
        }

        myAnim.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));
        myAnim.SetBool("IsGrounded", isGrounded);

        if (knockbackTime > 0)
        {
            knockbackTime -= Time.deltaTime;
            if(transform.localScale.x > 0)
            {
                myRigidbody.velocity = new Vector3(-knockbackForce, knockbackForce, 0f);
            }
            else
            {
                myRigidbody.velocity = new Vector3(knockbackForce, knockbackForce, 0f);
            }
        }

        if (iFrameTime > 0)
        {
            playerSprite.color = new Color(1, 1, 1, .3f);
            iFrameTime -= Time.deltaTime;
        }

        if (iFrameTime <= 0.1f)
        {
            manager.iFrame = false;
            playerSprite.color = new Color(1, 1, 1, 1);
        }
        
        if(myRigidbody.velocity.y < 0)
        {
            stompBox.SetActive(true);
        }
        else
        {
            stompBox.SetActive(false);
        }
    }

    public void Knockback()
    {
        knockbackTime = knockbackLength;
        iFrameTime = iFrameLength;
        manager.iFrame = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "KillPlane")
        {
            //gameObject.SetActive(false);
            //transform.position = respawnPos;
            manager.healthCount = 0;
            manager.UpdateHeartDisplay();
            manager.Respawn();
        }
        if(other.tag == "Checkpoint")
        {
            respawnPos = other.transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
            onMoving = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
            onMoving = false;
        }
    }
}
