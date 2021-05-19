using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)        
            PauseGame();        
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
            UnPauseGame();
        if(!isPaused && !DialogueManager.inDialogue)
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void UnPauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

    }
}
