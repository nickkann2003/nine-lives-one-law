using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IHittableEntity
{
    //Attributes
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private float bornTime; // Time when bullet was initialized
    public float damage;
    public List<string> targetTags = new List<string>();

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

    private void OnCollisionEnter(Collision collision)
    {
     if(targetTags.Contains(collision.gameObject.tag))
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
