using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public List<Quest> possibleQuests;
    public List<Quest> currentQuestViewer;
    public static List<Quest> currentQuests;
    public static List<Quest> completedQuests;

    private void Awake()
    {
        SceneManager.activeSceneChanged += HandleSceneChanged;

        currentQuests = new List<Quest>();
        completedQuests = new List<Quest>();
    }

    private void Update()
    {
        currentQuestViewer = currentQuests;
    }

    public void StartQuestManager()
    {
        Debug.Log("Starting quest manager!!");
        if (OptionsHolder.instance.save.lastSaved < System.DateTime.Today)
        {
            Debug.Log("New day, new quests!!");
            StartQuest();
            StartQuest();
        }
    }

    public static void CompleteQuest(Quest quest)
    {
        Debug.Log("Quest completed: " + quest + "!");
        completedQuests.Add(quest);
    }

    private void HandleSceneChanged(Scene current, Scene next)
    {
        Debug.Log(next.name);
        if (next.name == "sc_MainMenu")
        {
            Debug.Log("Rewarding quests!");
            foreach (Quest i in completedQuests)
            {
                OptionsHolder.instance.save.totalPearls += i.pearlReward;
                OptionsHolder.instance.save.totalGold += i.goldReward;

                currentQuests.Remove(i);
            }
            Debug.Log("Pearls are now " + OptionsHolder.instance.save.totalPearls);
        }
    }


    [ContextMenu("Start New Quest")]
    private void StartQuest()
    {
        Quest newQuest = possibleQuests[Random.Range(0, possibleQuests.Count)];

        newQuest.StartQuest();

        currentQuests.Add(newQuest);
    }
}
