using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP_Controller : MonoBehaviour
{
    // Variables
    private Rigidbody2D rb;
    private BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //comment
        Vector2 velocity = Vector2.zero;

        if(Input.GetKey(KeyCode.W))
        {
            velocity.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity.x += 1;
        }

        rb.velocity = velocity;
    }
}
