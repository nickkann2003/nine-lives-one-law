using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGen : MonoBehaviour
{
    public GameObject tileList; //GameObject that holds tiles
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
        length = 15f;
        //tileList = GameObject.Find("ProcGenTileList");
        genTilesMod = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            CreateMap();
            Generate();
            printArr();
        }
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
                Debug.Log("2, " + i + " " + newTile.name);
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
                Debug.Log("1, " + i + " " + newTile.name);
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
                Debug.Log("4, " + i + " " + newTile.name);
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
                Debug.Log("5, " + i + " " + newTile.name);
            } while (!Compatible(5, i, newTile));
            map[5, i] = newTile;
        }
    }

    //Creates the map in game
    void Generate()
    {
        //Vector3 pos = new Vector3(-3 * length, -3 * length, 0);
        for (int y = -3; y <= 3; y++)
        {
            for (int x = -3; x <= 3; x++)
            {
                Instantiate(map[x+3, y+3], new Vector3(x * length, -y * length, 0), transform.rotation, tileList.transform);
                //Instantiate(map[i+3, j+3], tileList.transform);
                //tileList.transform.GetChild(tileList.transform.childCount - 1).transform.position = new Vector3(i * length, j * length, 0);
                //pos.y += length;
            }
            //pos.x += length;
            //pos.y -= length * 7;
        }
        Debug.Log("Generated");
        printMap();
    }

    //Checks if a tile is compatible with those around it using the open variables
    bool Compatible(int x, int y, GameObject tile)
    {
        //Instantiate(tile, tileList.transform);
        //tile = tileList.transform.GetChild(0).gameObject;
        Debug.Log("Compatible - x: " + x + " y: " + y + " tile:" + tile.name);
        printMap();
        if (map[x-1, y] != null)
        { //Tile to left
            Debug.Log("Left Go:" + map[x - 1, y].name);
            //Debug.Log("This tile left exit:"+ tile.GetComponent<LevelTile>().leftExit+ " Left tile right exit: ");
            //Instantiate(map[x - 1, y], tileList.transform);
            //GameObject compTile = tileList.transform.GetChild(1).gameObject;
            if (tile.GetComponent<LevelTile>().leftExit != map[x-1,y].GetComponent<LevelTile>().rightExit)
            {
                Debug.Log("Left Incompatible");
                //Destroy(tileList.transform.GetChild(1).gameObject);
                //Destroy(tileList.transform.GetChild(0).gameObject);
                return false;
            }
            //Destroy(tileList.transform.GetChild(1).gameObject);

            Debug.Log("Left Compatible");
        }
        if (map[x+1, y] != null)
        { //Tile to right
            Debug.Log("Right Go:" + map[x + 1, y].name);
            if (tile.GetComponent<LevelTile>().rightExit != map[x+1, y].GetComponent<LevelTile>().leftExit)
            {
                Debug.Log("Right Incompatible");
                //Destroy(tileList.transform.GetChild(0).gameObject);
                return false;
            }
            Debug.Log("Right Compatible");
        }
        if (map[x, y-1] != null)
        { //Tile above
            Debug.Log("Above Go:" + map[x, y-1].name);
            if (tile.GetComponent<LevelTile>().upExit != map[x, y-1].GetComponent<LevelTile>().downExit)
            {
                Debug.Log("Above Incompatible");
                //Destroy(tileList.transform.GetChild(0).gameObject);
                return false;
            }
            Debug.Log("Above Compatible");
        }
        if (map[x, y+1] != null)
        { //Tile below
            Debug.Log("Below Go:" + map[x, y+1].name);
            if (tile.GetComponent<LevelTile>().downExit != map[x, y+1].GetComponent<LevelTile>().upExit)
            {
                Debug.Log("Below Incompatible");
                //Destroy(tileList.transform.GetChild(0).gameObject);
                return false;
            }
            Debug.Log("Below Compatible");
        }
        Debug.Log("Compatible");
        //Destroy(tileList.transform.GetChild(0).gameObject);
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

    void printMap()
    {
        Debug.Log("print map start");
        string print = "";
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[x, y] == null)
                {
                    print += "null,";
                }
                else
                {
                    print += map[x, y].name+",";
                }
            }
            print += "\n";
        }
        Debug.Log(print);
    }

    void printArr()
    {
        Debug.Log("print arr start");
        string print = "";
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                print += "[" + x + "," + y + "]";
            }
            print += "\n";
        }
        Debug.Log(print);
    }
}
