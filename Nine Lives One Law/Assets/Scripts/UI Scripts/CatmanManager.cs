using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatmanManager : MonoBehaviour

    
{
    public GameObject CatMan;
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameManager.GameState state)
    {
        CatMan.SetActive(state == GameManager.GameState.Menu);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
