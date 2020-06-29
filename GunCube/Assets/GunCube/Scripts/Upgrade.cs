using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    public int cost;

    public float accuracy;
    public float bulletDamage;
    public float bulletVelocity;
    public float timeBetweenShots;
    public int bulletCount;
}
