using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    //Duration of the shake effect
    public float shakeDuration = 0.0F;
    // Strenth for the shake.
    public float shakeStrength = 0.1f;
    // A measure of how fast it will fade
    public float fadingSpeed = 5.0f;
    // The initial position of the camera
    Vector3 initialPosition;

    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeStrength;

            shakeDuration -= Time.deltaTime * fadingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }
    public void TriggerShake()
    {
        shakeDuration = 2.0f;
    }
}
