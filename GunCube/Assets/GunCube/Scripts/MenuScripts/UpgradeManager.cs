using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UpgradeType { sniper = 1, shotgun = 2, minigun = 3 };
public class UpgradeManager : MonoBehaviour
{
    public int selectedGun = 1;

    public Upgrade sniperUpgrade;

    public Upgrade shotgunUpgrade;

    public Upgrade minigunUpgrade;

    public Text selectedGunText;

    private void Start()
    {
        ValidateGunSelection();
    }

    public void SniperUpgrade() { UpgradeGun(sniperUpgrade); }
    public void MinigunUpgrade() { UpgradeGun(minigunUpgrade); }
    public void ShotgunUpgrade() { UpgradeGun(shotgunUpgrade); }
    public void UpgradeGun(Upgrade data)
    {
        OptionsHolder.instance.save.UpgradeGun(selectedGun, data);
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
}
