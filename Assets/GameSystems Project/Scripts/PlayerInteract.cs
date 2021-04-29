using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
   

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
                    Dialogue npcDialogue = hit.transform.GetComponent<Dialogue>();
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
