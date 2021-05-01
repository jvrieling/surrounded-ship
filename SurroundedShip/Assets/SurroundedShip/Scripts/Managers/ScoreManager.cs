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

    public SpawnSpeedChangeEvent spawnSpeedChangeEvent;

    private void Awake()
    {
        spawnSpeedChangeEvent = new SpawnSpeedChangeEvent();
        difficulty = OptionsHolder.instance.save.difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawner.activeSelf)
        {
            roundDuration += Time.deltaTime;
            difficultyIncrementTimer += Time.deltaTime;
            if (difficultyIncrementTimer > difficultyIncrementTime)
            {
                difficultyIncrementTimer = 0;
                difficulty += difficultyIncrement;

                if (difficulty % 10 == 0)
                {
                    timeBetweenSpawns = Mathf.Clamp(timeBetweenSpawns -= 0.1f, MIN_TIME_BETWEEN_SPAWNS, 1);
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


        if(hp <= 0)
        {
            BannerAd.showBannerAd = true;
            OptionsHolder.instance.save.CompleteRound(difficulty, kills, gold, roundDuration);
            OptionsHolder.instance.save.CheckHighScore(score);

            OptionsHolder.instance.SaveGame();

            BGMManager.instance.StopMusic();

            if (OptionsHolder.instance.save.gamesPlayed == 0)
            {
                //unlock the first day at sea achievement.
                GPGSAchievements.AchieveFirstDayAtSea();
                AnalyticsResult analyticAchievement = AnalyticsEvent.AchievementUnlocked(EM_GPGSIds.achievement_first_day_at_sea);
                Debug.Log("Sent analytic on first day at sea! " + analyticAchievement);
            }

            AnalyticsResult analytic = AnalyticsEvent.LevelComplete("standard_game", new Dictionary<string, object> {
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

[System.Serializable]
public class SpawnSpeedChangeEvent : UnityEvent<float> { }