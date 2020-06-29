using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterManager : MonoBehaviour
{
    private static int GUN_COUNT = 4;

    public GameObject bulletHolder;

    private ShooterController[] guns;

    private void Awake()
    {
        guns = new ShooterController[4];
    }

    private void Start()
    {
        guns[0] = ManagerManager.instance.player.transform.GetChild(0).GetComponent<ShooterController>();
        guns[1] = ManagerManager.instance.player.transform.GetChild(1).GetComponent<ShooterController>();
        guns[2] = ManagerManager.instance.player.transform.GetChild(2).GetComponent<ShooterController>();
        guns[3] = ManagerManager.instance.player.transform.GetChild(3).GetComponent<ShooterController>();

        guns[0].InitializeData(OptionsHolder.instance.save.gun1);
        guns[1].InitializeData(OptionsHolder.instance.save.gun2);
        guns[2].InitializeData(OptionsHolder.instance.save.gun3);
        guns[3].InitializeData(OptionsHolder.instance.save.gun4);
    }

    public void UpgradeAllGuns(Upgrade data)
    {
        for(int i = 0; i < GUN_COUNT; i++)
        {
            UpgradeGun(i, data);
        }
    }
    public void UpgradeGun(int index, Upgrade data)
    {
        guns[index].accuracy += data.accuracy;
        guns[index].bulletDamage += data.bulletDamage;
        guns[index].bulletVelocity += data.bulletVelocity;
    }

}
