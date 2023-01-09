using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationManager : MonoBehaviour
{
    static private InformationManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
