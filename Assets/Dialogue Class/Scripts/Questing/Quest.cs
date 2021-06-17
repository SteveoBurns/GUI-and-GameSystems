using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public enum QuestStage
    {
        Locked,         //We can't get the quest
        Unlocked,       //The quest is now available
        InProgress,     //We have accepted the quest
        RequirementsMet, //we have done all the things in the quest, need to hand it in.
        Complete        //Quest is now done and we can ignore
    }

    [System.Serializable]
    public abstract class Quest : MonoBehaviour
    {
        public string title;
        [TextArea] public string description;
        public Reward reward;

        public QuestStage stage;

        public int requiredLevel;
        [Tooltip("The title of the previous quest in the chain")]
        public string previousQuest;
        [Tooltip("The title of the quests to be unlocked")]
        public string[] unlockedQuests;

        public abstract bool CheckQuestCompletion();
    }

    [System.Serializable]
    public struct Reward
    {
        public Item rewardItem;
        public float experience;
        public int gold;
        public float factionIncrease;
    }
}
