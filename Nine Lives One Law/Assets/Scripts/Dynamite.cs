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
    private BoxCollider2D collider;
    private SpriteRenderer[] sprites;
    private State state;
    private float bornTime; // Time when bullet was initialized

    //Variables
    public float moveSpeed; // bullet speed
    public float moveTime; // How long stick should move
    public float stickLifetime; // How long stick should last
    public float explosionLifeTime; // How long explosion should last


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        sprites = new SpriteRenderer[] { this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>(), this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>()};
        state = State.Move;
        bornTime = Time.time;

        rb.velocity = transform.up * moveSpeed; //Move bullet forward constantly
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Move:
                if (Time.time - bornTime >= moveTime)
                {
                    Debug.Log("stop");
                    rb.velocity = Vector2.zero;
                    state = State.Stay;
                }
                break;
            case State.Stay:
                if (Time.time - bornTime >= stickLifetime)
                {
                    Debug.Log("boom");
                    sprites[0].enabled = false;
                    sprites[1].enabled = true;
                    state= State.Explode;
                }
                break;
            case State.Explode:
                if (Time.time - bornTime >= explosionLifeTime + stickLifetime)
                {
                    Debug.Log("destroy");
                    Destroy(gameObject);
                }
                break;
        }
    }
}
