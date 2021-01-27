using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour
{
    public GameObject deathParticles;

    private Rigidbody2D playerRigidbody;

    public float bounceAmount;

    void Start()
    {
        playerRigidbody = transform.parent.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            Instantiate(deathParticles, other.transform.position, other.transform.rotation);
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, bounceAmount, 0f);
        }
        if(other.tag == "Boss")
        {
            Instantiate(deathParticles, other.transform.position, other.transform.rotation);
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, bounceAmount, 0f);
            other.transform.parent.GetComponent<Boss>().takeDamage = true;
        }
        if (other.tag == "Button")
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, bounceAmount, 0f);
        }
    }
}
