using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private LevelManager manager;

    public int coinValue;

    void Start()
    {
        manager = FindObjectOfType<LevelManager>();
    }
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            manager.AddCoins(coinValue);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
