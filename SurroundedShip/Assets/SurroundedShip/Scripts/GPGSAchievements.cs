using UnityEngine;
using GooglePlayGames;

public class GPGSAchievements : MonoBehaviour
{
    public static void OpenAchievementPanel()
    {
        Social.ShowAchievementsUI();
    }

    public static void UpdateIncremental(int amount)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_gold_hoarder, amount, (bool success) =>
        {
            Debug.Log("Achievement increment was a " + success);
        });
    }

    public static void FirstDayAtSea()
    {
        Social.ReportProgress(GPGSIds.achievement_first_day_at_sea, 100f, null);
    }


}
