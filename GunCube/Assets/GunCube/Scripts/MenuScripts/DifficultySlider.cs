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
        difficultySlider.maxValue = OptionsHolder.options.recordDifficulty;
        difficultySlider.value = OptionsHolder.options.difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        sliderBackground.color = Color.Lerp(Color.green, Color.red, difficultySlider.value / OptionsHolder.options.recordDifficulty);
        difficultyText.text = difficultySlider.value.ToString("F1");
    }

    public void StartGame()
    {
        OptionsHolder.options.difficulty = difficultySlider.value;
        SceneManager.LoadScene("sc_SampleScene");
    }
}
