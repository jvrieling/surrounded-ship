using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;

public class BannerAd : MonoBehaviour
{
    public static bool showBannerAd = true;

    public BannerAdPosition position;
    public BannerAdSize size;

    private bool currentlyShowing = false;

    // Start is called before the first frame update
    void Start()
    {
        ShowBanner();     
    }
    private void Update()
    {
        if (!showBannerAd)
        {
            Advertising.DestroyBannerAd();
            currentlyShowing = false;
        }
        else
        {
            ShowBanner();
        }
    }
    public void ShowBanner()
    {
        if (Advertising.IsInitialized() && !currentlyShowing)
        {
            currentlyShowing = true;

            Advertising.ShowBannerAd(position);
        }
    }
    private void OnDestroy()
    {
        Advertising.DestroyBannerAd();
    }

}
