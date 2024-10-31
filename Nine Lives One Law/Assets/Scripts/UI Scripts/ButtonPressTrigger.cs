using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class ButtonPressTrigger : MonoBehaviour
{
    public UnityEvent onButtonPress;
    public KeyCode key;

    private float waitTime = 1f;
    private float cWaitTime;

    private bool inTrigger;

    // Start is called before the first frame update
    void Start()
    {
        cWaitTime = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(key))
            {
                if(cWaitTime <= 0)
                    onButtonPress.Invoke();
            }
        }
        cWaitTime -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }

    private void OnEnable()
    {
        Debug.Log("ENABLED!");
        cWaitTime = waitTime;
    }
}
