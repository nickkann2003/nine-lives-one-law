using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    //Attributes
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    [SerializeField]
    private Animator playerAnimator;

    //Controls
    public PlayerControls playerControls;
    private InputAction move;
    //private InputAction look;
    private InputAction fire;

    //Variables
    public float moveSpeed;
    private bool isActive;

    private void Awake()
    {
        playerControls = new PlayerControls();
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        isActive = false;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            MovePlayer();
            FaceMouse();
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

    // Makes the player fire a bullet
    void Fire(InputAction.CallbackContext context)
    {
       Debug.Log("Fire!");
    }

    // Enables player controls
    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;

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
