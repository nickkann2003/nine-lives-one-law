using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : EnemyBase
{
    //[SerializeField]
    //private Animator playerAnimator;
    private float lastShotTime; // Last time enemy shot

    // Game Objects
    public GameObject bullet;
    public Duel duelScript;

    // Variables
    public float range;
    public float moveSpeed;
    public float shootCooldown;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        lastShotTime = -shootCooldown;
        health = maxHealth;
        duelScript = GameObject.Find("Player").GetComponent<Duel>();
    }

    // Update is called once per frame
    new void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !duelScript.duel && isDuelReady())
        { //If M is pressed and there is no duel and boss is duel ready, start duel
            int x = Random.Range(5, 15);
            duelScript.startDuel(x, x);
            duelScript.boss = this.gameObject;
        }
        if (!duelScript.duel)
        { //Only update if no duel
            base.Update();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        { //Increment health for testing
            health--;
            Debug.Log("health: " + health + ", maxHealth: " + maxHealth);
        }

        if (isDuelReady())
        {
            healthBar.setSliderColor(Color.green);
        }
        else
        {
            healthBar.setSliderColor(Color.red);
        }
    }

    public override void Wander()
    {
        // Like base enemy, boss 1 never wanders, always hunting
        attackingTarget = true;
    }

    // Acts same as base enemy
    public override void AttackTarget()
    {
        if (isActive)
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
                    BulletManager.instance.CreateBullet(Bullets.EnemyBullet, 1, transform.position + (transform.up * 0.8f), (targetEntityPosition - transform.position).normalized * 5);
                    // Update last shot time
                    lastShotTime = Time.time;
                }
            }
        }
    }

    // If boss is at 25% health or less, it is able to be dueled
    public bool isDuelReady()
    {
        //return true;
        return health <= maxHealth / 4;
    }
}
