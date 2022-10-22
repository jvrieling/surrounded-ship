using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : ScriptableObject
{
    private int questId;

    public int goldReward;
    public int pearlReward;

    public virtual void StartQuest()
    {

    }

    public virtual void CompleteQuest()
    {
        OptionsHolder.instance.save.totalGold += goldReward;
        OptionsHolder.instance.save.totalPearls += pearlReward;
    }
}
