using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.TimeZoneInfo;

public class Fade : MonoBehaviour
{
    private bool fadingIn;
    private float fadeProgress = 0f;
    private float fadeRate;

    public bool sleep;
    public Image fadeImage;
    private Color fadeColor = Color.black;

    public float holdTime = 0f;
    private float currentHoldTime;

    // Start is called before the first frame update
    void Start()
    {
        currentHoldTime = holdTime;
    }

    // Update is called once per frame
    void Update()
    {
        // If not sleeping
        if (!sleep)
        {
            // If fading in, increase progress
            if (fadingIn)
            {
                fadeProgress += fadeRate * Time.deltaTime;
                if (fadeProgress > 1f)
                {
                    currentHoldTime -= Time.deltaTime;
                    if(currentHoldTime < 0f)
                    {
                        fadeProgress = 1f;
                        fadingIn = false;
                    }
                }
            }
            else //  if fading out, decrease progress
            {
                fadeProgress -= fadeRate * Time.deltaTime;

            }

            // If fully faded out, set to 0 and sleep
            if(fadeProgress < 0f)
            {
                fadeProgress = 0f;
                sleep = true;
                fadingIn = true;
            }
        }

        fadeColor.a = fadeProgress;
        fadeImage.color = fadeColor;
    }

    public void StartFade(float fadeTime)
    {
        float iFadeTime = (fadeTime - holdTime) / 2f;
        if (iFadeTime < 0f)
        {
            this.fadeRate = 100f;
        }
        else
        {
            this.fadeRate = 1f / iFadeTime;
        }

        sleep = false;
        fadingIn = true;
        currentHoldTime = holdTime;
    }

    public void forceSleep()
    {
        fadingIn = true;
        sleep = true;
        fadeProgress = 0f;
        fadeColor.a = 0f;
    }
}
