using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInit : MonoBehaviour
{
    Information info;
    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.Find("Information").GetComponent<Information>();
        GameObject Obj = Instantiate(Resources.Load("Prefabs/Vehicles/Vehicles" +info.pIndex.ToString() ) as GameObject);
        Obj.transform.position = transform.position;
        Obj.transform.parent = transform;
    }

}
