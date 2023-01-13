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
    [SerializeField] private GameObject Train;
    private CinemachineDollyCart dolly;
    private int Index;
    private string Root;
    private void Awake()
    {
        Index = 0;
        Root = "WayPoint";

        GameObject Obj = GameObject.Find(Root);
        for (int i = 0; i < Obj.transform.childCount; ++i)
            target.Add(Obj.transform.GetChild(i));
        dolly = transform.GetComponent<CinemachineDollyCart>();
        dolly.m_Path = target[Index].gameObject.GetComponent<CinemachinePath>();
        dolly.m_Speed = 20.0f;
    }

    void Update()
    {
        //dolly.m_Position = Train.GetComponent<CinemachineDollyCart>().m_Position - float.Parse(transform.name) * 4;
        //dolly.m_Path = Train.GetComponent<CinemachineDollyCart>().m_Path;
        ////if (dolly.m_Position >= dolly.m_Path.PathLength)
        //{
        //    Index++;
        //    dolly.m_Path = target[Index].gameObject.GetComponent<CinemachinePath>();

        //    dolly.m_Position = 0.0f;
        //}

        transform.localEulerAngles = new Vector3(270.0f, transform.localEulerAngles.y, transform.localEulerAngles.z);

        Debug.Log(transform.localEulerAngles);

    }
}
