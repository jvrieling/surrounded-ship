using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyMobile;

public class DoubleGoldRewardedAd : MonoBehaviour
{
    public int goldReward => OptionsHolder.instance.save.gold;

    public ScoreCounter counter;

    private Button button;

    // Subscribe to rewarded ad events
    void OnEnable()
    {
        button = GetComponent<Button>();

        Advertising.LoadRewardedAd();
        Advertising.RewardedAdCompleted += RewardedAdCompletedHandler;
        Advertising.RewardedAdSkipped += RewardedAdSkippedHandler;
    }

    // Unsubscribe events
    void OnDisable()
    {
        Advertising.RewardedAdCompleted -= RewardedAdCompletedHandler;
        Advertising.RewardedAdSkipped -= RewardedAdSkippedHandler;
    }

    // Event handler called when a rewarded ad has completed
    void RewardedAdCompletedHandler(RewardedAdNetwork network, AdPlacement location)
    {
        Debug.Log("Rewarded ad has completed. The user should be rewarded now.");
        OptionsHolder.instance.save.totalGold += goldReward;
        button.interactable = false;
        counter.SetGoldDoubled();
    }

    // Event handler called when a rewarded ad has been skipped
    void RewardedAdSkippedHandler(RewardedAdNetwork network, AdPlacement location)
    {
        Debug.Log("Rewarded ad was skipped. The user should NOT be rewarded.");
    }
    public void PlayRewardedAd()
    {
        bool isReady = Advertising.IsRewardedAdReady();

        if (isReady)
        {
            Advertising.ShowRewardedAd();
        }
    }
}
