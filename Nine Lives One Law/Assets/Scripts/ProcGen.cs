using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGen : MonoBehaviour
{
    private GameObject tileList;
    private GameObject[,] map;
    public GameObject[] genTiles;
    public GameObject[] startTiles;
    public GameObject[] bossTiles;
    public GameObject emptyTile;
    public GameObject[] UDTiles; //Up down tiles
    private float length;

    // Start is called before the first frame update
    void Start()
    {
        map = new GameObject[7, 7];
        length = emptyTile.GetComponent<TileBase>().length;
        tileList = GameObject.Find("TileList");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateMap()
    {
        map = new GameObject[7, 7];
        for (int i=0; i < 7; i++)
        {
            map[0, i] = emptyTile;
            map[6, i] = emptyTile;
            map[i, 0] = emptyTile;
            map[i, 6] = emptyTile;
        }
        map[3, 1] = bossTiles[Random.Range(0, bossTiles.Length)];
        map[3, 2] = UDTiles[Random.Range(0, UDTiles.Length)];
        map[3, 3] = UDTiles[Random.Range(0, UDTiles.Length)];
        map[3, 4] = UDTiles[Random.Range(0, UDTiles.Length)];
        map[3, 5] = startTiles[Random.Range(0, startTiles.Length)];
        for(int i = 5; i > 0; i--)
        {
            GameObject newTile;
            do
            {
                newTile = genTiles[Random.Range(0, genTiles.Length)];
            } while (Compatible(2,i,newTile));
            do
            {
                newTile = genTiles[Random.Range(0, genTiles.Length)];
            } while (Compatible(1, i, newTile));
            do
            {
                newTile = genTiles[Random.Range(0, genTiles.Length)];
            } while (Compatible(4, i, newTile));
            do
            {
                newTile = genTiles[Random.Range(0, genTiles.Length)];
            } while (Compatible(5, i, newTile));
        }
    }

    void Generate()
    {
        for(int i = -3; i <= 3; i++)
        {
            for (int j = -3; j <= 3; j++)
            {
                Instantiate(map[i, j], new Vector3(i * length, j * length, 0), transform.rotation, tileList.transform);
            }
        }
    }

    bool Compatible(int x, int y, GameObject tile)
    {
        if (map[x--, y] != null)
        { //Tile to left
            if (tile.GetComponent<TileBase>().leftOpen != !map[x--,y].GetComponent<TileBase>().rightOpen)
            {
                return false;
            }
        }
        if (map[x++, y] != null)
        { //Tile to right
            if (tile.GetComponent<TileBase>().rightOpen != !map[x++, y].GetComponent<TileBase>().leftOpen)
            {
                return false;
            }
        }
        if (map[x, y--] != null)
        { //Tile above
            if (tile.GetComponent<TileBase>().upOpen != !map[x, y--].GetComponent<TileBase>().downOpen)
            {
                return false;
            }
        }
        if (map[x, y++] != null)
        { //Tile below
            if (tile.GetComponent<TileBase>().downOpen != !map[x, y--].GetComponent<TileBase>().upOpen)
            {
                return false;
            }
        }
        return true;
    }
}
