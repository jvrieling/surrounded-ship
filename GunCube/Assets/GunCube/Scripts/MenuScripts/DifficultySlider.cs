using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultySlider : MonoBehaviour
{
    public Slider difficultySlider;
    public Image sliderBackground;
    public Text difficultyText;

    // Start is called before the first frame update
    void Start()
    {
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
        SceneManager.LoadScene("sc_Game");
    }
}
