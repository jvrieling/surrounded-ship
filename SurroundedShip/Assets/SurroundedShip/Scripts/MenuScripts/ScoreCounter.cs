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

    public float timePerCounter = 1.5f;

    [EventRef]
    public string coinStashSound;

    void Start()
    {
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
        float tempScore = 0;
        
        do
        {
            tempScore = Mathf.Clamp(tempScore += Mathf.RoundToInt(OptionsHolder.instance.save.score / 10), 0, OptionsHolder.instance.save.score);
            scoreText.text = tempScore.ToString();
            yield return new WaitForSeconds(timePerCounter/ INCREMENT_SEGMENTS);
        } while (tempScore < OptionsHolder.instance.save.score);
        
        tempScore = 0;
        do
        {
            tempScore = Mathf.Clamp(tempScore += Mathf.RoundToInt(OptionsHolder.instance.save.kills / 10), 0, OptionsHolder.instance.save.kills);
            killsText.text = tempScore.ToString();
            yield return new WaitForSeconds(timePerCounter / INCREMENT_SEGMENTS);
        } while (tempScore < OptionsHolder.instance.save.kills);

        tempScore = 0;
        do
        {
            tempScore = Mathf.Clamp(tempScore += OptionsHolder.instance.save.difficulty / 10, 0, OptionsHolder.instance.save.difficulty);
            diffText.text = tempScore.ToString("F2");
            yield return new WaitForSeconds((timePerCounter/2) / INCREMENT_SEGMENTS);
        } while (tempScore < OptionsHolder.instance.save.difficulty);

        tempScore = 0;
        do
        {
            RuntimeManager.PlayOneShot(coinStashSound);
            tempScore = Mathf.Clamp(tempScore += Mathf.RoundToInt(OptionsHolder.instance.save.gold / 10), 0, OptionsHolder.instance.save.gold);
            goldText.text = tempScore.ToString();
            yield return new WaitForSeconds((timePerCounter * 1.2f) / INCREMENT_SEGMENTS);
        } while (tempScore < OptionsHolder.instance.save.gold);

        menuButton.interactable = true;
    }
}
