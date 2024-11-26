using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour, IHittableEntity
{

    //Attributes
    public Rigidbody2D rb;
    public BoxCollider2D collider;
    //private float bornTime; // Time when bullet was initialized
    public float damage;
    public Animator bulletAnimator;
    public List<string> targetTags = new List<string>();
    public List<string> obstacleTags = new List<string>();

    //Variables
    public float moveSpeed; // bullet speed, overriden by createbullet function
    public float lifetime; // How long bullet should last

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        //hit.HandleBulletHit(this);
    }



    /// <summary>
    /// Handles when two bullets collide
    /// </summary>
    /// <param name="b"></param>
    public void HandleBulletHit(Bullet b)
    {
        // Handle specific interaction for when bullets collide with each other here
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

    public void HandleDamageHit(float damage)
    {

    }

}
