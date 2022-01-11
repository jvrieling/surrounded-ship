///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ShooterData Holds the data for the gun. Controls upgrades, and makes sure values don't get out of hand!
/// </summary>
[System.Serializable]
public class ShooterData
{
    public const float MIN_ACCURACY = 0f;
    public const float MAX_ACCURACY = 0.98f;

    public const float MIN_SHOTTIME = 0.1f;
    public const float MAX_SHOTTIME = 2f;

    public const float MAX_VELOCITY = 6;

    public const float MIN_DAMAGE = 0.3f;
    public const float MAX_DAMAGE = 100;

    public const int MIN_BULLETS = 1;
    public const int MAX_BULLETS = 10;

    public float timeBetweenShots = 0.75f;

    public float bulletVelocity = 4;
    public float bulletDamage = 1;
    public float accuracy = 0.25f;
    public int bulletCount = 1;

    public int sniperUpgrades;
    public int shotgunUpgrades;
    public int cannonUpgrades;

    public void UpgradeGun(Upgrade data)
    {
        accuracy =          Mathf.Clamp(accuracy += data.accuracy, MIN_ACCURACY, MAX_ACCURACY);
        bulletDamage =      Mathf.Clamp(bulletDamage += data.bulletDamage, MIN_DAMAGE, float.PositiveInfinity);
        bulletVelocity =    Mathf.Clamp(bulletVelocity += data.bulletVelocity, 4, MAX_VELOCITY);
        timeBetweenShots =  Mathf.Clamp(timeBetweenShots += data.timeBetweenShots, MIN_SHOTTIME, MAX_SHOTTIME);
        bulletCount =       Mathf.Clamp(bulletCount += data.bulletCount, MIN_BULLETS, MAX_BULLETS);
    }
}
