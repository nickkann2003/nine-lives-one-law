using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGen : MonoBehaviour
{
    private TileBase[,] map;
    public TileBase[] startTiles;
    public TileBase[] bossTiles;
    public TileBase emptyTile;
    public TileBase[] UDTiles; //Up down tiles

    // Start is called before the first frame update
    void Start()
    {
        map = new TileBase[7, 7];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateMap()
    {
        map = new TileBase[7, 7];
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
            map[2, i] = emptyTile;
            map[1, i] = emptyTile;
        }
    }

    void Generate()
    {

    }

    void Compatible(int x, int y, TileBase tile)
    {
        if (map[x--, y] != null)
        {
            if (tile.leftOpen)
            {

            }
        }
    }
}
