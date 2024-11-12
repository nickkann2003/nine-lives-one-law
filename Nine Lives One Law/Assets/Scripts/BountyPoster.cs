using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BountyPoster : MonoBehaviour
{
    [Header("References")]
    public GameObject bountyImage;
    public GameObject bountyShotImage;
    public GameObject bountyPosterHover;

    // Start is called before the first frame update
    void Start()
    {
       bountyImage.SetActive(true);
       bountyShotImage.SetActive(false);
       bountyPosterHover.SetActive(false);
    }

    public void ShootBounty()
    {
        bountyImage.SetActive(false);
        bountyShotImage.SetActive(true);
        bountyPosterHover.SetActive(false);
    }

    public void HoverBounty()
    {
        bountyPosterHover.SetActive(true);
        bountyImage.SetActive(false);
    }

    public void UnHoverBounty()
    {
        bountyImage.SetActive(true);
        bountyPosterHover.SetActive(false);
    }

    public void ResetBounty()
    {
        bountyImage.SetActive(true);
        bountyShotImage.SetActive(false);
        bountyPosterHover.SetActive(false);
    }

    private void OnMouseEnter()
    {
        HoverBounty();
    }

    private void OnMouseExit()
    {
        UnHoverBounty();
    }
}
