///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void DamageUpgrade() { UpgradeGun(damageUpgrade); }
    public void RateUpgrade() { UpgradeGun(rateUpgrade); }
    public void CountUpgrade() { UpgradeGun(countUpgrade); }
    public void AccuracyUpgrade() { UpgradeGun(accuracyUpgrade); }
    public void UpgradeGun(Upgrade data)
    {
        if (OptionsHolder.instance.save.totalGold >= data.cost)
        {
            OptionsHolder.instance.save.UpgradeGun(selectedGun, data);
            OptionsHolder.instance.save.totalGold -= data.cost;
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
