using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image loadingBar;      //this will show the loading progress
    [SerializeField] private string sceneToLoad;    //this is the scene we will be loading dynamically
    [SerializeField] private GameObject loadingBarBackground;
    [SerializeField] private GameObject canvasCamera;

        
    // Start is called before the first frame update
    void Start()
    {
        
        loadingBarBackground.SetActive(true);
        
        StartCoroutine(LoadSceneAsync());


    }

    private IEnumerator LoadSceneAsync()
    {
        //reset the loading bar in case it isnt done
        loadingBar.fillAmount = 0;

        //start the load of the scene and tell it to activate when done
        AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
        sceneLoadOperation.allowSceneActivation = true;

        // loop continously until the operation is complete
        while (!sceneLoadOperation.isDone)
        {
            //update the progress bar and wait until the next frame
            loadingBar.fillAmount = sceneLoadOperation.progress;
            yield return null;

        }

        //update the loading bar to full and wait half a second
        loadingBar.fillAmount = 1;
        yield return new WaitForSeconds(1f);

       
       
        
    }
}
