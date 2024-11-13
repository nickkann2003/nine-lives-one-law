using System;
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
    protected Player player;
    [SerializeField]
    public List<UpTuple> upgrades = new List<UpTuple>();
    public List<UpgradeHelper.Upgrade> upgradeDelegates = new List<UpgradeHelper.Upgrade>();

    private void Start()
    {
        foreach(UpTuple tup in upgrades)
        {
            upgradeDelegates.Add(UpgradeHelper.instance.CreateUpgrade(tup.type));
        }
    }

    [NonSerialized]
    public bool purchased = false;

    /// <summary>
    /// Performs whatever effects this upgrade does. 
    /// Subclasses write there logic here.
    /// This should be called once, when the item is acquired.
    /// </summary>
    public virtual void PerformUpgrade()
    {
        purchased = true;
        for(int i = 0; i < upgradeDelegates.Count; i++)
        {
            upgradeDelegates[i].Invoke(upgrades[i].val);
        }
    }
}

[Serializable]
public class UpTuple
{
    public UpgradeType type;
    public float val;
    public UpTuple(UpgradeType type, float val)
    {
        this.type = type;
        this.val = val;
    }
}
