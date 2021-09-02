///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

/// <summary>
/// Handles the upgrades as they go through to the shooterData and thus the save game.
/// </summary>
public enum UpgradeType { sniper = 1, shotgun = 2, minigun = 3 };
public class UpgradeManager : MonoBehaviour
{
    public int selectedGun = 1;

    public Upgrade damageUpgrade;
    public Upgrade countUpgrade;
    public Upgrade rateUpgrade;
    public Upgrade accuracyUpgrade;

    private int damageCount;
    private int countCount;
    private int rateCount;
    private int accuracyCount;
    private int totalGoldSpent;

    public Text selectedGunText;
    public Text rateUpgradeText;
    public Text damageUpgradeText;
    public Text countUpgradeText;
    public Text accuracyUpgradeText;
    public Text yourGoldText;

    private void Start()
    {
        ValidateGunSelection();
        rateUpgradeText.text = "" + rateUpgrade.cost;
        damageUpgradeText.text = "" + damageUpgrade.cost;
        countUpgradeText.text = "" + countUpgrade.cost;
        accuracyUpgradeText.text = "" + accuracyUpgrade.cost;
    }

    private void Update()
    {
        yourGoldText.text = "" + OptionsHolder.instance.save.totalGold;
    }
    private void OnDestroy()
    {
        if (totalGoldSpent > 0)
        {
            Debug.Log("Sending analytic for upgrades: " + AnalyticsEvent.LevelUp("upgrade_ship", new Dictionary<string, object> {
            {"damage", damageCount},
            {"accuracy", accuracyCount },
            {"count", countCount },
            {"rate", rateCount},
            {"total_spent", totalGoldSpent }

        }));
        }
    }

    public void DamageUpgrade() { UpgradeGun(damageUpgrade);  }
    public void RateUpgrade() { UpgradeGun(rateUpgrade);  }
    public void CountUpgrade() { UpgradeGun(countUpgrade);  }
    public void AccuracyUpgrade() { UpgradeGun(accuracyUpgrade); }
    public void UpgradeGun(Upgrade data)
    {
        if (OptionsHolder.instance.save.totalGold >= data.cost)
        {
            OptionsHolder.instance.save.UpgradeGun(selectedGun, data);
            OptionsHolder.instance.save.totalGold -= data.cost;

            totalGoldSpent += data.cost;
            if(data.accuracy > 0) { accuracyCount++; } 
            else if(data.bulletCount > 0) { countCount++; }
            else if(data.bulletDamage > 0) { damageCount++; }
            else if(data.timeBetweenShots < 0) { rateCount++; }

        }
    }

    public void LeftSelection()
    {
        selectedGun--;
        ValidateGunSelection();
    }
    public void RightSelection()
    {
        selectedGun++;
        ValidateGunSelection();
    }
    private void ValidateGunSelection()
    {
        if (selectedGun < 1)
        {
            selectedGun = 4;
        }

        if (selectedGun > 4)
        {
            selectedGun = 1;
        }

        selectedGunText.text = (selectedGun).ToString();
    }
    public void AddGold()
    {
        OptionsHolder.instance.GiveGold();
    }
}
