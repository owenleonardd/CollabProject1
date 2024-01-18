using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    
    public CanvasGroup canvasGroup;
    public bool fadeIn = false;
    public bool fadeOut = false;
    
    public float fadeSpeed = 1f;
    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            canvasGroup.alpha += Time.deltaTime*fadeSpeed;
            if (canvasGroup.alpha >= 1)
            {
                fadeIn = false;
            }
        }
        else if (fadeOut)
        {
            canvasGroup.alpha -= Time.deltaTime*fadeSpeed;
            if (canvasGroup.alpha <= 0)
            {
                fadeOut = false;
            }
        }
    }
    
    public void FadeIn()
    {
        fadeIn = true;
        fadeOut = false;
    }
    
    public void FadeOut()
    {
        fadeOut = true;
        fadeIn = false;
    }
}
