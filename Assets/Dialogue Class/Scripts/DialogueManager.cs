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

    /// <summary>
    /// Loading the NPC's dialogue
    /// </summary>
    /// <param name="dialogue">Takes in the NPC's dialogue</param>
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
                
            }
            i++;
        }

        //Spawn the goodbye button
        Button byeButton = Instantiate(buttonPrefab, buttonPanel).GetComponent<Button>();
        byeButton.GetComponentInChildren<Text>().text = dialogue.goodbye.topic;
        byeButton.onClick.AddListener(delegate { EndConversation() ; });


    }

    /// <summary>
    /// Function for ending the dialogue and closing panels.
    /// </summary>
    public void EndConversation()
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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }    
    }

    /// <summary>
    /// Shows response when clicking the options buttons. If dialogue option has a next dialogue, shows that.
    /// </summary>
    /// <param name="dialogueNum"></param>
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

    /// <summary>
    /// Destroys all children in a parent transform
    /// </summary>
    public void CleanUpButtons()
    {
        foreach (Transform child in buttonPanel)
        {
            Destroy(child.gameObject);
        }
    }
        
}
