using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideController : MonoBehaviour
{
    static public SlideController Instance = null;

    [SerializeField] private RectTransform TargetUITransfrom;

    [SerializeField] private RectTransform StartPoint;
    [SerializeField] private RectTransform EndPoint;
    [SerializeField] private Animator Anim;

    public bool MoveCheck;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        TargetUITransfrom.position = StartPoint.position;

        MoveCheck = false;
    }

    float SetTime()
    {
        
        float distance = Vector3.Distance(StartPoint.position, EndPoint.position);
        float Offset = MoveCheck ? Vector3.Distance(TargetUITransfrom.position, EndPoint.position) : Vector3.Distance(TargetUITransfrom.position, StartPoint.position);
        return 1 - (Offset / distance);
    }
   
    public IEnumerator SlideInCoroutine_01()
    {
        float fTime = SetTime();
        
        while (fTime <= 1.0f && MoveCheck)
        {
            fTime += Time.deltaTime;
            TargetUITransfrom.position = Vector3.Lerp(StartPoint.position, EndPoint.position, fTime);
            yield return null;
        }

    }

    public IEnumerator SlideOutCoroutine_01()
    {
        float fTime = SetTime();

        while (fTime <= 1.0f && !MoveCheck)
        {
            fTime += Time.deltaTime;
            TargetUITransfrom.position = Vector3.Lerp(EndPoint.position, StartPoint.position, fTime);
            yield return null;
        }

    }
    public void Slide_02()
    {

    }
    public void Slide_03()
    {

    }
    public void Slide_04()
    {

    }
    public void Slide_05()
    {

    }
    public void Slide_06()
    {

    }
}
