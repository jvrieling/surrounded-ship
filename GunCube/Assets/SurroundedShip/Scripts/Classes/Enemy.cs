///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy is a class to hold data about an enemy. It's used when an enemy prefab is instantiated to change its speed, damage, health, and sail colour.
/// </summary>
[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy", order = 2)]
public class Enemy : ScriptableObject
{
    new public string name = "";


    [Tooltip("The minimum difficulty the ship can appear at.")]
    public float minDifficulty;
    public float maxDifficulty = 1000;

    public float movementSpeed = 1;
    public int damage = 1;
    public float hp = 3;

    public int pointValue = 10;
    public int goldValue = 1;

    public Color color = Color.red;
}
