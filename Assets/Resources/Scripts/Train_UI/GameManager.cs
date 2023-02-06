using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject ClearPanel;
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private List<GameObject> Interface;
    [SerializeField] private Transform StartPosition;
    [SerializeField] private Transform EndPosition;
    private GameObject Player;
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
       
    }



    public void EndGame()
    {
        Time.timeScale = 0;
        ClearPanel.SetActive(true);
        //UIPanel.transform.position = Vector2.Lerp(UIPanel.transform.position, new Vector2(960, 540), 10.0f);
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
            PausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
        }

    }

    public void StageClear()
    {

        PausePanel.SetActive(true);
        Panel.SetActive(false);
        FindObjectOfType<Information>().SetStar();
        AudioSource audio = Camera.main.GetComponent<AudioSource>();
        audio.Stop();
        audio.PlayOneShot(Resources.Load("Audio/End Win") as AudioClip);
        Victory();
    }

    public void Victory()
    {
        UIPanel.GetComponent<Animator>().SetBool("isClear", true);
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        Panel.SetActive(false);
    }

    public void NextStage()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayerStop()
    {
        Player = FindObjectOfType<CinemachineController>().gameObject;
        StartCoroutine(Player.GetComponent<CinemachineController>().SlowlyStop());
    }

    public void SceneOption()
    {
        SceneManager.LoadScene("Option");
    }
    public void SceneMain()
    {
        SceneManager.LoadScene("Stage");
    }
}
