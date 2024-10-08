using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyManager instance;

    public GameObject player;

    public GameObject enemyPrefab;
    public List<Enemy> enemies;

    // On awake, set singletone
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        { //If M is pressed and there is no duel, start duel
            GameObject enemyGO = Instantiate(enemyPrefab, this.transform);
            Enemy enemy = enemyGO.GetComponent<Enemy>();
            enemies.Add(enemy);
        }
        foreach (Enemy e in enemies)
        {
            e.targetEntityPosition = player.transform.position;
        }
    }
}
