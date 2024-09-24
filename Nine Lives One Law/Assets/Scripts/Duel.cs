using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duel : MonoBehaviour
{

    private string[] duelKeys;
    private List<string> currentDuel;
    private bool duel;
    private float duelTime;


    // Start is called before the first frame update
    void Start()
    {
        duelKeys = new string[]{"W", "A", "S", "D", "L", "R"};
        currentDuel = new List<string>();
        duel = false;
        duelTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            startDuel(5,600);
        }

        if (duel)
        {
            updateDuel();
        }
    }

    void startDuel(int length, float time)
    {
        for(int i = 0; i < length; i++)
        {
            currentDuel.Add(duelKeys[Random.Range(0, duelKeys.Length)]);
        }
        duelTime = time;
        duel = true;
    }

    void updateDuel()
    {
        switch (currentDuel[0])
        {
            case "W":
                if (Input.GetKeyDown(KeyCode.W))
                {

                }
                break;
            case "A":
                break;
            case "S":
                break;
            case "D":
                break;
            case "L":
                break;
            case "R":
                break;
            default:
                break;
        }
        duelTime -= Time.deltaTime;
        if (duelTime < 0)
        {
            Debug.Log("DUEL FAIL");
            duel = false;
        }
    }
}
