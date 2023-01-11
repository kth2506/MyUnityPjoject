using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
public class TrainsController : MonoBehaviour
{
    public CinemachineDollyCart dolly;
    // Start is called before the first frame update
    void Awake()
    {
        dolly = transform.GetComponent<CinemachineDollyCart>();
        dolly.m_Path = GameObject.Find("EnemyRoot").GetComponent<CinemachinePath>();
        dolly.m_Speed = 15.0f;
    }

}
