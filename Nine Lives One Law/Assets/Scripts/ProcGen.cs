using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGen : MonoBehaviour
{
    private GameObject tileList; //GameObject that holds tiles
    private GameObject[,] map; //Internal view of map
    public GameObject[] genTiles; //Tiles for procgen
    public GameObject[] startTiles; //Tiles sherrif start on
    public GameObject[] bossTiles; //Tiles to fight boss on
    public GameObject wallTile; //Map borders
    public GameObject[] UDTiles; //Up down tiles
    private float length; //How long a tile is
    private List<GameObject> genTilesMod; //A list of the procgen tiles

    // Start is called before the first frame update
    void Start()
    {
        map = new GameObject[7, 7];
        length = wallTile.GetComponent<TileBase>().length;
        tileList = GameObject.Find("TileList");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Creates internal map
    void CreateMap()
    {
        map = new GameObject[7, 7];
        for (int i=0; i < 7; i++)
        { //Makes wall tiles on map borders
            map[0, i] = wallTile;
            map[6, i] = wallTile;
            map[i, 0] = wallTile;
            map[i, 6] = wallTile;
        }
        map[3, 1] = bossTiles[Random.Range(0, bossTiles.Length)]; //Top middle is boss tile
        map[3, 2] = UDTiles[Random.Range(0, UDTiles.Length)]; //In between is tiles that let you walk up and down
        map[3, 3] = UDTiles[Random.Range(0, UDTiles.Length)]; //In between is tiles that let you walk up and down
        map[3, 4] = UDTiles[Random.Range(0, UDTiles.Length)]; //In between is tiles that let you walk up and down
        map[3, 5] = startTiles[Random.Range(0, startTiles.Length)]; //Bottom middle is start tile
        for(int i = 5; i > 0; i--)
        { //Makes all other tiles on map
            GameObject newTile; //Tile to put on map
            int index = -1;
            makeMod();
            do
            { //Get a random genTile, if it's not compatible remove it from the list and try again
                if (index >= 0)
                {
                    genTilesMod.RemoveAt(index);
                }
                index = Random.Range(0, genTilesMod.Count);
                newTile = genTilesMod[index];
            } while (!Compatible(2, i, newTile));
            map[2, i] = newTile;
            index = -1;
            makeMod();
            do
            {
                if (index >= 0)
                {
                    genTilesMod.RemoveAt(index);
                }
                index = Random.Range(0, genTilesMod.Count);
                newTile = genTilesMod[index];
            } while (!Compatible(1, i, newTile));
            map[1, i] = newTile;
            index = -1;
            makeMod();
            do
            {
                if (index >= 0)
                {
                    genTilesMod.RemoveAt(index);
                }
                index = Random.Range(0, genTilesMod.Count);
                newTile = genTilesMod[index];
            } while (!Compatible(4, i, newTile));
            map[4, i] = newTile;
            index = -1;
            makeMod();
            do
            {
                if (index >= 0)
                {
                    genTilesMod.RemoveAt(index);
                }
                index = Random.Range(0, genTilesMod.Count);
                newTile = genTilesMod[index];
            } while (!Compatible(5, i, newTile));
            map[5, i] = newTile;
        }
    }

    //Creates the map in game
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

    //Checks if a tile is compatible with those around it using the open variables
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

    //Creates the genTiles list using the array
    void makeMod()
    {
        genTilesMod.Clear();
        for(int i=0; i < genTiles.Length; i++)
        {
            genTilesMod.Add(genTiles[i]);
        }
    }
}
