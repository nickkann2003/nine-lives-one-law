using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelManager : MonoBehaviour 
{
    public GameObject DuelPanel;
    public GameObject DuelText;
    
    // Start is called before the first frame update

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
        DuelPanel.SetActive(state == GameManager.GameState.Duel);
        DuelText.SetActive(state == GameManager.GameState.Duel);
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
