using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Boss1 : EnemyBase
{
    //[SerializeField]
    //private Animator playerAnimator;
    private float lastShotTime; // Last time enemy shot
    private int ammo;
    private float dualWieldOffset;

    // Game Objects
    public GameObject bullet;
    public Duel duelScript;

    // Variables
    public float range;
    public float moveSpeed;
    public float shootCooldown;

    [SerializeField]
    private List<GameObject> drops;

    [SerializeField]
    private UnityEvent onDeathEvents;
    

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        lastShotTime = Time.time;
        health = maxHealth;
        duelScript = GameObject.Find("Player").GetComponent<Duel>();
        ammo = 0;
        dualWieldOffset = 0.3f;
    }

    // Update is called once per frame
    new void Update()
    {
        if (Input.GetMouseButtonDown(1) && !duelScript.duel && isDuelReady())
        { //If M is pressed and there is no duel and boss is duel ready, start duel
            int x = Random.Range(5, 15);
            duelScript.startDuel(x, x);
            duelScript.boss = this.gameObject;
        }
        if (!duelScript.duel)
        { //Only update if no duel
            base.Update();
        }

        /*
        if (Input.GetKeyDown(KeyCode.Y))
        { //Increment health for testing
            health--;
            Debug.Log("health: " + health + ", maxHealth: " + maxHealth);
        }
        */

        if (isDuelReady())
        { //If duel ready, make health bar green
            healthBar.setSliderColor(Color.green);
        }
        else
        { //If not duel ready, make health bar red
            healthBar.setSliderColor(Color.red);
        }
    }

    public override void Wander()
    {
        // Like base enemy, boss 1 never wanders, always hunting
        //attackingTarget = true;

        if (Vector3.Distance(transform.position, targetEntityPosition) < range * 3)
        {
            attackingTarget = true;
        }
    }

    // Acts same as base enemy
    public override void AttackTarget()
    {
        if (isActive)
        {
            rb.velocity = Vector2.zero;
            transform.up = targetEntityPosition - transform.position; //Look at player
            if (ammo == 0)
            {
                if (Time.time - lastShotTime >= (shootCooldown * 15))
                { //If no ammo, there is a long shot cooldown to reload
                    ammo = 12; //Reloads
                }
            }
            else
            {
                //transform.up = targetEntityPosition - transform.position; //Look at player
                if (Vector3.Distance(transform.position, targetEntityPosition) > range)
                { // If our of range, move forward
                    rb.velocity = transform.up * moveSpeed;
                    //playerAnimator.SetBool("Moving", true);
                }
                //else
                //{ // If in range, do not move
                //    rb.velocity = Vector2.zero;
                //    //playerAnimator.SetBool("Moving", false);
                //}
                if (Time.time - lastShotTime >= shootCooldown)
                { // If reloaded, shoot fast
                    ammo--;
                    BulletManager.instance.CreateBullet(Bullets.EnemyBullet, 1, transform.position + (transform.up * 1.4f) + (transform.right * dualWieldOffset), (targetEntityPosition - transform.position).normalized * 7);
                    dualWieldOffset *= -1; //Alternates where gun is
                    lastShotTime = Time.time;
                }
            }
            
            
            
        }
    }

    public override void Die()
    {
        onDeathEvents.Invoke();
        foreach (GameObject obj in drops)
        {
            // Set the pickups parent to this things parent
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(transform.parent);
            Debug.Log("Dropped bounty");
        }
        base.Die();
    }

    // If boss is at 25% health or less, it is able to be dueled
    public bool isDuelReady()
    {
        //return true;
        return health <= maxHealth / 4;
    }

    public override void HandleBulletHit(Bullet b)
    {
        Debug.Log("boss handlebullethit");
        // Subtract health
        health -= b.damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        // Activate immunity
        if (health < 0)
        {
            health = 1; // Boss cannot die 
        }

        // Return to bullet that it hit an entity
        b.HandleEntityHit();
    }
}
