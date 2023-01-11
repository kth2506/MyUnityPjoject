using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Image image;
    public float duration;
    private float currentTime;
    public int Star = 0;



    public IEnumerator Start()
    {
        currentTime = duration;
        while(image.fillAmount > 0)
        {
            currentTime -= 0.1f;
            image.fillAmount = (currentTime / duration);
            if (image.fillAmount > 0.22f)
            {
                Star = 3;
            }
            else if (image.fillAmount > 0.08f && image.fillAmount <= 0.22f)
                Star = 2;
            else
                Star = 1;
            yield return new WaitForSeconds(0.1f);
        }
        image.fillAmount = 0;
        currentTime = 0.0f;
    }


 
}
