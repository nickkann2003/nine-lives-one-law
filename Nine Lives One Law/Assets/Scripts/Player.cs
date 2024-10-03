using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    //Attributes
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    //private SpriteRenderer sprite;
    [SerializeField]
    private Animator playerAnimator;

    //Controls
    public PlayerControls playerControls;
    private InputAction move;
    //private InputAction look;
    private InputAction fire;
    private InputAction roll;
    private bool isActive; // If player is active
    private bool isShooting; // If player is shooting
    private float lastShotTime; // Last time player shot
    private float lastRollTime; // Last time player rolled

    //Game Objects
    public GameObject bullet; // bullet prefab
    public GameObject bulletList; // object that holds bullets

    //Variables
    public float moveSpeed; // Player speed
    public float shootCooldown; // How often player can shoot
    public float rollTime; // How long player roll
    public float rollSpeed; // Speed multiplier during roll

    private void Awake()
    {
        playerControls = new PlayerControls();
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        isActive = false;
        isShooting = false;
        lastShotTime = -shootCooldown;
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
        }
        else
        {
            isActive = false;
            OnDisable();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        //sprite = GetComponent<SpriteRenderer>();
        //sprite.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            MovePlayer();
            FaceMouse();
            Shoot();
        }
    }

    // Moves the player depending on move speed variable, tells animator if moving
    void MovePlayer()
    {
        Vector2 moveDirection = move.ReadValue<Vector2>();
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float AngleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad - 90;
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }

    void StartRoll(InputAction.CallbackContext context)
    {

    }

    // Makes the player fire a bullet
    void Shoot()
    {
        if (isShooting && Time.time - lastShotTime >= shootCooldown)
        { // Instantiate bullets at the object's position if shooting and enough time since last shot has passed
            Vector3 bulletSpawn = transform.position;
            bulletSpawn += transform.up * 0.8f; // bullet offset so it spawns on the gun
            Instantiate(bullet, bulletSpawn, transform.rotation, bulletList.transform);
            // Update last shot time
            lastShotTime = Time.time;
        }
    }

    // Left click down, activate shooting
    void StartShooting(InputAction.CallbackContext context)
    {
        isShooting = true;
    }

    // Left click up, deactive shooting
    void StopShooting(InputAction.CallbackContext context)
    {
        isShooting = false;
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
        roll.performed += StartRoll;

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
}
