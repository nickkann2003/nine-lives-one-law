using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for upgrades, holds basic information about the upgrade
public class UpgradeBase : MonoBehaviour
{
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public float itemShopPrice;

    /// <summary>
    /// Performs whatever effects this upgrade does. 
    /// Subclasses write there logic here.
    /// This should be called once, when the item is acquired.
    /// </summary>
    public virtual void PerformUpgrade()
    {

    }
}
