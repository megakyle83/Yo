using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public float moveSpeed;
    private bool canMove;
    private Rigidbody2D myRigidBody;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (canMove)
        {
            myRigidBody.velocity = new Vector3(-moveSpeed, myRigidBody.velocity.y, 0f);
        }
    }
    private void OnBecameVisible()
    {
        canMove = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "KillPlane")
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
    void OnEnable()
    {
        canMove = false;
    }
}
