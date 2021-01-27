using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneUp : MonoBehaviour
{
    public int addToLives;

    private LevelManager manager;

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
            manager.AddLives(addToLives);
            gameObject.SetActive(false);
        }
    }
}
