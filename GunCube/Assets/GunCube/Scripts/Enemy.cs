using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy", order = 2)]
public class Enemy : ScriptableObject
{
    public float minDifficulty;
    public float maxDifficulty = 1000;

    public float movementSpeed = 1;
    public int damage = 1;
    public float hp = 3;

    public int pointValue = 10;
    public int goldValue = 1;

    public Color color = Color.red;
}
