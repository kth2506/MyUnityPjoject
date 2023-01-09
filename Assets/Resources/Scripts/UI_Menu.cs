using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;    
using UnityEngine.SceneManagement;


public class UI_Menu : MonoBehaviour
{
    public AudioMixer mainMixer;
    public void SetVolume(float _Volume)
    {
        mainMixer.SetFloat("volume", _Volume);
    }

    public void Stage1()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void Stage3()
    {
        SceneManager.LoadScene("Stage3");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
