using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableSavingToggle : MonoBehaviour
{
    public Toggle t;
    private void OnEnable()
    {
        t.isOn = OptionsHolder.instance.saveEnabled;
    }
    public void OnPress()
    {
        OptionsHolder.instance.saveEnabled = t.isOn;
    }
}
