using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Obj;
    [SerializeField] private GameObject ClearPanel;
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private List<GameObject> Interface;
    public static Information Instance;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
    }
    public void EndGame()
    {
        Time.timeScale = 1;
        ClearPanel.SetActive(true);
        UIPanel.transform.position = Vector2.Lerp(UIPanel.transform.position, new Vector2(960, 540), 10.0f);
        AudioSource audio = Camera.main.GetComponent<AudioSource>();
        audio.Stop();
        audio.PlayOneShot(Resources.Load("Audio/crash") as AudioClip);
        audio.PlayOneShot(Resources.Load("Audio/End Defeat") as AudioClip);


    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        Debug.Log("0");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void RetrunHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void GamePause()
    {
        if (Time.timeScale == 0)
        {
            Obj.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Obj.SetActive(true);
            Time.timeScale = 0;
        }
       
    }

    public void StageClear()
    {
        Time.timeScale = 0;

        ClearPanel.SetActive(true);

        AudioSource audio = Camera.main.GetComponent<AudioSource>();
        audio.Stop();
        audio.PlayOneShot(Resources.Load("Audio/End Win") as AudioClip);
        
        
    }
    IEnumerator Victory(int _Score)
    {
        Debug.Log("1");
        yield return new WaitForSeconds(2.0f);
        Debug.Log("2");
        AudioSource audio = Camera.main.GetComponent<AudioSource>();

        switch (_Score)
        {
        case 1:
        audio.PlayOneShot(Resources.Load("Audio/Hat1") as AudioClip);
            break;
        case 2:
        audio.PlayOneShot(Resources.Load("Audio/Hat1") as AudioClip);
        yield return new WaitForSeconds(1.0f);
                audio.PlayOneShot(Resources.Load("Audio/Hat2") as AudioClip);
            break;
        case 3:
        Debug.Log("3");
                audio.PlayOneShot(Resources.Load("Audio/Hat1") as AudioClip);
        yield return new WaitForSeconds(1.0f);
                audio.PlayOneShot(Resources.Load("Audio/Hat2") as AudioClip);
        yield return new WaitForSeconds(1.0f);
                audio.PlayOneShot(Resources.Load("Audio/Hat3") as AudioClip);
            break;
        }
    }
    public void NextStage()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
