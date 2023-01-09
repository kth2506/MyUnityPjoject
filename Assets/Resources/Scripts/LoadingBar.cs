using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingBar : MonoBehaviour
{
    private Image HealthBar = null;
    private float CrossLine;
    private void Awake()
    {
        HealthBar = GameObject.Find("HealthBar").GetComponent<Image>();
    }
    IEnumerator Start()
    {
        CrossLine = 0.7f;
        HealthBar.fillAmount = 0;
        float Frame = 0.5f;
        while(true)
        {

            if (CrossLine > HealthBar.fillAmount)
            {
                if (HealthBar.fillAmount >= 0.85f)
                    Frame = 2.5f;
                    HealthBar.fillAmount += Time.deltaTime * 0.3f;
                if (HealthBar.fillAmount >= 1.0f)
                    break;
            }
            else
            {
                yield return new WaitForSeconds(Frame);
                CrossLine += 0.1f;
            }
            yield return null;
        }

        Debug.Log("Next Scene!!");
        SceneManager.LoadScene("LoadingScene1");
    }

}
