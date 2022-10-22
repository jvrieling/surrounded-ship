using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collect Gold", menuName = "Quests/Collect Gold", order = 2)]
public class CollectGold : Quest
{
    public int goldRequired;
    private int goldEarned;

    public override void StartQuest()
    {
        base.StartQuest();
        ScoreManager.OnGoldEarned += HandleGoldEarned;
    }

    private void UnsubscribeFromEvents()
    {
        ScoreManager.OnGoldEarned -= HandleGoldEarned;
    }

    private void HandleGoldEarned(int amount)
    {
        Debug.Log("Quest advanced to " + goldEarned);
        goldEarned += amount;

        if(goldEarned >= goldRequired)
        {
            ScoreManager.OnGoldEarned -= HandleGoldEarned;
            QuestManager.CompleteQuest(this);
        }
    }
}
