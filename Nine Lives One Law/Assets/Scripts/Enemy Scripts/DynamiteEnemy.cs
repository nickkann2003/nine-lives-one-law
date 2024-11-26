using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteEnemy : EnemyBase, IHittableEntity
{
    //[SerializeField]
    //private Animator playerAnimator;
    private float lastShotTime; // Last time enemy shot

    // Game Objects
    public GameObject dynamite;

    // Variables
    public float range;
    public float moveSpeed;
    public float shootCooldown;
    public float damage = 3f;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        lastShotTime = Time.time;
    }

    // Update is called once per frame
    new void Update()
    {
        transform.up = targetEntityPosition - transform.position; //Look at player
        base.Update();
    }

    public override void Wander()
    {
        // Need to get in range of Dynamite Enemy before attacking
        
        if (Vector3.Distance(transform.position, targetEntityPosition) < range * 2.5)
        {
            attackingTarget = true;
        }
    }

    public override void AttackTarget()
    {
        if (isActive)
        {
            //transform.up = targetEntityPosition - transform.position; //Look at player
            if (Vector3.Distance(transform.position, targetEntityPosition) > range)
            { // If out of range, move forward
                rb.velocity = transform.up * moveSpeed;
                //playerAnimator.SetBool("Moving", true);
            }
            else
            { // If in range, do not move
                rb.velocity = Vector2.zero;
                //playerAnimator.SetBool("Moving", false);
            }
            if (Time.time - lastShotTime >= shootCooldown)
            { // If shot cooldown is up, shoot
                Vector3 bulletSpawn = transform.position;
                bulletSpawn += transform.up * 0.8f; // bullet offset so it spawns on the gun
                GameObject dyn = Instantiate(dynamite, bulletSpawn, transform.rotation, bulletList.transform);
                Dynamite d = dyn.GetComponent<Dynamite>();
                d.Set(BulletManager.targetsDictionary[Bullets.All], damage);

                // Update last shot time
                lastShotTime = Time.time;
            }
        }
    }
}
