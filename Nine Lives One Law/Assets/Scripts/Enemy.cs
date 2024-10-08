using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHittableEntity
{
    //Attributes
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    //[SerializeField]
    //private Animator playerAnimator;
    private float lastShotTime; // Last time enemy shot

    // Game Objects
    public GameObject player;
    public GameObject bullet;
    public GameObject bulletList;

    // Variables
    public float range;
    public float moveSpeed;
    public float shootCooldown;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        bulletList = GameObject.Find("BulletList");
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        lastShotTime = -shootCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = player.transform.position - transform.position; //Look at player
        if (Vector3.Distance(transform.position, player.transform.position) > range)
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

    public void HandleBulletHit(Bullet b)
    {
        // Subtract health equal to bullet damage
        // Activate immunity

        // Return to bullet that it hit an entity
        b.HandleEntityHit();
    }
}
