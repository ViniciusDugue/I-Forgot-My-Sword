using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    // private void OnEnable() 
    // {
        
    // }
    public void StartMainMenuAudio()
    {
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void StopMainMenuAudio()
    {
        audioSource.Stop();
    }
}