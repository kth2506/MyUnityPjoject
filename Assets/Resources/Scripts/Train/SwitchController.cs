using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    private GameObject Player;

    public void SwitchCourse()
    {
        Player = FindObjectOfType<CinemachineController>().gameObject;

    }
}
