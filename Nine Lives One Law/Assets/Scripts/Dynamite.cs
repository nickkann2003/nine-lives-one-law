using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.U2D;

public class Dynamite : MonoBehaviour
{
    enum State {Move,Stay,Explode};

    //Attributes
    private Rigidbody2D rb;
    private CircleCollider2D collider;
    private SpriteRenderer[] sprites;
    private State state;
    private float bornTime; // Time when bullet was initialized
    private List<string> targetTags;

    private List<IHittableEntity> hits = new List<IHittableEntity>();

    //Variables
    public float damage;
    public float moveSpeed; // bullet speed
    public float moveTime; // How long stick should move
    public float stickLifetime; // How long stick should last
    public float explosionLifeTime; // How long explosion should last

    private Vector3 pauseVelocity;
    private Boolean paused;

    public Animator dynamiteAnimator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
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

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    // Start is called before the first frame update
    void Start()
    {
       
        sprites = new SpriteRenderer[] { this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>(), this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>()};
        state = State.Move;
        bornTime = Time.time;
        if(targetTags.Count == 0)
            targetTags = BulletManager.targetsDictionary[Bullets.All];

        rb.velocity = transform.up * moveSpeed; //Move bullet forward constantly
        pauseVelocity = rb.velocity;
        dynamiteAnimator.SetTrigger("Launch");
        Debug.Log(stickLifetime);
    }


    //HOW TO FIX::::
    // you add +1 second every frame so it gets to like 2000 seconds 
    //So store the time it started freezing at and do math to find
    //out the total time left and change borntime to be that
    //far away from regular time.








    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Move:
                if (paused)
                {
                    bornTime += 1;
                }
                if (Time.time - bornTime >= moveTime)
                {
                    StopMoving();
                }
                break;
            case State.Stay:
                if (paused)
                {
                    bornTime += 1;
                }
                if (Time.time - bornTime >= stickLifetime)
                {
                    Explode();
                }
                break;
            case State.Explode:
                break;
        }
        
        Debug.Log("bornTime: " + bornTime + ". time: " + Time.time + ". stick lifetime: " + stickLifetime);
    }

    public void Set(List<string> tags, float damage)
    {
        targetTags = tags;
        this.damage = damage;
    }

    private void StopMoving()
    {
        //Debug.Log("stop");
        //rb.velocity = Vector2.zero;
        rb.drag = 2.5f;
        rb.angularDrag = 2f;
        state = State.Stay;
    }

    private void Explode()
    {
        Debug.Log("boom");
        state = State.Explode;

        dynamiteAnimator.SetTrigger("Explode");
        dynamiteAnimator.SetTrigger("Launch");
        rb.velocity = Vector2.zero;
        rb.freezeRotation = true;
        //rb.simulated = false;
    }

    private void DestroyDynamite()
    {
        //Debug.Log("destroy");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (targetTags.Contains(other.gameObject.tag))
        {
            IHittableEntity otherHit = other.GetComponent<IHittableEntity>();
            if (otherHit != null)
            {
                if (!hits.Contains(otherHit))
                {
                    otherHit.HandleDamageHit(damage);
                    hits.Add(otherHit);
                }
            }
        }
    }
}
