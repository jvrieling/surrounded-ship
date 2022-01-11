using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDamage : MonoBehaviour
{
    public Text dmg;

    // Update is called once per frame
    void Update()
    {
        switch (UpgradeManager.selectedGun)
        {
            case 1:
                dmg.text = OptionsHolder.instance.save.gun1.bulletDamage.ToString("###.##");
                break;
            case 2:
                dmg.text = OptionsHolder.instance.save.gun2.bulletDamage.ToString("###.##");
                break;
            case 3:
                dmg.text = OptionsHolder.instance.save.gun3.bulletDamage.ToString("###.##");
                break;
            case 4:
                dmg.text = OptionsHolder.instance.save.gun4.bulletDamage.ToString("###.##");
                break;

        }
    }
}
