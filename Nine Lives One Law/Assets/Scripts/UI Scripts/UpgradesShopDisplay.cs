using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesShopDisplay : MonoBehaviour
{
    public TextMeshProUGUI nameDisplay;
    public Image itemDisplay;

    public string itemName;
    public Sprite itemImage;
    public string itemDescription;
    public float itemPrice;

    private UpgradeBase upgrade;

    // Start is called before the first frame update
    void Start()
    {
        SetDisplayValues();
    }

    /// <summary>
    /// Sets this shop items values based on an upgrade
    /// </summary>
    /// <param name="u"></param>
    public void SetUpgrade(UpgradeBase u)
    {
        this.itemName = u.itemName;
        this.itemDescription = u.itemDescription;
        this.itemImage = u.itemSprite;
        this.itemPrice = u.itemShopPrice;
        SetDisplayValues();
    }

    public void PurchaseItem()
    {
        if (StatsManager.instance.CheckBalanceHasEnough(itemPrice) && upgrade != null)
        {
            StatsManager.instance.SubtractMoney(itemPrice);
            upgrade.PerformUpgrade();
        }
    }

    /// <summary>
    /// Private function that sets visual values based on internal state
    /// </summary>
    private void SetDisplayValues()
    {
        nameDisplay.text = itemName;
        if (itemImage != null)
            itemDisplay.sprite = itemImage;
    }
}
