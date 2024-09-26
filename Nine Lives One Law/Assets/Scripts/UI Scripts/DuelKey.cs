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

    private string text;
    private char character;

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
        keyHitOverlay.gameObject.SetActive(false);
        keyMissOverlay.gameObject.SetActive(false);
    }

    public void hitKey()
    {
        hit = true;
        keyHitOverlay.gameObject.SetActive(true);
        keyBackground.color = keyEndBackgroundColor;
    }

    public void missKey()
    {
        hit = true;
        keyMissOverlay.gameObject.SetActive(true);
        keyBackground.color = keyEndBackgroundColor;
    }

    public void resetKey()
    {
        keyBackground.enabled = false;
        keyText.enabled = false;
        keyHitOverlay.gameObject.SetActive(false);
        keyMissOverlay.gameObject.SetActive(false);
        keyBackground.color = keyStartBackgroundColor;
        hit = false;
    }

    public void setKeyPosition(Vector2 pos)
    {
        GetComponent<RectTransform>().localPosition = pos;
    }

    public void SetText(char c)
    {
        this.character = c;
        text = c.ToString();
        keyText.text = text;
    }
}
