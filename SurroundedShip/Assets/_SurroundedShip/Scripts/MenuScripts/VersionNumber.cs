///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 18, 2021     ///
///////////////////////////////

using UnityEngine;
using UnityEngine.UI;

public class VersionNumber : MonoBehaviour
{
    public Text versionText;
    string platformCode;
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android) platformCode = "AN";
        else if (Application.platform == RuntimePlatform.WindowsEditor) platformCode = "U3D";
        else if (Application.platform == RuntimePlatform.WebGLPlayer) platformCode = "WGL";
        versionText.text = platformCode + Application.version;
    }

}
