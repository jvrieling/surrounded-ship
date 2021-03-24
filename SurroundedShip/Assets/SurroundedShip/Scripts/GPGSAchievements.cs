///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 18, 2021    ///
///////////////////////////////
using UnityEngine;
using GooglePlayGames;

public class GPGSAchievements : MonoBehaviour
{
    public void OpenAchievementPanel()
    {
        Social.ShowAchievementsUI();
    }

    public static void UpdateGoldEarned(int amount)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_gold_hoarder, amount, null);
        PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_pocket_change, amount, null);
    }

    public static void UpdateShipsDestroyed(int amount)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_defender, amount, null);
        PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_pirate_eliminator, amount, null);
    }

    public static void AchieveFirstDayAtSea()
    {
        Social.ReportProgress(GPGSIds.achievement_first_day_at_sea, 100f, null);
    }
    public static void AchieveDestroyer()
    {
        Social.ReportProgress(GPGSIds.achievement_ultimate_destroyer, 100f, null);
    }


}
