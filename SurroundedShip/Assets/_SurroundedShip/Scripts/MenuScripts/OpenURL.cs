using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class OpenURL : MonoBehaviour
{
    public string url;
    public void Click()
    {
        Analytics.CustomEvent("click_link", new Dictionary<string, object> {{"url", url}});
        Application.OpenURL(url);
    }
}
