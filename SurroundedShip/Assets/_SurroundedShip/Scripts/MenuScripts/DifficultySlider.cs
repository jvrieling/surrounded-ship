///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Handled the slider that changes the difficulty.
/// </summary>
public class DifficultySlider : MonoBehaviour
{
    public Slider difficultySlider;
    public Image sliderBackground;
    public Text difficultyText;

    // Start is called before the first frame update
    void Start()
    {
        if (OptionsHolder.instance != null)
        {
            if (OptionsHolder.instance.save == null)
            {
                enabled = false;
                return;
            }
        }
        else
        {
            enabled = false;
            return;
        }
        difficultySlider.maxValue = OptionsHolder.instance.save.recordDifficulty;
        difficultySlider.value = OptionsHolder.instance.save.difficulty;

        BGMManager.instance.StartMusic();
    }

    // Update is called once per frame
    void Update()
    {
        sliderBackground.color = Color.Lerp(Color.green, Color.red, difficultySlider.value / OptionsHolder.instance.save.recordDifficulty);
        difficultyText.text = difficultySlider.value.ToString("F1");
    }

    public void StartGame()
    {
        OptionsHolder.instance.save.difficulty = difficultySlider.value;
        BGMManager.instance.SetMusicLevel(1);
        BannerAd.showBannerAd = false;
        SceneManager.LoadScene("sc_Game");
    }
}
