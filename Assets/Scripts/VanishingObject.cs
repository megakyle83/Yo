using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingObject : MonoBehaviour
{
    public bool platformActive;
    public float buttonTime, waitTime;
    private Animator myAnim;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        platformActive = false;
    }
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        
        if (other.tag == "Player"&& !platformActive)
        {
            StartCoroutine("ButtonPush");
        }
        
        
    }

    public IEnumerator ButtonPush()
    {
        myAnim.SetBool("ButtonActive", true);
        transform.GetChild(0).gameObject.SetActive(true);
        platformActive = true;
        yield return new WaitForSeconds(buttonTime);
        myAnim.SetBool("ButtonActive", false);
        yield return new WaitForSeconds(waitTime);
        transform.GetChild(0).gameObject.SetActive(false);
        platformActive = false;
    }
}
