using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EXP_Controller : MonoBehaviour
{
    // Variables
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    [SerializeField]
    private Animator playerAnimator;
    public float moveSpeed;

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
            velocity.y += moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity.y -= moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity.x -= moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity.x += moveSpeed;
        }

        rb.velocity = velocity;
        if (velocity.magnitude > 0)
        {
            playerAnimator.SetBool("Moving", true);
        }
        else
        {
            playerAnimator.SetBool("Moving", false);
        }
    }
}
