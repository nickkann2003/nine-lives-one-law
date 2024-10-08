using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected BoxCollider2D collider;

    public Vector3 targetEntityPosition;
    protected bool attackingTarget;

    public Transform bulletList;

    protected  void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        bulletList = GameObject.Find("BulletList").transform;
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
}
