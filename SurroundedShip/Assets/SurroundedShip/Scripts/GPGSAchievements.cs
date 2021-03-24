///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 18, 2021    ///
///////////////////////////////
using UnityEngine;
using GooglePlayGames;
using EasyMobile;

public class GPGSAchievements : MonoBehaviour
{
    public void OpenAchievementPanel()
    {
        if (GameServices.IsInitialized()) GameServices.ShowAchievementsUI();
    }

    public static void UpdateGoldEarned(int amount)
    {
        if (amount > 0)
        {
            GameServices.ReportAchievementProgress(GPGSIds.achievement_gold_hoarder, amount, null);
            GameServices.ReportAchievementProgress(GPGSIds.achievement_pocket_change, amount, null);
        }
    }

    public static void UpdateShipsDestroyed(int amount)
    {
        if (amount > 0)
        {
            GameServices.ReportAchievementProgress(GPGSIds.achievement_defender, amount, null);
            GameServices.ReportAchievementProgress(GPGSIds.achievement_pirate_eliminator, amount, null);
        }
    }

    public static void AchieveFirstDayAtSea()
    {
        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_First_Day);
    }
    public static void AchieveDestroyer()
    {
        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_Ult_Destroyer);
    }


}
