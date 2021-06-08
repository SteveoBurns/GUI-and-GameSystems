using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    // This will be called from the Death panel Animation when it has finished fading to black.
    public void Reload()
    {
        SceneManager.LoadScene("Level");
    }
}
