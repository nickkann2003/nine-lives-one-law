using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * STATS MANAGER
 * Class for handling all stats
 * Initial setup incorporates only money, all stats should flow through here
 */
public class StatsManager : MonoBehaviour
{
    // Money
    public float currentMoney = 0;
    public float totalMoney = 0;
    public GameObject moneyUIDisplay;
    public TextMeshProUGUI cMoneyDisplay;
    public Player player;

    // Other stats
    public float bossesDefeated = 0;
    public float bulletsFired = 0;
    public float enemiesDefeated = 0;
    public float dynamiteExploded = 0;
    public float dynamiteTeamKills = 0;
    public float dodgeRollCount = 0;
    public float deathCount = 0;

    // TMPro Other Stats
    public TextMeshProUGUI totalMoneyText;
    public TextMeshProUGUI enemiesDefeatedText;
    public TextMeshProUGUI bossesDefeatedText;
    public TextMeshProUGUI bulletsFiredText;
    public TextMeshProUGUI dodgeCountText;
    public TextMeshProUGUI deathCountText;

    public static StatsManager instance;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1.15f; //	(�_�)
    }

    // ------------------ Functions ------------------
    /// <summary>
    /// Gives the player money
    /// </summary>
    /// <param name="m">Money to add</param>
    public void AddMoney(float m)
    {
        currentMoney += m;
        totalMoney += m;
        UpdateMoneyValue();
    }

    /// <summary>
    /// Subtracts money from the player. No negative checks
    /// </summary>
    /// <param name="m">Money to subtract</param>
    public void SubtractMoney(float m)
    {
        currentMoney -= m;
        UpdateMoneyValue();
    }

    /// <summary>
    /// Checks if the player has money greater than a value
    /// </summary>
    /// <param name="m">Amount player has more/less than</param>
    /// <returns>Whether the player has more money than the value passed in</returns>
    public bool CheckBalanceHasEnough(float m)
    {
        if (currentMoney >= m)
            return true;
        return false;
    }

    public void HideMoney()
    {
        moneyUIDisplay.SetActive(false);
    }

    public void ShowMoney()
    {
        moneyUIDisplay.SetActive(true);
    }

    public void UpdateMoneyValue()
    {
        cMoneyDisplay.text = currentMoney.ToString();
    }

    public void UpdateStatsPage()
    {
        totalMoneyText.text = totalMoney.ToString();
        enemiesDefeatedText.text = enemiesDefeated.ToString();
        bossesDefeatedText.text = bossesDefeated.ToString();
        bulletsFiredText.text = bulletsFired.ToString();
        dodgeCountText.text = dodgeRollCount.ToString();
        deathCountText.text = deathCount.ToString();
    }
}
