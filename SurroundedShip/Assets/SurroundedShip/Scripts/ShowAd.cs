using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;

public class ShowAd : MonoBehaviour
{
    public void Show()
    {
        bool ready = Advertising.IsInterstitialAdReady();
        Debug.Log(ready);
        if (ready)
        {
            Advertising.ShowInterstitialAd();
        }
    }
    void OnEnable()
    {
        Advertising.InterstitialAdCompleted += InterstitialAdCompletedHandler;
    }

    // The event handler
    void InterstitialAdCompletedHandler(InterstitialAdNetwork network, AdPlacement location)
    {
        Debug.Log("Interstitial ad has been closed.");
    }

    // Unsubscribe
    void OnDisable()
    {
        Advertising.InterstitialAdCompleted -= InterstitialAdCompletedHandler;
    }
}
