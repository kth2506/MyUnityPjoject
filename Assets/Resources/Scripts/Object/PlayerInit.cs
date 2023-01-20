using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject Obj = Instantiate(Resources.Load("Prefabs/Vehicles/Vehicles" + FindObjectOfType<Information>().GetIndex().ToString() ) as GameObject);
        Obj.name = "Player";
        Obj.transform.position = transform.position;
        Obj.transform.parent = transform;
    }

}
