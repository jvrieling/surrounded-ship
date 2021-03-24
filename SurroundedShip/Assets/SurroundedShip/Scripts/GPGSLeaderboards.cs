///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 18, 2021     ///
///////////////////////////////
using UnityEngine;
using EasyMobile;

public class GPGSLeaderboards : MonoBehaviour
{
    public void OpenLeaderboard()
    {
        if(GameServices.IsInitialized()) GameServices.ShowLeaderboardUI();
    }

    public static void UpdateLeaderboardScore(int score)
    {
        if(score == 0)
        {
            return;
        }

        GameServices.ReportScore(score, EM_GameServicesConstants.Leaderboard_High_Scores);
    }
}
