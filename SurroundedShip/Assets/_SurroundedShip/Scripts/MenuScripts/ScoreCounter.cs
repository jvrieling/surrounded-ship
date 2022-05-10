///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

/// <summary>
/// Tallies up the score on the ending screen after game over.
/// </summary>
public class ScoreCounter : MonoBehaviour
{
    private const int INCREMENT_SEGMENTS = 20;

    public Text scoreText;
    public Text killsText;
    public Text diffText;
    public Text goldText;
    public Button menuButton;

    public float steps = 18;
    public float stepDuration = 0.07f;

    [EventRef]
    public string coinStashSound;

    public SaveGame debugSave;
    private SaveGame save;
    void Start()
    {
        if(OptionsHolder.instance == null)
        {
            save = debugSave;
        } else
        {
            save = OptionsHolder.instance.save;
        }
        StartCoroutine(CountScores());
        StartCoroutine(ForceMenuButton());
    }

    public IEnumerator ForceMenuButton()
    {
        yield return new WaitForSeconds(4);
        menuButton.interactable = true;
    }
    public IEnumerator CountScores()
    {
        int score = 0, kills = 0, gold = 0;
        float diff = 0;

        do
        {
            scoreText.text = score.ToString();
            goldText.text = gold.ToString();
            killsText.text = kills.ToString();
            diffText.text = diff.ToString("0.0");

            int scoreStep = Mathf.CeilToInt(save.score / steps);
            if (scoreStep == 0) scoreStep = 1;
            int killsStep = Mathf.CeilToInt(save.kills / steps);
            if (killsStep == 0) killsStep = 1;
            int coinStep = Mathf.CeilToInt(save.gold / steps);
            if (coinStep == 0) coinStep = 1;
            int diffStep = Mathf.CeilToInt(save.difficulty / steps);
            if (diffStep == 0) diffStep = 1;

            score = Mathf.Clamp(score + scoreStep, 0, save.score);
            kills = Mathf.Clamp(kills + killsStep, 0, save.kills);
            gold = Mathf.Clamp(gold + coinStep, 0, save.gold);
            diff = Mathf.Clamp(diff + diffStep, 0, save.difficulty);

            yield return new WaitForSeconds(stepDuration);
        } while (score < save.score);

        RuntimeManager.PlayOneShot(coinStashSound);

        scoreText.text = save.score.ToString();
        goldText.text = save.gold.ToString();
        killsText.text = save.kills.ToString();
        diffText.text = save.difficulty.ToString("0.0");

        menuButton.interactable = true;
    }
}
