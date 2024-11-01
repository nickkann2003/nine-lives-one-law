using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyManager instance;

    public GameObject player;

    public GameObject enemyPrefab;
    public GameObject dynamiteEnemyPrefab;
    public GameObject boss1Prefab;
    public List<EnemyBase> enemies;
    public GameObject level1Prefab;

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
        { 
            GameObject enemyGO = Instantiate(enemyPrefab, this.transform);
            EnemyBase enemy = enemyGO.GetComponent<EnemyBase>();
            enemies.Add(enemy);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject enemyGO = Instantiate(dynamiteEnemyPrefab, this.transform);
            EnemyBase enemy = enemyGO.GetComponent<EnemyBase>();
            enemies.Add(enemy);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameObject enemyGO = Instantiate(boss1Prefab, this.transform);
            EnemyBase enemy = enemyGO.GetComponent<EnemyBase>();
            enemies.Add(enemy);
        }
        foreach (EnemyBase e in enemies)
        {
            e.targetEntityPosition = player.transform.position;
        }
    }

    public void LoadLevel()
    {
        Destroy(transform.GetChild(0).gameObject);

        // Only one level, so load level 1
        Instantiate(level1Prefab, this.transform);

        // Add to list
        EnemyBase[] children = gameObject.GetComponentsInChildren<EnemyBase>();
        foreach (EnemyBase child in children)
        {
            enemies.Add(child);
        }
    }
}
