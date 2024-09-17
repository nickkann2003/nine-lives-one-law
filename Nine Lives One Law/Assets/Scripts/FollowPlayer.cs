using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Sets object position to the same as the player's. Further back on the Z-Axis for camera
        transform.position = player.transform.position + new Vector3(0, 0, -10);
    }
}
