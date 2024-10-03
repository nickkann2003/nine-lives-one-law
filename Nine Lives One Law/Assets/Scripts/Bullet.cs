using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Attributes
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private float bornTime; // Time when bullet was initialized

    //Variables
    public float moveSpeed; // bullet speed
    public float lifetime; // How long bullet should last


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();

        bornTime = Time.time;

        rb.velocity = transform.up * moveSpeed; //Move bullet forward constantly
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy bullet after lifetime
        if (Time.time - bornTime >= lifetime) Destroy(gameObject);
    }
}
