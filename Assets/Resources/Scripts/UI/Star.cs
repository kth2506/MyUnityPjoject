using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    //static private Star Instance = null;
    [SerializeField] private Image image;
    //private void Awake()
    //{
    //    if (object.ReferenceEquals(Instance,null))
    //        Instance = this;
    //}

    private void Start()
    {
        image.sprite = Resources.Load<Sprite>("Images/stars" + FindObjectOfType<Information>().GetStar(transform.name).ToString());
    }

   

}
