using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IHittableEntity
{

    //Attributes
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private SpriteRenderer sprite;
    [SerializeField]
    private Animator playerAnimator;

    //Controls
    public PlayerControls playerControls;
    private InputAction move;
    //private InputAction look;
    private InputAction fire;
    private InputAction roll;
    private bool isActive; // If player is active
    private bool tryingToShoot; // If player is trying to shoot
    private bool tryingToRoll; // If player is trying to roll
    private bool isMidRoll; // If player object is mid-roll
    private float lastShotTime; // Last time player shot
    private float lastRollTime; // Last time player rolled

    //Game Objects
    public GameObject bullet; // bullet prefab
    public GameObject bulletList; // object that holds bullets

    //Variables
    public float moveSpeed; // Player speed
    public float shootCooldown; // How often player can shoot
    public float rollCooldown; // How often player can roll
    public float rollTime; // How long player roll
    public float rollSpeed; // Speed multiplier during roll

    private void Awake()
    {
        playerControls = new PlayerControls();
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        isActive = false;
        tryingToShoot = false;
        tryingToRoll = false;
        isMidRoll = false;
        lastShotTime = -shootCooldown;
        lastRollTime = -rollCooldown;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Gameplay)
        {
            isActive = true;
            OnEnable();
            rb.isKinematic = false;
        }
        else
        {
            isActive = false;
            OnDisable();
            rb.isKinematic = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        sprite = this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            MovePlayer();
            FaceMouse();
            Shoot();
            Roll();
        }
    }

    // Moves the player depending on move speed variable, tells animator if moving
    void MovePlayer()
    {
        Vector2 moveDirection = move.ReadValue<Vector2>();
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        if(rb.velocity.magnitude == 0 && isMidRoll)
        {
            rb.velocity = (Vector2)transform.up * moveSpeed;
        }

        if (rb.velocity.magnitude > 0)
        {
            playerAnimator.SetBool("Moving", true);
        }
        else
        {
            playerAnimator.SetBool("Moving", false);
        }
    }

    // Makes the player rotation face the mouse
    void FaceMouse()
    {
        if (isActive)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float AngleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
            float AngleDeg = (180 / Mathf.PI) * AngleRad - 90;
            this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        }
    }

    // Makes the player dodge roll
    void Roll()
    {
        if(tryingToRoll && Time.time - lastRollTime >= rollCooldown)
        { // Makes player roll if trying to roll and cooldown is up
            sprite.color = Color.cyan; //Color shifts for visual clarity, likely temp
            moveSpeed *= rollSpeed; //Speeds up player movement during roll
            isMidRoll = true;
            lastRollTime = Time.time; // Update last roll time
        }
        if(isMidRoll && Time.time - lastRollTime >= rollTime)
        { // Stops player roll once the roll time is up
            sprite.color = Color.white; //Goes back to normal color
            moveSpeed /= rollSpeed; //Slows player movement down to normal
            isMidRoll = false;
        }
    }

    // Makes the player fire a bullet
    void Shoot()
    {
        if (tryingToShoot && !isMidRoll && Time.time - lastShotTime >= shootCooldown)
        { // Makes bullet if trying to shoot and not rolling and cooldown is up
            BulletManager.instance.CreateBullet(Bullets.PlayerBullet, 1.0f, transform.position + (transform.up * 0.8f), transform.up * 12f);
            lastShotTime = Time.time; // Update last shot time
        }
    }

    // Left click down, activate shooting
    void StartShooting(InputAction.CallbackContext context)
    {
        tryingToShoot = true;
    }

    // Left click up, deactive shooting
    void StopShooting(InputAction.CallbackContext context)
    {
        tryingToShoot = false;
    }

    // Right click down, activate rolling
    void StartRolling(InputAction.CallbackContext context)
    {
        tryingToRoll = true;
    }

    // Right click up, deactive rolling
    void StopRolling(InputAction.CallbackContext context)
    {
        tryingToRoll = false;
    }

    // Enables player controls
    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += StartShooting;
        fire.canceled += StopShooting;

        roll = playerControls.Player.Roll;
        roll.Enable();
        roll.performed += StartRolling;
        roll.canceled += StopRolling;

        //look = playerControls.Player.Look;
        //look.Enable();
    }


    // Disables player controls
    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        //look.Disable();
    }


    public void HandleBulletHit(Bullet b)
    {
        // Subtract health equal to bullet damage
        // Activate immunity

        // Return to bullet that it hit an entity
        b.HandleEntityHit();
    }

    public void HandleDamageHit(float damage)
    {
        // Subtract player health
        // Activate immunity
    }
}
