using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Duel : MonoBehaviour
{
    public DuelKeyManager keyManager;

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
        if (Input.GetKeyDown(KeyCode.M) && !duel)
        { //If M is pressed and there is no duel, start duel
            startDuel(5,5);
        }

        if (duel)
        { //If there is a duel, update duel
            updateDuel();
        }
    }

    //Starts a duel by making a list of inputs
    void startDuel(int length, float time)
    {
        keyManager.gameObject.SetActive(true);
        currentDuel.Clear();
        for(int i = 0; i < length; i++)
        { //Fills list of inputs, WASD, L-Click, R-Click
            currentDuel.Add(duelKeys[Random.Range(0, duelKeys.Length)]);
        }
        foreach (string s in currentDuel)
        {
            keyManager.AddKey(s);
        }
        duelTime = time; //How long the user has for the duel
        duel = true;
        keyManager.SetStartingPositions();
        PrintDuel(); //Prints full inputs
    }

    //Updates the duel by checking for correct input, lowering duel timer, checks for win or lose
    void updateDuel()
    {
        //Checks the first input in list, checks if player pressed it
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
        { //If enough time has passed, duel failed
            Debug.Log("DUEL FAIL");
            duel = false;
            keyManager.EndDuel();
        }
    }

    //Updates for if the player pressed the correct key in the duel
    void PressDuelKey()
    {
        currentDuel.RemoveAt(0); //Removes the first input in the list
        keyManager.IncrementKeys();
        if (currentDuel.Count == 0)
        { //If there is 0 inputs left, the player wins and the duel ends
            duel = false;
            keyManager.EndDuel();
            Debug.Log("WIN!");
        }
        else
        { //If there are more inputs, print the remaining inputs
            PrintDuel();
        }
    }

    //Prints all the inputs in the duel
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
