///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The Upgrade object holds data for an upgrade that can be aplied to a gun. Used to easily have control over what the shop offers.
/// </summary>
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
