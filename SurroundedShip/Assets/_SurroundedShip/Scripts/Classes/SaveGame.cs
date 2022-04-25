///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

/// <summary>
/// SaveGame handles the data for the current instance of the game. It's a serialized object so it can be easily written to a file in binary.
/// </summary>
[System.Serializable]
public class SaveGame
{
    public string name;

    public float gamesPlayed = 0;

    public float difficulty;
    public float recordDifficulty;

    public int score;
    public int highScore;

    public int kills;
    public int highKills;

    public int gold;
    public int totalGold;

    public ShooterData gun1;
    public ShooterData gun2;
    public ShooterData gun3;
    public ShooterData gun4;

    public string gun1JSON;
    public string gun2JSON;
    public string gun3JSON;
    public string gun4JSON;

    /// <summary>
    /// The UTC DateTime that this save data was last saved to.
    /// </summary>
    public DateTime lastSaved;
    public DateTime dateStarted;
    public float totalTimePlayed;
    public string lastSaveGameVersion;

    public bool showTutorial = true;
    public bool firstGameCompleted;

    public SaveGame()
    {
        ResetSaveData();
    }

    public void ResetSaveData()
    {
        gun1 = new ShooterData();
        gun2 = new ShooterData();
        gun3 = new ShooterData();
        gun4 = new ShooterData();

        showTutorial = true;

        gamesPlayed = 0;
        showTutorial = true;
        difficulty = 0;
        recordDifficulty = 0;
        score = 0;
        highScore = 0;
        kills = 0;
        highKills = 0;
        gold = 0;
        totalGold = 0;
        lastSaved = DateTime.MinValue;
        dateStarted = DateTime.Now;
        totalTimePlayed = 0;
        firstGameCompleted = false;
    }

    //Called when the round ends. 
    public void CompleteRound(float diff, int shipsSunk, int goldEarned, float roundDuration)
    {
        //Update the scores in the SaveGame.
        difficulty = diff;
        if (diff > recordDifficulty) recordDifficulty = diff;
        kills = shipsSunk;
        if (kills > highKills) highKills = kills;
        gold = goldEarned;
        totalGold += goldEarned;

        if(!firstGameCompleted)
        {
            Debug.Log("First game completed");
            dateStarted = DateTime.Now;
            //unlock the first day at sea achievement. 
            GPGSAchievements.AchieveFirstDayAtSea();
        }

        showTutorial = false;
        gamesPlayed++;
        totalTimePlayed += roundDuration;

        GPGSAchievements.UpdateGoldEarned(goldEarned);
        GPGSAchievements.UpdateShipsDestroyed(shipsSunk);
        if (shipsSunk > 150) { 
            GPGSAchievements.AchieveDestroyer();
        }
    }
    public void CheckHighScore(int score)
    {
        this.score = score;
        if (score > highScore)
        {
            highScore = score;

            //update the GPG leaderboard
            GPGSLeaderboards.UpdateLeaderboardScore(highScore);
        }
    }
    public void UpgradeGun(int index, Upgrade data)
    {
        switch (index)
        {
            case 1:
                gun1.UpgradeGun(data);
                break;
            case 2:
                gun2.UpgradeGun(data);
                break;
            case 3:
                gun3.UpgradeGun(data);
                break;
            case 4:
                gun4.UpgradeGun(data);
                break;
        }

    }

    public string GetSummary()
    {
        return "Save Game: " + name
            + "\nRounds Played: " + gamesPlayed + " First Game Completed: " + firstGameCompleted
            + "\nHighest... difficulty: " + recordDifficulty + " Score: " + highScore + " Kills: " + highKills
            + "\n"
            + "\n Time Played: " + TimeSpan.FromSeconds(totalTimePlayed)
            + "\n Last save: " + lastSaved + " ver" + lastSaveGameVersion
            + "\n Save Created: " + dateStarted
            + "\n GPG Logged in: " + Social.localUser.authenticated;
    }

    /// <summary>
    ///  Compares two savegames and returns the one that is more recently saved.
    /// </summary>
    /// <param name="d1">The first SaveGame to be compared</param>
    /// <param name="d2">The second SaveGame to be compared</param>
    /// <returns>The more recent of the two passed SaveGames</returns>
    public static SaveGame CompareLastSaved(SaveGame d1, SaveGame d2)
    {
        return (DateTime.Compare(d1.lastSaved, d2.lastSaved) >= 0) ? d1 : d2;
    } 
}
