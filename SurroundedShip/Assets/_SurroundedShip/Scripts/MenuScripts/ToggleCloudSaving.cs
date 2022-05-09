///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: May 9, 2022       ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCloudSaving : MonoBehaviour
{
    public Toggle t;
    private void OnEnable()
    {
        t.isOn = OptionsHolder.instance.save.saveToCloud;
    }
    public void OnPress()
    {
        OptionsHolder.instance.save.saveToCloud = t.isOn;
    }
}
