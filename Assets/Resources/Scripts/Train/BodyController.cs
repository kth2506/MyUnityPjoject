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
        if (SceneManager.GetActiveScene().name.Contains("Stage"))
        {
            Index = 0;
            Root = "WayPoint";
            GameObject Obj = GameObject.Find(Root);
            for (int i = 0; i < Obj.transform.childCount; ++i)
                target.Add(Obj.transform.GetChild(i));
            dolly = transform.GetComponent<CinemachineDollyCart>();
            dolly.m_Path = target[Index].gameObject.GetComponent<CinemachinePath>();
            dolly.m_Speed = 20.0f;

            float Size = transform.GetChild(0).GetComponent<Collider>().bounds.size.x
                * transform.GetChild(0).GetComponent<Collider>().bounds.size.z;
            Debug.Log(Size + "   " + transform.GetChild(0).name);
            Debug.Log(transform.GetChild(0).GetComponent<Collider>().bounds.size + "   " + transform.GetChild(0).name);
            switch (transform.name)
            {
                case "1":
                    offset = 0.0f;
                    break;
                case "2":
                    offset = 4.2f;
                    break;
                case "3":
                    offset = 7.3f;
                    break;
                case "4":
                    offset = 11.5f;
                    break;
                case "5":
                    offset = 15.0f;
                    break;
                case "6":
                    offset = 18.0f;
                    break;
            }
            dolly.m_Position = 20.0f - offset;
        }
        else
            GetComponent<BodyController>().enabled = false;

    }

    void LateUpdate()
    {
        //dolly.m_Position = Train.GetComponent<CinemachineDollyCart>().m_Position - offset;
        //dolly.m_Path = Train.GetComponent<CinemachineDollyCart>().m_Path;
        if (dolly.m_Position + 3.0f >= dolly.m_Path.PathLength && target.Count > Index + 1 && !target[Index].gameObject.GetComponent<CinemachinePath>().Looped)
        {
            float temp = dolly.m_Path.PathLength - 2.9f;
            Index++;
            dolly.m_Path = target[Index].gameObject.GetComponent<CinemachinePath>();
            dolly.m_Position -= temp;

        }

    }
}
