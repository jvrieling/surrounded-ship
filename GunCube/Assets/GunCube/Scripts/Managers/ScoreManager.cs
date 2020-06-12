using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public int hp = 100;

    public float difficulty = 0;
    public float difficultyIncrement = 0.1f;
    public float difficultyIncrementTime = 1;
    private float difficultyIncrementTimer;

    public SpawnSpeedChangeEvent spawnSpeedChangeEvent;

    private void Awake()
    {
        spawnSpeedChangeEvent = new SpawnSpeedChangeEvent();
        difficulty = OptionsHolder.options.difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        difficultyIncrementTimer += Time.deltaTime;
        if (difficultyIncrementTimer > difficultyIncrementTime)
        {
            difficultyIncrementTimer = 0;
            difficulty += difficultyIncrement;
            if (difficulty > 1) spawnSpeedChangeEvent.Invoke(0.9f);
            if (difficulty > 2) spawnSpeedChangeEvent.Invoke(0.8f);
            if (difficulty > 3) spawnSpeedChangeEvent.Invoke(0.7f);
            if (difficulty > 4) spawnSpeedChangeEvent.Invoke(0.6f);
            if (difficulty > 6) spawnSpeedChangeEvent.Invoke(0.5f);
            if (difficulty > 8) spawnSpeedChangeEvent.Invoke(0.4f);
            if (difficulty > 10) spawnSpeedChangeEvent.Invoke(0.3f);
        }
    }
    public void AddScore(int amount)
    {
        score += amount;
    }
    public void ReduceHealth(int amount)
    {
        hp -= amount;


        if(hp <= 0)
        {
            OptionsHolder.options.CompleteRound(difficulty);
            OptionsHolder.options.CheckHighScore(score);

            SceneManager.LoadScene("sc_MainMenu");
        }
    }

}

[System.Serializable]
public class SpawnSpeedChangeEvent : UnityEvent<float> { }