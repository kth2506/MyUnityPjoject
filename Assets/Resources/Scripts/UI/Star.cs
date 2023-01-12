using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    static private Star Instance = null;
    private Image image;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = Resources.Load
            ("Images/stars" + Information.Instance.GetStar(transform.name)) as Sprite;
    }


}
