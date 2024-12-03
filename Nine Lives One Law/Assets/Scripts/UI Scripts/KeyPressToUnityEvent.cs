using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyPressToUnityEvent : MonoBehaviour
{
    public UnityEvent onPressEvent;
    public KeyCode key;
    private bool keyPressed = false;

    private float waitTime = 0.2f;
    private float cWaitTime;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key) && !keyPressed)
        {
            if (cWaitTime <= 0)
            {
                keyPressed = true;
                onPressEvent.Invoke();
                AudioManager.instance.PlaySound("door-squeek");
            }
        }
        cWaitTime -= Time.deltaTime;
    }

    private void OnEnable()
    {
        cWaitTime = waitTime;
        keyPressed = false;
    }

}
