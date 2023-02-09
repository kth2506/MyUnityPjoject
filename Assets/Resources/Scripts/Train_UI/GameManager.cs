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
    [SerializeField] private GameObject PauseButton;
    [SerializeField] private GameObject StopButton;
    [SerializeField] private GameObject StarImage;
    private GameObject Player;
    public static GameManager Instance;
    private Camera cam;

    private float shakeTime = 2.0f;
    private float shakeAmount = 10.0f;

    Animator animator;
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
        cam = Camera.main;
    }

   

    public IEnumerator EndGame()
    {
        cam.GetComponent<FollowCamera>().enabled = false;

        Vector3 originPosition = cam.transform.localPosition;
        float elaspedTime = 0.0f;

        while (elaspedTime < shakeTime)
        {
            Vector3 randomPoint = originPosition + Random.insideUnitSphere * shakeAmount;
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, randomPoint, Time.deltaTime);
            yield return new WaitForSeconds(0.1f);

            elaspedTime += 0.1f;
        }
        cam.transform.localPosition = originPosition;


        Time.timeScale = 0;

        OffUI();
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
        OffUI();
        FindObjectOfType<Information>().SetStar();
        AudioSource audio = Camera.main.GetComponent<AudioSource>();
        audio.Stop();
        audio.PlayOneShot(Resources.Load("Audio/End Win") as AudioClip);
        animator = UIPanel.GetComponent<Animator>();
        animator.SetBool("isClear", true);

        StartCoroutine(CheckAnimationState());
        




    }


    IEnumerator CheckAnimationState()
    {
        float fTime = 0.0f; 
        while (fTime < 2.0f)
        {
            Debug.Log(fTime);
            fTime += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        StarImage.GetComponent<Animator>().SetInteger("Star", FindObjectOfType<Timer>().Star);
        Time.timeScale = 0;
        Debug.Log("End");
        
    }


    private void OffUI()
    {
        StopButton.SetActive(false);
        PauseButton.SetActive(false);
        ClearPanel.SetActive(true);
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
        Player = FindObjectOfType<TrainManager>().gameObject;
        Player.GetComponent<TrainManager>().SlowlyStop();
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
