using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyPressToUnityEvent : MonoBehaviour
{
    public UnityEvent onPressEvent;
    public KeyCode key;
    private bool keyPressed = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key) && !keyPressed)
        {
            keyPressed = true;
            onPressEvent.Invoke();
        }
    }

    private void OnEnable()
    {
        keyPressed = false;
    }
}
