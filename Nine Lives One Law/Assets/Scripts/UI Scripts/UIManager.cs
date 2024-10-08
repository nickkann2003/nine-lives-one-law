using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum UIState
    {
        StatsMenu,
        UpgradesMenu,
        StartRunMenu,
        MainMenu,
        GameMenu
    }

    // Singleton pattern
    public static UIManager instance;

    [Header("References")]
    public GameObject StatsMenu;
    public GameObject UpgradesMenu;
    public GameObject StartRunMenu;
    public GameObject MainMenu;
    public GameObject GameMenu;

    [Header("UI Objects")]
    public List<GameObject> StatsObjects = new List<GameObject>();
    public List<GameObject> UpgradesObjects = new List<GameObject>();
    public List<GameObject> StartRunObjects = new List<GameObject>();
    public List<GameObject> MainMenuObjects = new List<GameObject>();
    public List<GameObject> InGameObjects = new List<GameObject>();


    public GameObject OptionsMenu;
    private float previousTimeScale = 0f;
    private bool options; // Toggle for options, its not a state since it overlays other states
    public Duel DuelInstance;

    public Fade fadeRef;

    [Header("Options")]
    public float transitionTime = 1f;

    private UIState currentState;

    // On awake, initialize singleton
    // Subscribe this menu to the game manager
    private void Awake()
    {
        instance = this;
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    //Remove subscription on destruction to avoid memory leaks
    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    //Activate the main menu when the game manager state is set to menu
    private void GameManager_OnGameStateChanged(GameManager.GameState state)
    {
        if(state == GameManager.GameState.Menu)
        {
            StartCoroutine(OpenMenu(UIState.MainMenu));
        }
    }

    // On start, enable Main Menu
    private void Start()
    {
        currentState = UIState.MainMenu;
        //MainMenu.SetActive(true);
    }

    private void Update()
    {
        // Check for escape key, open options if escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (options)
            {
                CloseOptions();
            }
            else
            {
                OpenOptions();
            }
        }
    }

    // ------------------ Functions -----------------
    public IEnumerator OpenMenu(UIState newState)
    {

        // Close options if open
        if (options)
        {
            CloseOptions();
        }

        // Start the fade
        fadeRef.StartFade(transitionTime);

        yield return new WaitForSeconds(transitionTime / 2f);



        // Close previous menu
        switch (currentState)
        {
            case UIState.StatsMenu:
                CloseMenu(StatsMenu);
                deActivateAll(StatsObjects);
                break;
            case UIState.UpgradesMenu:
                CloseMenu(UpgradesMenu);
                deActivateAll(UpgradesObjects);
                break;
            case UIState.StartRunMenu:
                CloseMenu(StartRunMenu);
                deActivateAll(StartRunObjects);
                break;
            case UIState.MainMenu:
                CloseMenu(MainMenu);
                deActivateAll(MainMenuObjects);
                break;
            case UIState.GameMenu:
                CloseMenu(GameMenu);
                deActivateAll(InGameObjects);
                break;
        }


        // Set current state
        currentState = newState;
        
        // Open new menu
        switch (currentState)
        {
            case UIState.StatsMenu:
                OpenMenu(StatsMenu); 
                activateAll(StatsObjects);
                break;
            case UIState.UpgradesMenu:
                OpenMenu(UpgradesMenu);                
                activateAll(UpgradesObjects);
                break;
            case UIState.StartRunMenu:
                OpenMenu(StartRunMenu);                
                activateAll(StartRunObjects);
                break;
            case UIState.MainMenu:
                OpenMenu(MainMenu);                
                activateAll(MainMenuObjects);
                break;
            case UIState.GameMenu:
                OpenMenu(GameMenu);                
                activateAll(InGameObjects);
                break;
        }
    }

    public void OpenMenu(int i)
    {
        StartCoroutine(OpenMenu(IntToUIState(i)));
        if (i == 4)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Gameplay);
        }
        else if (i == 3)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Menu);
        }
    }

    public void OpenOptions()
    {
        previousTimeScale = Time.timeScale;
        options = true;
        Time.timeScale = 0;
        OptionsMenu.SetActive(options);
    }

    public void CloseOptions()
    {
        options = false;
        Time.timeScale = previousTimeScale;
        OptionsMenu.SetActive(options);
    }

    private void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    private void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    private void Upgrade()
    {
        DuelInstance.duelTimePowerUp += 1;
    }

    private UIState IntToUIState(int i)
    {
        return (UIState)i;
    }

    private void activateAll(List<GameObject> list)
    {
        foreach (GameObject go in list)
        {
            go.SetActive(true);
        }
    }
    private void deActivateAll(List<GameObject> list)
    {
        foreach (GameObject go in list)
        {
            go.SetActive(false);
        }
    }
}
