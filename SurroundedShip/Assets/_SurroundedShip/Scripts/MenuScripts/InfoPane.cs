using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPane : MonoBehaviour
{
    public Text textBox;
    public static string log;

    public void OnEnable()
    {
        textBox.text = OptionsHolder.instance.save.GetSummary();
        textBox.text += "\n\n" + log;
    }
}
