using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IHittableEntity
{
    protected Rigidbody2D rb;
    protected BoxCollider2D collider;

    public Vector3 targetEntityPosition;
    protected bool attackingTarget;

    public Transform bulletList;

    protected float health;
    public int maxHealth;

    protected float invTime;
    public float maxInvTime;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        bulletList = GameObject.Find("BulletList").transform;
        health = maxHealth;
    }

    protected void Update()
    {
        if (attackingTarget)
        {
            AttackTarget();
        }
        else
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

    public void HandleBulletHit(Bullet b)
    {
        // Subtract health
        health -= b.damage;
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
        // Activate immunity
        if(health < 0)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Heals the enemy by a percent of its max health
    /// </summary>
    /// <param name="percent">Percent of HP healed</param>
    public virtual void Heal(float percent)
    {
        health += maxHealth / (int)(100/percent);
        Debug.Log("health: " + health + ", maxHealth: " + maxHealth);
    }
}
