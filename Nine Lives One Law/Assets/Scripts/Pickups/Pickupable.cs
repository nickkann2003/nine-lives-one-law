using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickupable : MonoBehaviour
{
    public bool pickupOnCollide = true;
    public bool deleteOnPickup = true;

    [SerializeField]
    private UnityEvent onPickupEvents;

    /// <summary>
    /// Picks up this object and performs code
    /// </summary>
    public void PickUp()
    {
        onPickupEvents.Invoke();

        StatsManager.instance.AddMoney(100);
        GameManager.Instance.UpdateGameState(GameManager.GameState.Menu);

        if(deleteOnPickup)
        {
            Destroy(gameObject);
        }
    }

    public void SpawnPickupAtLocation(Vector2 spawnLocation)
    {
        // Safety for picking up immediately upon appearing
        bool wasPickupable = pickupOnCollide;
        pickupOnCollide = false;

        // Sets position
        this.transform.position = spawnLocation;

        // Pickup safety
        pickupOnCollide = wasPickupable;
    }

    public void SpawnPickup()
    {
        // Safety for picking up immediately upon appearing
        bool wasPickupable = pickupOnCollide;
        pickupOnCollide = false;

        // Pickup safety
        pickupOnCollide = wasPickupable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If collided with player
        if(collision.gameObject.tag == "Player")
        {
            // If collectible, perform pickup
            if (pickupOnCollide)
            {
                PickUp();
            }
        }
    }
    
    // Runs when this is enabled
    private void OnEnable()
    {

    }
}
