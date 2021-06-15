using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles character interaction with objects
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {            
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5))
            {                
                // If hit NPC start dialogue with them.
                if (hit.transform.tag == "NPC")
                {
                    Dialogue npcDialogue = hit.transform.GetComponents<Dialogue>()[0];
                    if (npcDialogue)
                    {
                        // Load dialogue and set cursor mode.
                        DialogueManager.theManager.LoadDialogue(npcDialogue);
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                }

                if (hit.transform.tag == "Quest Board")
                {
                    Quests.QuestManager.instance.LoadQuests();
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }

                DroppedItem droppedItem = hit.collider.gameObject.GetComponent<DroppedItem>();
                if (droppedItem != null)
                {
                    inventory.AddItem(droppedItem.item);
                    Destroy(hit.collider.gameObject);
                }
            }
             void OnDrawGizmos()
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.forward);
            }
            
        }
    }

}
