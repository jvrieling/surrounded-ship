///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds a refrence to the other managers for easy access from other scripts.
/// </summary>
public class ManagerManager : MonoBehaviour
{
    public static ManagerManager instance;
    public static ShooterManager shooterManager;
    public static ScoreManager scoreManager;
    public static UIManager uiManager;

    public GameObject player;

    private void Awake()
    {
        instance = this;

        shooterManager = GetComponent<ShooterManager>();
        scoreManager = GetComponent<ScoreManager>();
        uiManager = GetComponent<UIManager>();
    }
}
