﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    const float MIN_TIME_BETWEEN_SPAWNS = 0.2f;

    public int score;
    public int hp = 100;
    public int kills = 0;
    public int gold = 0;


    public float difficulty = 0;
    public float difficultyIncrement = 0.1f;
    public float difficultyIncrementTime = 1;
    private float difficultyIncrementTimer;

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
        difficultyIncrementTimer += Time.deltaTime;
        if (difficultyIncrementTimer > difficultyIncrementTime)
        {
            difficultyIncrementTimer = 0;
            difficulty += difficultyIncrement;

            if(difficulty % 10 == 0)
            {
                timeBetweenSpawns -= 0.1f;
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
            OptionsHolder.instance.save.CompleteRound(difficulty, kills, gold);
            OptionsHolder.instance.save.CheckHighScore(score);

            OptionsHolder.instance.SaveGame();

            BGMManager.instance.StopMusic();

            SceneManager.LoadScene("sc_EndScreen");
        }
    }
    public void AddKill()
    {
        kills++;
    }

}

[System.Serializable]
public class SpawnSpeedChangeEvent : UnityEvent<float> { }