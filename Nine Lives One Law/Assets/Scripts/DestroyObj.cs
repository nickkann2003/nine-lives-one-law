using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    public void DestroyThisObj()
    {
        Destroy(gameObject);
    }
}
