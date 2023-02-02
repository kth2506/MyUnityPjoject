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
    [SerializeField] private GameObject articulation;
    private CinemachineDollyCart dolly;

    
    public float offset;
    private int Index;
    private string Root;
    private bool isExpected;
    float time;

    private void Awake()
    {
        // 해당 Scene이 Stage일 경우에만 이 스크립트를 활성화
        if (SceneManager.GetActiveScene().name.Contains("Stage"))
        {
            isExpected = false;
            Index = 0;
            Root = "WayPoint";
            GameObject Obj = GameObject.Find(Root);
            for (int i = 0; i < Obj.transform.childCount; ++i)
                target.Add(Obj.transform.GetChild(i));
            dolly = transform.GetComponent<CinemachineDollyCart>();
            dolly.m_Path = target[Index].gameObject.GetComponent<CinemachinePath>();
           
            dolly.m_Speed = 20.0f;
            
            switch(transform.name)
            {
                case "1":
                   offset =  0.0f;
                    break;
                case "2":
                    offset = 3.4f;
                    break;
                case "3":
                    offset = 6.3f;
                    break;
                case "4":
                    offset = 9.2f;
                    break;
                case "5":
                    offset = 12.6f;
                    break;
            }
            //offset = transform.GetComponentInChildren<Collider>().bounds.size.z;


            dolly.m_Position = 20.0f - offset;
        }
        else
            GetComponent<BodyController>().enabled = false;

    }

    private void Start()
    {
        
    }

    void LateUpdate()
    {
        time += Time.deltaTime;
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
