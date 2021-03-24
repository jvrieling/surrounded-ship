///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 18, 2021     ///
///////////////////////////////
using UnityEngine;

public class GPGSLeaderboards : MonoBehaviour
{
    public void OpenLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    public static void UpdateLeaderboardScore(int score)
    {
        if(score == 0)
        {
            return;
        }

        Social.ReportScore(score, GPGSIds.leaderboard_high_scores, null);
    }
}
