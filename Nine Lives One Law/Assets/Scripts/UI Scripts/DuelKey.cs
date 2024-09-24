using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DuelKey : MonoBehaviour
{
    public Image keyBackground;
    public TextMeshProUGUI keyText;

    public Image keyHitOverlay;
    public Image keyMissOverlay;

    public Color keyStartBackgroundColor;
    public Color keyEndBackgroundColor;

    public bool hit;

    public void displayKey()
    {
        keyBackground.enabled = true;
        keyText.enabled = true;
    }

    public void hideKey()
    {
        keyBackground.enabled = false;
        keyText.enabled = false;
        keyHitOverlay.enabled = false;
        keyMissOverlay.enabled = false;
    }

    public void hitKey()
    {
        hit = true;
    }

    public void resetKey()
    {
        keyBackground.enabled = false;
        keyText.enabled = false;
        keyHitOverlay.enabled = false;
        keyMissOverlay.enabled = false;
        keyBackground.color = keyStartBackgroundColor;
        hit = false;
    }
}
