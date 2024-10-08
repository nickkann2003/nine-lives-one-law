using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : EnemyBase
{

    //[SerializeField]
    //private Animator playerAnimator;
    private float lastShotTime; // Last time enemy shot

    // Game Objects
    public GameObject bullet;

    // Variables
    public float range;
    public float moveSpeed;
    public float shootCooldown;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        lastShotTime = -shootCooldown;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        
    }

    public override void Wander()
    {
        // Base enemy never wanders, always hunting
        attackingTarget = true;
    }

    public override void AttackTarget()
    {
        transform.up = targetEntityPosition - transform.position; //Look at player
        if (Vector3.Distance(transform.position, targetEntityPosition) > range)
        { // If our of range, move forward
            rb.velocity = transform.up * moveSpeed;
            //playerAnimator.SetBool("Moving", true);
        }
        else
        { // If in range, do not move
            rb.velocity = Vector2.zero;
            //playerAnimator.SetBool("Moving", false);
            if (Time.time - lastShotTime >= shootCooldown)
            { // If shot cooldown is up, shoot
                Vector3 bulletSpawn = transform.position;
                bulletSpawn += transform.up * 0.8f; // bullet offset so it spawns on the gun
                Instantiate(bullet, bulletSpawn, transform.rotation, bulletList.transform);
                // Update last shot time
                lastShotTime = Time.time;
            }
        }
    }
}
