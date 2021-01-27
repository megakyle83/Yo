using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : MonoBehaviour
{
    public int addToHealth;

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
        if (other.tag == "Player")
        {
            manager.AddHealth(addToHealth);
            gameObject.SetActive(false);
        }
    }
}
