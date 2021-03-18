using UnityEngine;

public class GPGSLeaderboards : MonoBehaviour
{
    public static void OpenLeaderboard()
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
