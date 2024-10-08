using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IHittableEntity
{
    //Attributes
    public Rigidbody2D rb;
    public BoxCollider2D collider;
    private float bornTime; // Time when bullet was initialized
    public float damage;
    public List<string> targetTags = new List<string>();
    public List<string> obstacleTags = new List<string>();

    //Variables
    public float moveSpeed; // bullet speed
    public float lifetime; // How long bullet should last


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bornTime = Time.time;
        rb.velocity = transform.up * moveSpeed; //Move bullet forward constantly
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy bullet after lifetime
        if (Time.time - bornTime >= lifetime) Destroy(gameObject);
    }

    public void SetVelocity(Vector3 velocity)
    {
        moveSpeed = velocity.magnitude;
        transform.up = velocity.normalized;
        rb.velocity = transform.up * moveSpeed;
    }

    public void Shoot(Vector3 position, Vector3 velocity)
    {
        transform.position = position;
        SetVelocity(velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (targetTags.Contains(collision.gameObject.tag))
        {
            PerformHit(collision.gameObject);
        }
    }

    private void PerformHit(GameObject hitGO)
    {
        IHittableEntity hit = hitGO.GetComponent<IHittableEntity>();
        hit.HandleBulletHit(this);
    }

    /// <summary>
    /// Handles when two bullets collide
    /// </summary>
    /// <param name="b"></param>
    public void HandleBulletHit(Bullet b)
    {
        // Handle specific interaction for when bullets collide with each other here

        // Bullet hits should not send a callback, since bullets will call this on each other
        Destroy(gameObject);
    }

    /// <summary>
    /// Handles this bullets logic for when it hits an entity
    /// </summary>
    public void HandleEntityHit()
    {
        // Destroy bullet OR
        // If it bounces, bounce
        // If it pierces, handle piercing logic
    }
}
