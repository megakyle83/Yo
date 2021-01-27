using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{ 
    // Transform of the GameObject you want to shake
    private Transform camPos;
    // Desired duration of the shake effect
    private float shakeDuration = 0f;
    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.4f;
    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;
    // The initial position of the GameObject
    Vector3 initialPosition;

    void Awake()
    {
            camPos = GetComponent(typeof(Transform)) as Transform;
    }
    void Start()
    {
        initialPosition = camPos.localPosition;
    }
    void Update()
    {
        if (shakeDuration > 0)
        {
            camPos.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            camPos.localPosition = initialPosition;
        }
    }
    public void TriggerShake()
    {
        shakeDuration = .5f;
    }
}
