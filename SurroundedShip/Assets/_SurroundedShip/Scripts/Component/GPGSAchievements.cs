///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 18, 2021    ///
///////////////////////////////
using UnityEngine;
using GooglePlayGames;
using EasyMobile;
using UnityEngine.Analytics;

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
            Debug.Log("Reporting" + amount + "gold earned! " + 
                "\n" + System.Convert.ToDouble((amount / 10000f) * 100f) + "% of gold hoarder" +
                "\n" + System.Convert.ToDouble((amount / 2000f) * 100f) + "% of pocket change");
            GameServices.ReportAchievementProgress(EM_GameServicesConstants.Achievement_Gold_Hoarder, System.Convert.ToDouble((amount / 10000f) * 100f), null);
            GameServices.ReportAchievementProgress(EM_GameServicesConstants.Achievement_Pocket_Change, System.Convert.ToDouble((amount / 2000f) * 100f), null);
        }
    }

    public static void UpdateShipsDestroyed(int amount)
    {
        if (amount > 0)
        {
            Debug.Log("Reporting " + amount + " Ships Sunk! " +
                "\n" + System.Convert.ToDouble((amount / 20f) * 100f) + "% of defender" +
                "\n" + System.Convert.ToDouble((amount / 500f) * 100f) + "% of pirate eliminator");
            GameServices.ReportAchievementProgress(EM_GameServicesConstants.Achievement_Defender, System.Convert.ToDouble((amount / 20f) * 100f), null);
            GameServices.ReportAchievementProgress(EM_GameServicesConstants.Achievement_Pirate_Eliminator, System.Convert.ToDouble((amount / 500f) * 100f), null);
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
