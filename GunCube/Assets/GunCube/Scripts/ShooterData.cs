using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShooterData", menuName = "ShooterData", order = 4)]
public class ShooterData : ScriptableObject
{
    public float timeBetweenShots = 1;

    public float bulletVelocity = 1;
    public float bulletDamage = 1;
    public float accuracy = 0.7f;

    public int sniperUpgrades;
    public int shotgunUpgrades;
    public int cannonUpgrades;

    public void UpgradeGun(Upgrade data)
    {
        accuracy += data.accuracy;
        bulletDamage += data.bulletDamage;
        bulletVelocity += data.bulletVelocity;
    }
}
