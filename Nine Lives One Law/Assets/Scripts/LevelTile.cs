using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class LevelTile : MonoBehaviour
{
    public static float tileSize = 15;
    public static int tileMargins = 10;

    [Header("Tile Position")]
    public int xPos = 0;
    public int yPos = 0;

    [Header("Tile Exits")]
    public bool upExit;
    public bool rightExit;
    public bool downExit;
    public bool leftExit;

    [Header("Generation")]
    public bool makeEnemies;
    public int makeBoss;

    public GameObject boss1;
    public GameObject[] enemies;
    public GameObject enemyList;

    // Start is called before the first frame update
    void Start()
    {
        enemyList = GameObject.Find("EnemyList");
        //transform.position = new Vector3(xPos * tileSize, yPos * tileSize, transform.position.z);
        if (makeEnemies)
        {
            for(int i=0;i<Random.Range(0, 4); i++)
            {
                Instantiate(enemies[Random.Range(0,enemies.Length)],transform.position + new Vector3(Random.Range(-7, 8), Random.Range(-7, 8),1),transform.rotation,enemyList.transform);
                //enemyList.GetComponent<EnemyManager>().enemies.Add();
            }
        }
        if (makeBoss > 0)
        { //Only one boss right now so this code works, will need to update if we have multiple bosses
            Instantiate(boss1,transform.position,transform.rotation,enemyList.transform);
            //enemyList.GetComponent<EnemyManager>().enemies.Add();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        //transform.position = new Vector3(xPos * tileSize, yPos * tileSize, transform.position.z);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(tileSize, tileSize, 1));
        if (upExit)
        {
            Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + (tileSize/2f) + (tileMargins/4f), 1), new Vector3(tileMargins/2f, tileMargins/2f));
        }
        if (rightExit)
        {
            Gizmos.DrawWireCube(new Vector3(transform.position.x + (tileSize / 2f) + (tileMargins / 4f), transform.position.y, 1), new Vector3(tileMargins / 2f, tileMargins / 2f));
        }
        if (downExit)
        {
            Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y - (tileSize / 2f) - (tileMargins / 4f), 1), new Vector3(tileMargins / 2f, tileMargins / 2f));
        }
        if (leftExit)
        {
            Gizmos.DrawWireCube(new Vector3(transform.position.x - (tileSize / 2f) - (tileMargins / 4f), transform.position.y, 1), new Vector3(tileMargins / 2f, tileMargins / 2f));
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(tileSize + tileMargins, tileSize + tileMargins, 1));
    }
}
