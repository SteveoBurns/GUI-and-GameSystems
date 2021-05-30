using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    
    [SerializeField] private TMP_Dropdown resDropdown;
    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        #region Resolutions
        resolutions =  Screen.resolutions;

        List<string> resOptions = new List<string>();
        int currentResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            resOptions.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }

        resDropdown.AddOptions(resOptions);
        resDropdown.value = currentResolution;
        resDropdown.RefreshShownValue();
        #endregion
    }

    public void SetResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Customize");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void Quality(int _index)
    {
        QualitySettings.SetQualityLevel(_index);
    }

    public void Fullscreen(bool _fullscreen)
    {
        Screen.fullScreen = _fullscreen;
    }

    #region Volume Sliders
    public void VolumeSlider(float _volume)
    {
        _volume = VolumeRemap(_volume);
        audioMixer.SetFloat("masterVolume", _volume);
    }

    public void SFXSlider(float _volume)
    {
        _volume = VolumeRemap(_volume);
        audioMixer.SetFloat("sfxVolume", _volume);
    }

    public float VolumeRemap(float value)
    {
        return -60 + (value - 0) * (20 - -60) / (1 - 0);
    }
    #endregion



    // Update is called once per frame
    void Update()
    {
        
    }
}
