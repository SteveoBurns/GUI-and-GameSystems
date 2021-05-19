using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource click;


    /// <summary>
    /// Plays Click sound.
    /// </summary>
    public void PlayClick()
    {
        click.Play();
    }

    
}
