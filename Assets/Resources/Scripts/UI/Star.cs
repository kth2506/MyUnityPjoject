using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    [SerializeField] private Image image;
    
    private void Start()
    {
        image.sprite = Resources.Load<Sprite>("Images/stars" + FindObjectOfType<Information>().GetStar(transform.name).ToString());
    }

   

}
