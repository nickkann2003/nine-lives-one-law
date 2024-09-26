using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
            startDuel(5,5);
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
        PrintDuel();
    }

    void updateDuel()
    {
        switch (currentDuel[0])
        {
            case "W":
                if (Input.GetKeyDown(KeyCode.W))
                {
                    PressDuelKey();
                }
                break;
            case "A":
                if (Input.GetKeyDown(KeyCode.A))
                {
                    PressDuelKey();
                }
                break;
            case "S":
                if (Input.GetKeyDown(KeyCode.S))
                {
                    PressDuelKey();
                }
                break;
            case "D":
                if (Input.GetKeyDown(KeyCode.D))
                {
                    PressDuelKey();
                }
                break;
            case "L":
                if (Input.GetMouseButtonDown(0))
                {
                    PressDuelKey();
                }
                break;
            case "R":
                if (Input.GetMouseButtonDown(1))
                {
                    PressDuelKey();
                }
                break;
            default:
                break;
        }
        duelTime -= 1 * Time.deltaTime;
        if (duelTime<0)
        {
            Debug.Log("DUEL FAIL");
            duel = false;
        }
    }

    void PressDuelKey()
    {
        currentDuel.RemoveAt(0);
        if (currentDuel.Count == 0)
        {
            duel = false;
            Debug.Log("WIN!");
        }
        else
        {
            PrintDuel();
        }
    }

    void PrintDuel()
    {
        string print = "";
        for (int i = 0; i < currentDuel.Count; i++)
        {
            print+=currentDuel[i];
        }
        Debug.Log(print);
    }
}
