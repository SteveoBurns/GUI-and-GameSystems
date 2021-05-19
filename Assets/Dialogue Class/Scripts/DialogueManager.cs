using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform buttonPanel;
    [SerializeField] GameObject responsePanel;
    [SerializeField] Text responseText;

    public static bool inDialogue = false;

    GameObject dialoguePanel;

    Dialogue currentDialogue;

    public static DialogueManager theManager;

    private void Awake()
    {
        inDialogue = false;

        dialoguePanel = transform.Find("Scroll View").gameObject;
        dialoguePanel.SetActive(false);

        if (theManager == null)
        {
            theManager = this;
        }
        else
        {
            Destroy(this);
        }
    }


    public void LoadDialogue(Dialogue dialogue)
    {
        inDialogue = true;
        dialoguePanel.SetActive(true);
        responsePanel.SetActive(true);
        CleanUpButtons();
        currentDialogue = dialogue;

        responseText.text = dialogue.greeting;
        print(dialogue.greeting);

        int i = 0;
        foreach (LineOfDialogue item in dialogue.dialogueOptions)
        {
            float? currentApproval = FactionsManager.theManagerOfFactions.FactionsApproval(dialogue.faction);
            if (currentApproval != null && currentApproval > item.minApproval)
            {
                Button spawnedButton = Instantiate(buttonPrefab, buttonPanel).GetComponent<Button>();
                spawnedButton.GetComponentInChildren<Text>().text = item.topic;

                int i2 = i;
                spawnedButton.onClick.AddListener(delegate { ButtonClick(i2); });
                //i++;
                //print(item.topic);
            }
            i++;
        }

        //Spawn the goodbye button
        Button byeButton = Instantiate(buttonPrefab, buttonPanel).GetComponent<Button>();
        byeButton.GetComponentInChildren<Text>().text = dialogue.goodbye.topic;
        byeButton.onClick.AddListener(delegate { EndConversation() ; });


    }

    void EndConversation()
    {
        responsePanel.SetActive(true);
        responseText.text = currentDialogue.goodbye.response;
        print(currentDialogue.goodbye.response);
            
        if(currentDialogue.goodbye.nextDialogue != null)
        {
            LoadDialogue(currentDialogue = currentDialogue.goodbye.nextDialogue);
        }
        else
        {
            CleanUpButtons();
            dialoguePanel.SetActive(false);
            inDialogue = false;
        }    
    }

    public void ButtonClick(int dialogueNum)
    {
        //Changing the factions approval when clicking the dialogue option
        FactionsManager.theManagerOfFactions.FactionsApproval(currentDialogue.faction, currentDialogue.dialogueOptions[dialogueNum].changeApproval);

        print(currentDialogue.dialogueOptions[dialogueNum].response);
        responsePanel.SetActive(true);
        responseText.text = currentDialogue.dialogueOptions[dialogueNum].response;
        if (currentDialogue.dialogueOptions[dialogueNum].nextDialogue != null)
        {
            LoadDialogue(currentDialogue = currentDialogue.dialogueOptions[dialogueNum].nextDialogue);
        }

    }

    public void CleanUpButtons()
    {
        foreach (Transform child in buttonPanel)
        {
            Destroy(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
