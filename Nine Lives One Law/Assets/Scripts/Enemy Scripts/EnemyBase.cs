using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IHittableEntity
{
    protected Rigidbody2D rb;
    protected BoxCollider2D collider;
    protected FloatingHealthBar healthBar;

    public Vector3 targetEntityPosition;
    protected bool attackingTarget;

    public Transform bulletList;

    protected float health;
    public int maxHealth;

    protected float invTime;
    public float maxInvTime;

    protected bool isActive = true;
    protected Vector2 pauseVelocity;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        bulletList = GameObject.Find("BulletList").transform;
        health = maxHealth;
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    protected void GameManager_OnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Gameplay)
        {
            isActive = true;
            rb.velocity = pauseVelocity;
            rb.isKinematic = false;
        }


        else
        {
            isActive = false;
            pauseVelocity = rb.velocity;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
        
    }

    protected void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    protected void Update()
    {
        if (attackingTarget && isActive)
        {
            AttackTarget();
        }
        else if(isActive)
        {
            Wander();
        }
    }

    /// <summary>
    /// Performed every frame if this enemy is not attacking its target
    /// Performs checks to see if it should start attacking
    /// </summary>
    abstract public void Wander();

    /// <summary>
    /// Performed every frame if this enemy is attacking its target
    /// Performs checks to see if it should stop attacking
    /// </summary>
    abstract public void AttackTarget();

    /// <summary>
    /// Sets the position of this enemy's target
    /// </summary>
    /// <param name="position"></param>
    public void SetTargetPosition(Vector3 position)
    {
        targetEntityPosition = position;
    }

    public virtual void HandleBulletHit(Bullet b)
    {
        Debug.Log("base handlebullethit");
        // Subtract health
        health -= b.damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        // Activate immunity
        if (health < 0)
        {
            Destroy(this.gameObject);
        }

        // Return to bullet that it hit an entity
        b.HandleEntityHit();
    }

    public void HandleDamageHit(float damage)
    {
        // Subtract health
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        // Activate immunity
        if (health < 0)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Heals the enemy by a percent of its max health
    /// </summary>
    /// <param name="percent">Percent of HP healed</param>
    public void Heal(float percent)
    {
        health += maxHealth / (int)(100/percent);
        healthBar.UpdateHealthBar(health, maxHealth);
        Debug.Log("health: " + health + ", maxHealth: " + maxHealth);
    }
}
