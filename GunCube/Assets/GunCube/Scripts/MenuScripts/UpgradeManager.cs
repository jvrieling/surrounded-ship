using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UpgradeType { sniper = 1, shotgun = 2, minigun = 3 };
public class UpgradeManager : MonoBehaviour
{
    public int selectedGun;

    public Upgrade sniperUpgrade;

    public Upgrade shotgunUpgrade;

    public Upgrade minigunUpgrade;

    public Text selectedGunText;

    public void SniperUpgrade() { UpgradeGun(sniperUpgrade); }
    public void MinigunUpgrade() { UpgradeGun(minigunUpgrade); }
    public void ShotgunUpgrade() { UpgradeGun(shotgunUpgrade); }
    public void UpgradeGun(Upgrade data)
    {
        OptionsHolder.options.UpgradeGun(selectedGun, data);
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
        if (selectedGun < 0)
        {
            selectedGun = 3;
        }

        if (selectedGun > 3)
        {
            selectedGun = 0;
        }

        selectedGunText.text = (selectedGun + 1).ToString();
    }
}
