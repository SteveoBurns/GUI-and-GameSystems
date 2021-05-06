using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2))
            {
                if (hit.transform.tag == "NPC")
                {
                    Dialogue npcDialogue = hit.transform.GetComponents<Dialogue>()[0];
                    if (npcDialogue)
                    {
                        DialogueManager.theManager.LoadDialogue(npcDialogue);
                        Cursor.lockState = CursorLockMode.None;
                    }
                }
            }
            
        }
    }
}
