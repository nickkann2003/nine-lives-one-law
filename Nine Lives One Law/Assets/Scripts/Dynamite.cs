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

    private List<IHittableEntity> hits;

    //Variables
    public float damage;
    public float moveSpeed; // bullet speed
    public float moveTime; // How long stick should move
    public float stickLifetime; // How long stick should last
    public float explosionLifeTime; // How long explosion should last

    public Animator dynamiteAnimator;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        sprites = new SpriteRenderer[] { this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>(), this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>()};
        state = State.Move;
        bornTime = Time.time;

        targetTags = BulletManager.targetsDictionary[Bullets.EnemyBullet];

        rb.velocity = transform.up * moveSpeed; //Move bullet forward constantly

        dynamiteAnimator.SetTrigger("Launch");
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Move:
                if (Time.time - bornTime >= moveTime)
                {
                    StopMoving();
                }
                break;
            case State.Stay:
                if (Time.time - bornTime >= stickLifetime)
                {
                    Explode();
                }
                break;
            case State.Explode:
                break;
        }
    }

    private void StopMoving()
    {
        Debug.Log("stop");
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
        rb.simulated = false;
    }

    private void DestroyDynamite()
    {
        Debug.Log("destroy");
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
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
