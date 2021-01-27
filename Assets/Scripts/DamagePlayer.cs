using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage;

    private LevelManager manager;
    
    void Start()
    {
        manager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //manager.Respawn();
            manager.Damage(damage);
        }
           
    }
}