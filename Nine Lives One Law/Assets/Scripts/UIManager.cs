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

    public GameObject OptionsMenu;
    private float previousTimeScale = 0f;
    private bool options; // Toggle for options, its not a state since it overlays other states

    public Fade fadeRef;

    [Header("Options")]
    public float transitionTime = 1f;

    private UIState currentState;

    // On awake, initialize singleton
    private void Awake()
    {
        instance = this;
    }

    // On start, enable Main Menu
    private void Start()
    {
        currentState = UIState.MainMenu;
        MainMenu.SetActive(true);
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
                break;
            case UIState.UpgradesMenu:
                CloseMenu(UpgradesMenu);
                break;
            case UIState.StartRunMenu:
                CloseMenu(StartRunMenu);
                break;
            case UIState.MainMenu:
                CloseMenu(MainMenu);
                break;
            case UIState.GameMenu:
                CloseMenu(GameMenu);
                break;
        }


        // Set current state
        currentState = newState;
        
        // Open new menu
        switch (currentState)
        {
            case UIState.StatsMenu:
                OpenMenu(StatsMenu);
                break;
            case UIState.UpgradesMenu:
                OpenMenu(UpgradesMenu);
                break;
            case UIState.StartRunMenu:
                OpenMenu(StartRunMenu);
                break;
            case UIState.MainMenu:
                OpenMenu(MainMenu);
                break;
            case UIState.GameMenu:
                OpenMenu(GameMenu);
                break;
        }
    }

    public void OpenMenu(int i)
    {
        StartCoroutine(OpenMenu(IntToUIState(i)));
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

    private UIState IntToUIState(int i)
    {
        return (UIState)i;
    }
}
