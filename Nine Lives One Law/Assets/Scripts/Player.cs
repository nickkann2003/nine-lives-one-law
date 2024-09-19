using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Variables
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
        MovePlayer();
        FaceMouse();
    }

    // Moves the player depending on move speed variable, tells animator if moving
    void MovePlayer()
    {
        Vector2 velocity = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
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

    // Makes the player rotation face the mouse
    void FaceMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float AngleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad - 90;
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }
}
