using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BountyPoster : MonoBehaviour
{
    [Header("References")]
    public GameObject bountyImage;
    public GameObject bountyShotImage;
    public Animator bountyAnimator;

    // Start is called before the first frame update
    void Start()
    {
       bountyImage.SetActive(true);
       bountyShotImage.SetActive(true);
    }

    public void ShootBounty()
    {
        bountyImage.SetActive(false);
        bountyShotImage.SetActive(true);
    }

    public void ResetBounty()
    {
        bountyImage.SetActive(true);
        bountyShotImage.SetActive(true);
    }
}
