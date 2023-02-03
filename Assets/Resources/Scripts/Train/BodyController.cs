using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class BodyController : MonoBehaviour
{
    [SerializeField] private List<Transform> target;
    private CinemachineDollyCart dolly;

    
    private float offset;
    private int Index;
    private string Root;

    private void Awake()
    {
        // 해당 Scene이 Stage일 경우에만 이 스크립트를 활성화
        if (SceneManager.GetActiveScene().name.Contains("Stage"))
        {
            Index = 0;
            Root = "WayPoint";
            GameObject Obj = GameObject.Find(Root);
            for (int i = 0; i < Obj.transform.childCount; ++i)
                target.Add(Obj.transform.GetChild(i));
            dolly = transform.GetComponent<CinemachineDollyCart>();
            dolly.m_Path = target[Index].gameObject.GetComponent<CinemachinePath>();
            offset = 0;
            dolly.m_Speed = 20.0f;
        }
        else
            GetComponent<BodyController>().enabled = false;
    }

    private void Start()
    {
        dolly.m_Position = offset;
    }


    public void SetOffset(float Value)
    {
        offset += Value;
    }
    void LateUpdate()
    {
        if (dolly.m_Position + 3.0f >= dolly.m_Path.PathLength)
        {
            if(target.Count > Index + 1 && !target[Index].gameObject.GetComponent<CinemachinePath>().Looped)
            {
                float temp = dolly.m_Path.PathLength - 2.9f;
                Index++;
                dolly.m_Path = target[Index].gameObject.GetComponent<CinemachinePath>();
                dolly.m_Position -= temp;
            }
            //if(isExpected == true)
            //{
            //    float temp = dolly.m_Path.PathLength - 2.9f;

            //    dolly.m_Path = target[Index].gameObject.GetComponent<CinemachinePath>();
            //    dolly.m_Position -= temp;

            //    isExpected = false;
            //}
            

        }

    }

    public void ChangeCourse()
    {
    }
}
