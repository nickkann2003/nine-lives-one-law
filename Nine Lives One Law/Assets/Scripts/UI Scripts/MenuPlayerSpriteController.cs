using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerSpriteController : MonoBehaviour
{
    public GameObject stopSprite;
    public GameObject leftSprite;
    public GameObject rightSprite;
    public RectTransform spriteParentTransform;

    private float stopTime = 0f;
    private bool stopping = false;

    private Vector3 velocity = Vector3.zero;
    private float speed = 400f;

    private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = spriteParentTransform.position;
        MoveStop();
    }

    private void Update()
    {
        if (!stopping)
        {
            spriteParentTransform.position += velocity * Time.deltaTime;
            stopTime += Time.deltaTime;
        }
        else
        {
            stopTime -= Time.deltaTime;
            spriteParentTransform.position += velocity * Time.deltaTime;
            if (stopTime < 0f)
            {
                MoveStop();
            }
        }
    }

    public void MoveLeft()
    {
        if (!UIManager.instance.fading)
        {
            disableAllSprites();
            leftSprite.SetActive(true);
            velocity.x = -speed;
        }
    }

    public void MoveRight()
    {
        if (!UIManager.instance.fading)
        {
            disableAllSprites();
            rightSprite.SetActive(true);
            velocity.x = speed;
        }
        
    }

    public void MoveStop()
    {
        if (!UIManager.instance.fading)
        {
            disableAllSprites();
            stopSprite.SetActive(true);
            velocity.x = 0;
        }   
    }

    private void disableAllSprites()
    {
        stopSprite.SetActive(false);
        leftSprite.SetActive(false);
        rightSprite.SetActive(false);
        stopping = false;
        stopTime = 0f;
    }

    public void ResetPosition()
    {
        spriteParentTransform.position = startingPosition - velocity * 1f;
        stopping = true;
    }

    private void OnDisable()
    {
        MoveStop();
        ResetPosition();
    }
}
