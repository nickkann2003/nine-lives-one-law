using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //This lets you grab it from anywhere
    public static GameManager Instance;
    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.Menu);
    }

    // Update is called once per frame
    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch (newState)
        {
            case GameState.Menu:
                break;
            case GameState.Gameplay:
                break;
            case GameState.Duel:
                break;
            default:
                break;

        }

        OnGameStateChanged?.Invoke(newState);
    }

    public enum GameState
{
    Menu,
    Gameplay,
    Duel
}
}