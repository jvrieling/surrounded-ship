using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerManager : MonoBehaviour
{
    public static ManagerManager instance;
    public static ShooterManager shooterManager;
    public static ScoreManager scoreManager;
    public static UIManager uiManager;

    public GameObject player;

    private void Awake()
    {
        //if (instance != null) Destroy(gameObject);

        instance = this;
        //DontDestroyOnLoad(gameObject);

        shooterManager = GetComponent<ShooterManager>();
        scoreManager = GetComponent<ScoreManager>();
        uiManager = GetComponent<UIManager>();
    }
}
