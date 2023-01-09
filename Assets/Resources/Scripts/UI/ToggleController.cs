using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleController : MonoBehaviour
{
     private Animator Anim;
   
    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Anim.SetBool("Check", SlideController.Instance.MoveCheck);
        
    }
    public void ToggleButton()
    {
        SlideController.Instance.MoveCheck = SlideController.Instance.MoveCheck ? false : true;
        //Check = Check ? false : true;

        Anim.SetBool("Check", SlideController.Instance.MoveCheck);


        if (SlideController.Instance.MoveCheck)
            StartCoroutine(SlideController.Instance.SlideInCoroutine_01());
        else
            StartCoroutine(SlideController.Instance.SlideOutCoroutine_01());

    }

}
