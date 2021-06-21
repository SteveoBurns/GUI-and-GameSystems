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

        public List<Quest> activeQuests = new List<Quest>();
        private Dictionary<string, Quest> questDatabase = new Dictionary<string, Quest>();

        public List<Quest> GetActiveQuests() => activeQuests;

        [NonSerialized] public Quest selectedQuest = null;

        [Header("Quest UI")]
        [SerializeField] private Button buttonPrefab;
        [SerializeField] private GameObject activeQuestsGameObject;
        [SerializeField] private GameObject questsContent;
        [SerializeField] private GameObject questUIButtons;
        [SerializeField] private GameObject claimRewardButton;
        [SerializeField] private GameObject rewardPanel;
        [SerializeField] private Text rewardText;
        [SerializeField] private GameObject requirementsMetText;
        [SerializeField] private GameObject cantAcceptQuestPanel;
        [SerializeField] private Text cantAcceptQuestText;
        [SerializeField] private GameObject foundQuestItemPanel;
        [SerializeField] private Transform spawnLocation;

        [Header("Selected Quest Display")]
        [SerializeField] private Text questTitle;
        [SerializeField] private Text questDescription;

        [SerializeField] private Inventory inventory;



        private void Awake()
        {
            // If the instance isn't set, set it to this gameObject
            if (instance == null)
            {
                instance = this;
            }
            // If the instance is already set and it isn't this, destroy this gameobject.
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            activeQuestsGameObject.SetActive(false);
            claimRewardButton.SetActive(false);
            requirementsMetText.SetActive(false);
            cantAcceptQuestPanel.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (activeQuestsGameObject.activeSelf)
                {
                    activeQuestsGameObject.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = false;
                }
                else
                {
                    
                    activeQuestsGameObject.SetActive(true);
                    DisplayActiveQuestsCanvas();
                    selectedQuest = null;
                    DisplaySelectedQuestOnCanvas(selectedQuest);
                    // Set buttons unactive when accessing quests with tab.
                    questUIButtons.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }

        /// <summary>
        /// Adds the reward from the passed quest into the inventory
        /// </summary>
        /// <param name="quest">the completed quest</param>
        private void GiveReward(Quest quest)
        {
            rewardPanel.SetActive(true);
            rewardText.text = "Money: " + quest.reward.rewardItem.Amount.ToString();

            inventory.AddItem(quest.reward.rewardItem);
        }


        /// <summary>
        /// Gets the quest from the quest database
        /// </summary>
        /// <param name="title">Quest Title</param>
        /// <returns>the corresponding quest</returns>
        public Quest GetQuest(string title)
        {
            questDatabase.TryGetValue(title, out Quest quest);
            return quest;
        }

        //claim rewqards button
        public void ClaimRewardsButton()
        {
            CompleteQuest(selectedQuest.title);

            // activate game object to inform player of quest rewards
        }

        /// <summary>
        /// Function for the decline quests button, closes the quest panel
        /// </summary>
        public void DeclineQuestButton()
        {
            activeQuestsGameObject.SetActive(false);
        }

        /// <summary>
        /// Function for the Accept quest button. 
        /// </summary>
        public void AcceptQuestButton()
        {
            if (selectedQuest != null)
            {
                if (selectedQuest.requiredLevel <= PlayerStats.ThePlayerStats.levelInt)
                {
                    AcceptQuest(selectedQuest.title);
                    selectedQuest = null;
                    DisplayQuestsCanvas();
                    DisplaySelectedQuestOnCanvas(selectedQuest);
                }
                else
                {
                    cantAcceptQuestPanel.SetActive(true);
                    cantAcceptQuestText.text = "You need to be level " + selectedQuest.requiredLevel.ToString() + " to accept this quest";
                    Debug.Log("You need to be " + selectedQuest.requiredLevel.ToString() + " to accept this quest");
                }

            }
            else
                Debug.Log("No quest selected");
        }



        /// <summary>
        /// Called from playerInteract
        /// </summary>
        public void LoadQuests()
        {
            if (activeQuestsGameObject.activeSelf)
            {
                activeQuestsGameObject.SetActive(false);
            }
            else
            {
                activeQuestsGameObject.SetActive(true);
                //Set the buttons visable when accessing quests from the quest board
                questUIButtons.SetActive(true);
                DisplayQuestsCanvas();
                
            }
        }

        /// <summary>
        /// Displays unlocked quests to the canvas
        /// </summary>
        private void DisplayQuestsCanvas()
        {
            DestroyAllChildren(questsContent.transform);
            foreach (Quest quest in quests)
            {           
                // Put a test in here to test if the quest hass been unlocked yet??
                if (quest.stage == QuestStage.Unlocked ||quest.stage == QuestStage.InProgress || quest.stage == QuestStage.RequirementsMet)
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

        /// <summary>
        /// Only displays the quests in the active quests list
        /// </summary>
        private void DisplayActiveQuestsCanvas()
        {
            DestroyAllChildren(questsContent.transform);
            foreach (Quest quest in activeQuests)
            {
                // Put a test in here to test if the quest hass been unlocked yet??
                if (quest.stage == QuestStage.InProgress || quest.stage == QuestStage.RequirementsMet)
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

        /// <summary>
        /// Displays the selected quest to the canvas
        /// </summary>
        /// <param name="_quest"></param>
        public void DisplaySelectedQuestOnCanvas(Quest _quest)
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
                if (_quest.stage == QuestStage.RequirementsMet)
                {
                    requirementsMetText.SetActive(true);
                    claimRewardButton.SetActive(true);
                }
                else
                {
                    requirementsMetText.SetActive(false);
                    claimRewardButton.SetActive(false);
                }
            }


        }

        /// <summary>
        /// Destroys all transforms in the parent transform
        /// </summary>
        /// <param name="parent">parent transform</param>
        public void DestroyAllChildren(Transform parent)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }


        /// <summary>
        /// Upadtes the passed quest.
        /// </summary>
        /// <param name="_id">quest title</param>
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
                if(quest.stage == QuestStage.RequirementsMet)
                {
                    GameObject spawnPanel = Instantiate(foundQuestItemPanel, spawnLocation);
                    Destroy(spawnPanel, 3f);
                }
            }
        }

        /// <summary>
        /// Pass in the quest title. Completes the quest and gives reward
        /// </summary>
        /// <param name="_id">quest title</param>
        public void CompleteQuest(string _id)
        {
            if(questDatabase.TryGetValue(_id, out Quest quest))
            {
                if (quest.stage == QuestStage.RequirementsMet)
                {
                    quest.stage = QuestStage.Complete;
                    //Give the player their reward
                    GiveReward(quest);

                    activeQuests.Remove(quest);

                    //Find all related quests that are going to be unlocked
                    foreach (string questId in quest.unlockedQuests)
                    {
                        if (questDatabase.TryGetValue(questId, out Quest unlocked))
                        {
                            //Update their stages
                            unlocked.stage = QuestStage.Unlocked;                            
                        }
                    }
                    selectedQuest = null;
                    DisplayQuestsCanvas();
                    DisplaySelectedQuestOnCanvas(selectedQuest);


                }
                else
                    Debug.Log("You havent completed this quest yet");

                
            }
        }

        /// <summary>
        /// Accecpts the passed quest, adds it to sctive quests and changes its status to InProgress.
        /// </summary>
        /// <param name="_id">quest.Title</param>
        public void AcceptQuest(string _id)
        {
            if(questDatabase.TryGetValue(_id, out Quest quest))
            {
                if(quest.stage == QuestStage.Unlocked)
                {
                    quest.stage = QuestStage.InProgress;
                    activeQuests.Add(quest);
                }
                if (quest.stage == QuestStage.InProgress)
                {
                    UpdateQuest(quest.title);
                }
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
