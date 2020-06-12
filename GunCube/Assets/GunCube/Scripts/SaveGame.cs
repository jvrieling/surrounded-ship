using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Options", menuName = "Options", order = 3)]
public class SaveGame : ScriptableObject
{
    public float difficulty;
    public float recordDifficulty;

    public int score;
    public float highScore;

    public ShooterData gun1;
    public ShooterData gun2;
    public ShooterData gun3;
    public ShooterData gun4;

    public void CompleteRound(float diff)
    {
        difficulty = diff;
        if (diff > recordDifficulty) recordDifficulty = diff;
    }
    public void CheckHighScore(int score)
    {
        this.score = score;
        if (score > highScore) highScore = score;
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
}
