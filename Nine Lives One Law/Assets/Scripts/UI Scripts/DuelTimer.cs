using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuelTimer : MonoBehaviour
{
    // References
    public Animator timerAnimator;
    public GameObject timerParent;
    public Image timer;

    // Private vars
    private float maxDuelTime = 1f;
    private float currentDuelTime = 1f;

    // Sets the current time of the timer and updates the visual display
    public void SetCurrentTime(float cTime)
    {
        currentDuelTime = cTime;
        float scale = currentDuelTime / maxDuelTime;
        timer.transform.localScale = new Vector3(scale, scale, scale);
    }

    // Sets the max time of the timer
    public void SetMaxTime(float maxTime)
    {
        maxDuelTime = maxTime;
    }

    // Enables the timer parent
    public void StartTimer()
    {
        timerParent.SetActive(true);
    }

    // Disables the timer parent
    public void StopTimer()
    {
        timerParent.SetActive(false);
    }

    public void HitKey()
    {
        timerAnimator.SetTrigger("Hit");
    }

    public void MissedKey()
    {
        timerAnimator.SetTrigger("Shake");
    }
}
