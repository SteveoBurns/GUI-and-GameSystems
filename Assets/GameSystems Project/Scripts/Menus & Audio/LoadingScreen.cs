using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image loadingBar;      //this will show the loading progress
    [SerializeField] private string sceneToLoad;    //this is the scene we will be loading dynamically
    //[SerializeField] private GameObject loadingBarBackground;    
    [SerializeField] private GameObject loadingBackground;    
    [SerializeField] private GameObject eventSystem;    
    [SerializeField] private GameObject canvasCamera;   //this wll be disabled when the new scene is loaded

  

    // Start is called before the first frame update
    void Start()
    {
        //swap out the objects being shown on the screen
        //loadingBarBackground.SetActive(false);        
        canvasCamera.SetActive(true);

        StartCoroutine(LoadSceneAsync());

    }

    private IEnumerator LoadSceneAsync()
    {
        //reset the loading bar in case it isnt done
        loadingBar.fillAmount = 0;

        //start the load of the scene and tell it to activate when done
        AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        sceneLoadOperation.allowSceneActivation = false;

        // loop continously until the operation is complete
        while (!sceneLoadOperation.isDone)
        {
            //update the progress bar and wait until the next frame
            loadingBar.fillAmount = sceneLoadOperation.progress * 0.1f;

            if (sceneLoadOperation.progress >= .9f)
            {
                loadingBar.fillAmount = 1;
                yield return new WaitForSeconds(1f);
                sceneLoadOperation.allowSceneActivation = true;
            }

            yield return null;

        }

        //update the loading bar to full and wait half a second
        

        /*
        //find and activate the character controller
        CharController_Motor motor = FindObjectOfType<CharController_Motor>();
        motor.Initialize();
        */

        //disable the camera and the loading bar
        canvasCamera.SetActive(false);
        //loadingBarBackground.SetActive(false);
        loadingBackground.SetActive(false);
        Destroy(eventSystem);
        SceneManager.UnloadSceneAsync("Loading Screen");

    }
}
