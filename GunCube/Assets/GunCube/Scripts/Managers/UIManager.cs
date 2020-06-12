using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text levelText;
    public Slider healthSlider;

    private ScoreManager sm;

    private void Start()
    {
        sm = ManagerManager.scoreManager;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = sm.score.ToString();
        levelText.text = sm.difficulty.ToString("F1");
        healthSlider.value = ((float)sm.hp / 100f);
    }
}
