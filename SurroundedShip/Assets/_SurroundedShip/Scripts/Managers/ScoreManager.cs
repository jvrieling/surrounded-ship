///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

/// <summary>
/// Manages the scores for the current game, then sends them over to the save game to be saved to a file.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    const float MIN_TIME_BETWEEN_SPAWNS = 0.15f;

    public int score;
    public int hp = 100;
    public int kills = 0;
    public int gold = 0;
    public float roundDuration;

    public float difficulty = 0;
    public float difficultyIncrement = 0.1f;
    public float difficultyIncrementTime = 1;       //How long between each difficulty increment
    private float difficultyIncrementTimer;

    public GameObject spawner;

    public float timeBetweenSpawns = 1;


    private void Awake()
    {
        difficulty = OptionsHolder.instance.save.difficulty;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            difficulty = 19.8f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            difficulty = 39.8f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            difficulty = 59.8f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            difficulty = 79.8f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            difficulty = 99.8f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            difficulty = 119.8f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            difficulty = 139.8f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            difficulty = 159.8f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            difficulty = 179.8f;
        }
#endif

        if (spawner.activeSelf && !CircleSpawner.bossActive)
        {
            roundDuration += Time.deltaTime;
            difficultyIncrementTimer += Time.deltaTime;
            if (difficultyIncrementTimer > difficultyIncrementTime)
            {
                difficultyIncrementTimer = 0;
                difficulty += difficultyIncrement;
                if ((Mathf.Round(difficulty * 10) / 10) % 20 == 0)
                {
                    //timeBetweenSpawns = Mathf.Clamp(timeBetweenSpawns -= 0.1f, MIN_TIME_BETWEEN_SPAWNS, 1);
                    spawner.GetComponent<CircleSpawner>().SpawnBoss();
                }
            }
        }
    }
    public void AddScore(int amount)
    {
        score += amount;
    }
    public void AddGold(int amount)
    {
        gold += amount;
    }
    public void ReduceHealth(int amount)
    {
        hp -= amount;


        if (hp <= 0)
        {
            BannerAd.showBannerAd = true;
            OptionsHolder.instance.save.CompleteRound(difficulty, kills, gold, roundDuration);
            OptionsHolder.instance.save.CheckHighScore(score);

            OptionsHolder.instance.SaveGame();

            BGMManager.instance.StopMusic();

            AnalyticsResult analytic = Analytics.CustomEvent("standard_game", new Dictionary<string, object> {
                {"difficulty_reached", difficulty},
                {"kills", kills },
                {"gold_earned", gold },
                {"time_survived", roundDuration }
            });
            Debug.Log("Sent analytic on game played! " + analytic);

            SceneManager.LoadScene("sc_EndScreen");
        }
    }
    public void AddKill()
    {
        kills++;
    }

    public void ForceGameEnd()
    {
        hp = 0;
        ReduceHealth(1);
    }

}