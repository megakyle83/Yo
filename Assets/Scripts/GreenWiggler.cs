using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWiggler : MonoBehaviour
{
    public Transform left;
    public Transform right;

    public float moveSpeed;
    public bool movingRight;

    private Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(movingRight && transform.position.x > right.position.x)
        {
            movingRight = false;
        }
        if(!movingRight && transform.position.x < left.position.x)
        {
            movingRight = true;
        }
        if (movingRight)
        {
            myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
        }
        else
        {
            myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
        }
    }
}
