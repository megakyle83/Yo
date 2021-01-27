﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public float fadeTime;
    private Image blackScreen;

    void Start()
    {
        blackScreen = GetComponent<Image>();
        blackScreen.CrossFadeAlpha(0f, fadeTime, false);
        if (blackScreen.color.a == 0)
        {
            gameObject.SetActive(false);
        }
    }
    void Update()
    {

    }
}
