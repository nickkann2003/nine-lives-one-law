using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum UpgradeType
{
    Health_flat,
    Speed_multiplier,
    Damage_flat,
    FireRate_multiplier,
    RollCooldown_multiplier,
    RollSpeed_multiplier,
    RollDuration_multiplier,
    DuelTime
}

public class UpgradeHelper : MonoBehaviour
{
    public static UpgradeHelper instance;
    public Player player;

    public delegate void Upgrade(float val);

    private void Awake()
    {
        instance = this;
    }

    public Upgrade CreateUpgrade(UpgradeType t)
    {
        Upgrade up = player.UpgradeMaxHealth;
        switch (t)
        {
            case UpgradeType.Health_flat:
                up = player.UpgradeMaxHealth;
                break;
            case UpgradeType.Speed_multiplier:
                up = player.UpgradeSpeed;
                break;
            case UpgradeType.Damage_flat:
                up = player.UpgradeDamage;
                break;
            case UpgradeType.FireRate_multiplier:
                up = player.UpgradeFireRate;
                break;
            case UpgradeType.RollCooldown_multiplier:
                up = player.UpgradeRollCooldown;
                break;
            case UpgradeType.RollSpeed_multiplier:
                up = player.UpgradeRollSpeed;
                break;
            case UpgradeType.RollDuration_multiplier:
                up = player.UpgradeRollDuration;
                break;
            case UpgradeType.DuelTime:
                break;
        }
        return up;
    }
}
