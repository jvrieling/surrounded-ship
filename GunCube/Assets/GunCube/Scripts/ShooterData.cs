﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShooterData
{
    public const float MIN_ACCURACY = 0.3f;
    public const float MAX_ACCURACY = 1f;

    public const float MIN_SHOTTIME = 0.1f;
    public const float MAX_SHOTTIME = 2f;

    public const float MAX_VELOCITY = 6;

    public const float MIN_DAMAGE = 0.3f;
    public const float MAX_DAMAGE = 10;

    public const int MIN_BULLETS = 1;
    public const int MAX_BULLETS = 15;

    public float timeBetweenShots = 0.75f;

    public float bulletVelocity = 4;
    public float bulletDamage = 1;
    public float accuracy = 0.7f;
    public int bulletCount = 1;

    public int sniperUpgrades;
    public int shotgunUpgrades;
    public int cannonUpgrades;

    public void UpgradeGun(Upgrade data)
    {
        accuracy =          Mathf.Clamp(accuracy += data.accuracy, MIN_ACCURACY, MAX_ACCURACY);
        bulletDamage =      Mathf.Clamp(bulletDamage += data.bulletDamage, MIN_DAMAGE, MAX_DAMAGE);
        bulletVelocity =    Mathf.Clamp(bulletVelocity += data.bulletVelocity, 4, MAX_VELOCITY);
        timeBetweenShots =  Mathf.Clamp(timeBetweenShots += data.timeBetweenShots, MIN_SHOTTIME, MAX_SHOTTIME);
        bulletCount =       Mathf.Clamp(bulletCount += data.bulletCount, MIN_BULLETS, MAX_BULLETS);
    }
}
