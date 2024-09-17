using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //MovePlayer();
        FaceMouse();
    }

    // Moves the player depending on move speed variable
    void MovePlayer()
    {
        Vector3 newPos = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            newPos.y += moveSpeed / 5;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newPos.y -= moveSpeed / 5;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newPos.x -= moveSpeed / 5;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newPos.x += moveSpeed / 5;
        }

        transform.position = newPos;
    }

    // Makes the player rotation face the mouse
    void FaceMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float AngleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad - 90;
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }
}
