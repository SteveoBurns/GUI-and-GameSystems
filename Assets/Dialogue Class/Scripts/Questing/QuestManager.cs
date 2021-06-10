using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Quests
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager instance = null;

        public List<Quest> quests = new List<Quest>();

        private List<Quest> activeQuests = new List<Quest>();
        private Dictionary<string, Quest> questDatabase = new Dictionary<string, Quest>();

        public List<Quest> GetActiveQuests() => activeQuests;

        [NonSerialized] public Quest selectedQuest = null;

        [Header("Quest UI")]
        [SerializeField] private Button buttonPrefab;
        [SerializeField] private GameObject questsGameObject;
        [SerializeField] private GameObject questsContent;

        [Header("Selected Quest Display")]
        [SerializeField] private Text questTitle;
        [SerializeField] private Text questDescription;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (questsGameObject.activeSelf)
                {
                    questsGameObject.SetActive(false);
                }
                else
                {
                    questsGameObject.SetActive(true);
                    DisplayQuestsCanvas();
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }

        /// <summary>
        /// Called from playerInteract
        /// </summary>
        public void LoadQuests()
        {
            if (questsGameObject.activeSelf)
            {
                questsGameObject.SetActive(false);
            }
            else
            {
                questsGameObject.SetActive(true);
                DisplayQuestsCanvas();
                
            }
        }

        private void DisplayQuestsCanvas()
        {
            DestroyAllChildren(questsContent.transform);
            foreach (Quest quest in quests)
            {           
                // Put a test in here to test if the quest hass been unlocked yet??
                if (quest.stage == QuestStage.Unlocked)
                {                               
                    Button buttonGo = Instantiate<Button>(buttonPrefab, questsContent.transform);
                    Text buttonText = buttonGo.GetComponentInChildren<Text>();
                    buttonGo.name = quest.title + " button";
                    buttonText.text = quest.title;

                    Quest _quest = quest;
                    buttonGo.onClick.AddListener(delegate { DisplaySelectedQuestOnCanvas(_quest); });
                }
            }
        }

        void DisplaySelectedQuestOnCanvas(Quest _quest)
        {
            selectedQuest = _quest;

            if (_quest == null)
            {                
                questTitle.text = "";
                questDescription.text = "";
            }
            else
            {
                questTitle.text = selectedQuest.title;
                questDescription.text = $" {selectedQuest.description} \n Required Level: {selectedQuest.requiredLevel} \n Reward: {selectedQuest.reward.gold} ";

            }


        }

        void DestroyAllChildren(Transform parent)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }


        //pass a player into this function
        public void UpdateQuest(string _id)
        {
            // This is the same as checking if the key exists, if it does, return it.
            // Trygetvalue returns a bool if it successfully got the item
            if(questDatabase.TryGetValue(_id, out Quest quest))
            {
                if(quest.stage == QuestStage.InProgress)
                {
                    //Check if the quest is ready to complete, if it is , update the stage, otherwise retain the stage.
                    quest.stage = quest.CheckQuestCompletion() ? QuestStage.RequirementsMet : quest.stage;
                }
            }
        }

        // Take in the player
        public void CompleteQuest(string _id)
        {
            if(questDatabase.TryGetValue(_id, out Quest quest))
            {
                quest.stage = QuestStage.Complete;
                activeQuests.Remove(quest);

                //Find all related quests that are going to be unlocked
                foreach (string questId in quest.unlockedQuests)
                {
                    if(questDatabase.TryGetValue(questId, out Quest unlocked))
                    {
                        //Update their stages
                        unlocked.stage = QuestStage.Unlocked;

                        // Display unlocked quests
                    }
                       
                }

                //Give the player their reward
                // quest.reward
            }
        }

        public void AcceptQuest(string _id)
        {
            if(questDatabase.TryGetValue(_id, out Quest quest))
            {
                if(quest.stage == QuestStage.Unlocked)
                {
                    quest.stage = QuestStage.InProgress;
                    activeQuests.Add(quest);
                }
            }
        }


        private void Awake()
        {
            // If the instance isn't set, set it to this gameObject
            if(instance == null)
            {
                instance = this;
            }
            // If the instance is already set and it isn't this, destroy this gameobject.
            else if(instance != this)
            {
                Destroy(gameObject);
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            //Clear the list before adding the found quests to the list
            quests.Clear();
            // Find all the quests in the game
            quests.AddRange(FindObjectsOfType<Quest>());


            // For each function is specific to list types that just functions like a for each loop with lambdas.. cause lambdas are cool..bro!
            quests.ForEach(quest =>
            {
                // If the quest doesn't already exist store it in the database
                if (!questDatabase.ContainsKey(quest.title))
                    questDatabase.Add(quest.title, quest);
                else
                    Debug.LogError("That quest already exists you dingus");
            });
        }

        
    }
}
