using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEditor.Timeline;
using UnityEngine;

public class TileTempGen : MonoBehaviour
{
    // BOOTLEG EDITOR SCRIPT
    // DO NOT just edit this into a proper gen script
    // This is temporary and used for generating a layout quickly and poorly

    public int width;
    public int height;
    public float tileWidth;
    public float tileHeight;
    public List<GameObject> basePrefabs = new List<GameObject>();
    public List<GameObject> decorations = new List<GameObject>();

    private void OnDrawGizmosSelected()
    {
        if (gameObject.transform.childCount <= 0)
        {
            // Perform gen code here
            for(int i = 0; i < width; i++) { 
                for(int j = 0; j < height; j++)
                {
                    GameObject o = Instantiate(basePrefabs[Random.Range(0, basePrefabs.Count)], transform);
                    o.transform.position = new Vector3(i * tileWidth, j * tileHeight);
                }
            }
        }
    }
}
