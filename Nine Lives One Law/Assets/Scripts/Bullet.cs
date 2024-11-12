using System;
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
    public Animator bulletAnimator;
    public List<string> targetTags = new List<string>();
    public List<string> obstacleTags = new List<string>();
    private Vector3 pauseVelocity;
    private Boolean paused;

    //Variables
    public float moveSpeed; // bullet speed, overriden by createbullet function
    public float lifetime; // How long bullet should last


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        paused = false;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Gameplay)
        {
            paused = false;
            rb.velocity = pauseVelocity;
            rb.constraints = RigidbodyConstraints2D.None;
        }
        else
        {
            paused = true;
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
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
        if (paused)
            bornTime += 1;
        
        if (Time.time - bornTime >= lifetime) BulletManager.instance.DestroyBullet(this);
    }

    public void SetVelocity(Vector3 velocity)
    {
        moveSpeed = velocity.magnitude;
        transform.up = velocity.normalized;
        rb.velocity = transform.up * moveSpeed;
        pauseVelocity = velocity;
    }

    public void Shoot(Vector3 position, Vector3 velocity)
    {
        transform.position = position;
        SetVelocity(velocity);
        bornTime = Time.time;
        bulletAnimator.SetTrigger("Shoot");
        collider.isTrigger = false;
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
        bulletAnimator.SetTrigger("Hit");
        collider.isTrigger = true;
    }

    /// <summary>
    /// Handles this bullets logic for when it hits an entity
    /// </summary>
    public void HandleEntityHit()
    {
        bulletAnimator.SetTrigger("Hit");
        collider.isTrigger = true;
        // Destroy bullet OR
        // If it bounces, bounce
        // If it pierces, handle piercing logic
    }

    public void HandleDamageHit(float damage)
    {

    }

    public void DestroyBullet()
    {
        BulletManager.instance.DestroyBullet(this);
    }
}
