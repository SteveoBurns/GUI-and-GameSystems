using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Resolutions")]
    [SerializeField] private TMP_Dropdown resDropdown;
    Resolution[] resolutions;

    [Header("Quality")]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [Header("Fullscreen Toggle")]
    [SerializeField] private Toggle fullscreenToggle;

    [Header("Volume Sliders")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    public static bool loadGame = false;

    private void Awake()
    {
        // Finding all the screens resolution options, adding them to the dropdown and selecting the current resolution.
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
        // Loads any settings saved in playerprefs, or sets it to the current screen resolution
        if (PlayerPrefs.HasKey("Resolution"))
        {
            int resIndex = PlayerPrefs.GetInt("Resolution");
            resDropdown.value = resIndex;
            resDropdown.RefreshShownValue();
            SetResolution(resIndex);
        }
        else
        {
            resDropdown.value = currentResolution;
            resDropdown.RefreshShownValue();
        }
        #endregion

    }

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerPrefs();
    }

    /// <summary>
    /// Loads all the saved values for settings from playerprefs.
    /// </summary>
    public void LoadPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("Resolution"))
        {
            int resIndex = PlayerPrefs.GetInt("Resolution");
            resDropdown.value = resIndex;
            resDropdown.RefreshShownValue();
            SetResolution(resIndex);            
            
        }
        if (PlayerPrefs.HasKey("Quality"))
        {
            int quality = PlayerPrefs.GetInt("Quality");
            qualityDropdown.value = quality;
            Quality(quality);
        }
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            float volume = PlayerPrefs.GetFloat("MasterVolume");
            masterVolumeSlider.value = volume;
            VolumeSlider(volume);
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
            sfxVolumeSlider.value = sfxVolume;
            SFXSlider(sfxVolume);
        }
        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            bool _fullscreen = true;
            int fullscreen = PlayerPrefs.GetInt("Fullscreen");
            if (fullscreen == 1)
            {
                _fullscreen = true;
                fullscreenToggle.isOn = true;
            }
            else if (fullscreen == 0)
            {
                _fullscreen = false;
                fullscreenToggle.isOn = false;
            }

            Fullscreen(_fullscreen);

        }

    }


    /// <summary>
    /// Setting the chosen resolution from the dropdown on the UI
    /// </summary>
    public void SetResolution(int resIndex)
    {
        PlayerPrefs.SetInt("Resolution", resIndex);
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    /// <summary>
    /// Quits game in editor and build
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// Loads saved game by using load game bool to load saved data in the game scene
    /// </summary>
    public void LoadGame()
    {
        loadGame = true;
        SceneManager.LoadScene("Level");
    }

    /// <summary>
    /// Loads Main Menu
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    /// <summary>
    /// Loads the Customsation Scene
    /// </summary>
    public void NewGame()
    {
        SceneManager.LoadScene("Customize");
    }

    /// <summary>
    /// Continues the last game
    /// </summary>
    public void ContinueGame()
    {
        SceneManager.LoadScene("Level");
    }

    /// <summary>
    /// Sets the graphics quality level
    /// </summary>
    /// <param name="_index"></param>
    public void Quality(int _index)
    {
        QualitySettings.SetQualityLevel(_index);
        PlayerPrefs.SetInt("Quality", _index);
    }

    /// <summary>
    /// Turns fullscreen mode on and off
    /// </summary>    
    public void Fullscreen(bool _fullscreen)
    {
        PlayerPrefs.SetInt("Fullscreen", (_fullscreen ? 1 : 0));
        Screen.fullScreen = _fullscreen;
    }

    // Handles volume slider input and output by running value through a remap function before sending to the audio mixer.




    #region Volume Sliders
    public void VolumeSlider(float _volume)
    {
        PlayerPrefs.SetFloat("MasterVolume", _volume);
        _volume = VolumeRemap(_volume);
        audioMixer.SetFloat("masterVolume", _volume);
    }

    public void SFXSlider(float _volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", _volume);
        _volume = VolumeRemap(_volume);
        audioMixer.SetFloat("sfxVolume", _volume);
    }

    public float VolumeRemap(float value)
    {
        return -40 + (value - 0) * (20 - -40) / (1 - 0);
    }
    #endregion


    
}
