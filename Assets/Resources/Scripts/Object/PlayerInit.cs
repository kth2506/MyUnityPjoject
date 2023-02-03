using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInit : MonoBehaviour
{
    [SerializeField] private Information info;
    // Start is called before the first frame update
    void Start()
    {
        info = FindObjectOfType<Information>();
        GameObject temp = Resources.Load("Prefabs/Vehicles/Vehicles" + info.GetIndex().ToString()) as GameObject;
        GameObject Obj = new GameObject();
        Obj = Instantiate(temp);
            //+ info.GetIndex().ToString()) as GameObject; ;
        Obj.name = "Player";

        Obj.transform.position = transform.position;
        Obj.transform.parent = transform;
    }

}
