using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Attributes
    private float bornTime; // Time when bullet was initialized

    //Variables
    public float moveSpeed; // Movement speed
    public float lifetime; // How long bullet should last


    // Start is called before the first frame update
    void Start()
    {
        bornTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        if (Time.time - bornTime >= lifetime) Destroy(gameObject);
    }
}
