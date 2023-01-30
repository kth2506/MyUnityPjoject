using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;    


public class UI_Menu : MonoBehaviour
{
    public AudioMixer mainMixer;
    public void SetVolume(float _Volume)
    {
        mainMixer.SetFloat("volume", _Volume);
    }
  
    public void QuitGame()
    {
        Application.Quit();
    }

}
