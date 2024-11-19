using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IHittableEntity
{

    //Attributes
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    protected FloatingHealthBar healthBar;
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

    public float damage = 1.0f;

    //Game Objects
    public GameObject bullet; // bullet prefab
    public GameObject bulletList; // object that holds bullets

    //Variables
    public float moveSpeed; // Player speed
    public float shootCooldown; // How often player can shoot
    public float rollCooldown; // How often player can roll
    public float rollTime; // How long player roll
    public float rollSpeed; // Speed multiplier during roll
    public Vector2 pauseMovement; //Movement stored while paused

    private float health;
    private float maxHealthFloat;
    public int maxHealth;
    private float immunityTime = 0.5f;
    private float iTimeLeft = 0f;
    private bool immune = false;

    private int maxBullets = 6;
    private int currentBullets = 6;
    private float reloadTime = 1f;
    private float currentReloadTime = 1f;
    private bool reloading = false;
    public Animator ammoAnimator;

    private Vector3 startingPos = Vector3.zero;

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
        maxHealthFloat = maxHealth;
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
            rb.velocity = pauseMovement;
            rb.constraints = RigidbodyConstraints2D.None;

        }
        else
        {
            isActive = false;
            OnDisable();
            pauseMovement = rb.velocity;
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        sprite = this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        health = maxHealth;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            MovePlayer();
            if(!isMidRoll)
                FaceMouse();
            if (isMidRoll)
                FaceMovementDirection();
            Shoot();
            Roll();
            if (immune)
            {
                iTimeLeft -= Time.deltaTime;
                if(iTimeLeft <= 0)
                {
                    immune = false;
                    sprite.color = Color.white;
                }
            }
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

    void FaceMovementDirection()
    {
        if (isActive)
        {
            Vector3 dirPos = new Vector3(transform.position.x + rb.velocity.x, transform.position.y + rb.velocity.y, transform.position.z);
            float AngleRad = Mathf.Atan2(dirPos.y - transform.position.y, dirPos.x - transform.position.x);
            float AngleDeg = (180 / Mathf.PI) * AngleRad - 90;
            this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        }
    }

    // Makes the player dodge roll
    void Roll()
    {
        if(tryingToRoll && Time.time - lastRollTime >= rollCooldown && !isMidRoll)
        { // Makes player roll if trying to roll and cooldown is up
            sprite.color = new Color(0.9f, 0.9f, 0.9f); //Color shifts for visual clarity, likely temp
            moveSpeed *= rollSpeed; //Speeds up player movement during roll
            isMidRoll = true;
            //immunityTime = rollTime;
            lastRollTime = Time.time; // Update last roll time
            playerAnimator.SetBool("Rolling", true);
            StatsManager.instance.dodgeRollCount++;
        }
        if (isMidRoll && Time.time - lastRollTime >= rollTime)
        { // Stops player roll once the roll time is up
            sprite.color = Color.white; //Goes back to normal color
            moveSpeed /= rollSpeed; //Slows player movement down to normal
            isMidRoll = false;
            playerAnimator.SetBool("Rolling", false);
        }
    }

    // Makes the player fire a bullet
    void Shoot()
    {
        if (!reloading)
        {
            if (tryingToShoot && !isMidRoll && Time.time - lastShotTime >= shootCooldown)
            { // Makes bullet if trying to shoot and not rolling and cooldown is up
                BulletManager.instance.CreateBullet(Bullets.PlayerBullet, damage, transform.position + (transform.up * 0.8f), transform.up * 12f);
                lastShotTime = Time.time; // Update last shot time
                currentBullets -= 1;
                StatsManager.instance.bulletsFired++;
                ammoAnimator.SetTrigger("shoot");
                ammoAnimator.SetInteger("ammo", currentBullets);
                if (currentBullets <= 0)
                {
                    reloading = true;
                    currentReloadTime = reloadTime;
                    ammoAnimator.SetTrigger("reload");
                }
            }
        }
        else
        {
            currentReloadTime -= Time.deltaTime;
            if (currentReloadTime <= 0)
            {
                reloading = false;
                currentBullets = maxBullets;
                ammoAnimator.SetInteger("ammo", currentBullets);
            }
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
        if(iTimeLeft <= 0 && !isMidRoll)
        {
            // Subtract health equal to bullet damage
            health -= b.damage;
            // Activate immunity
            iTimeLeft = immunityTime;
            immune = true;
            sprite.color = Color.grey;
            CheckDead();
        }

        // Return to bullet that it hit an entity
        b.HandleEntityHit();
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    public void HandleDamageHit(float damage)
    {
        if (iTimeLeft <= 0 && !isMidRoll)
        {
            // Subtract health equal to bullet damage
            health -= damage;
            // Activate immunity
            iTimeLeft = immunityTime;
            immune = true;
            sprite.color = Color.grey;
            CheckDead();
        }

        healthBar.UpdateHealthBar(health, maxHealth);
    }

    private void CheckDead()
    {
        if(health <= 0)
        {
            // Perform death logic here
            StatsManager.instance.deathCount++;
            //TEMP
            health = maxHealth;
        }
    }

    public void SetPosition()
    {
        transform.position = startingPos;
    }

    // Upgrades
    public void UpgradeMaxHealth(float val)
    {
        float healthPercent = (float)health / (float)maxHealth;
        maxHealthFloat += val;
        maxHealth = (int)maxHealthFloat;
        health = (int)(healthPercent * (float)maxHealth);
    }

    public void UpgradeSpeed(float val)
    {
        moveSpeed = moveSpeed * val;
    }

    public void UpgradeDamage(float val)
    {
        damage += val;
    }

    public void UpgradeFireRate(float val)
    {
        shootCooldown = shootCooldown * (1f / val);
    }

    public void UpgradeRollCooldown(float val)
    {
        rollCooldown = rollCooldown * (1f / val);
    }

    public void UpgradeRollSpeed(float val) 
    {
        rollSpeed = rollSpeed * (val);
    }

    public void UpgradeRollDuration(float val)
    {
        rollTime = rollTime * val;
    }
}
