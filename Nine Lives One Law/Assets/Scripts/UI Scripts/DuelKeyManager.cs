using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelKeyManager : MonoBehaviour
{
    public GameObject duelKeyPrefab;

    public Vector2[] keyPosAnchors;
    public int centerKeyIndex;
    private int currentIndex;

    public List<DuelKey> keys;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementKeys()
    {
        keys[currentIndex].hitKey();
        try
        {
            keys[currentIndex - 2].resetKey();
        }
        catch { }

        try
        {
            keys[currentIndex + 3].displayKey();
        }
        catch { }

        currentIndex++;

        for(int i = 0; i < 5; i++)
        {
            // Temp code, does not account for different amount of anchors, should be adjusted if anchors changes
            int currentKey = currentIndex + i - 2;
            keys[currentKey].setKeyPosition(keyPosAnchors[i]);
        }
    }

    // Add a key to the list of keys
    public void AddKey(char key)
    {
        GameObject k = Instantiate(duelKeyPrefab);
        keys.Add(k.GetComponent<DuelKey>());
    }

    // Adding a key with a string uses the first char in that string
    public void AddKey(string key)
    {
        AddKey(key.ToCharArray()[0]);
    }

    private void OnDrawGizmosSelected()
    {
        Canvas c = GetComponentInParent<Canvas>();
        Gizmos.matrix = c.transform.localToWorldMatrix;
        
        Color[] colorsArray = { Color.grey, Color.red, Color.yellow, Color.green, Color.blue, Color.magenta};
        int colorIndex = 0;
        
        foreach (Vector2 pos in keyPosAnchors)
        {
            Gizmos.color = colorsArray[colorIndex];
            Gizmos.DrawSphere(new Vector3(pos.x, pos.y, 0), 10);
            colorIndex = (colorIndex + 1) % 5;
        }
    }
}
