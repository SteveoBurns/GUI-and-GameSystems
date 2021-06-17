using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quests;

public class FetchQuestItem : MonoBehaviour
{
    public string questTitle;
    private FetchQuest quest;

    public void UpdateQuest()
    {
        quest = QuestManager.instance.GetQuest(questTitle) as FetchQuest;
        quest.gotItem = true;
        quest.CheckQuestCompletion();
        QuestManager.instance.UpdateQuest(questTitle);
    }
}
