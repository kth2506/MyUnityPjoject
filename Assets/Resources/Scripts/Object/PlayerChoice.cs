using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoice : MonoBehaviour
{

    
    [SerializeField] private GameObject Player;
    [SerializeField] private Information info;
    
    // Start is called before the first frame update

    public void PlayerChange()
    {
        info = FindObjectOfType<Information>();
        if (info.GetIndex() > 5)
            info.PlayerSelect(1);
        if (info.GetIndex() < 1)
            info.PlayerSelect(5);
        Destroy(transform.GetChild(0).gameObject);
        Player = Resources.Load("Prefabs/Vehicles/Vehicles" + info.GetIndex().ToString()) as GameObject;
        GameObject Obj = Instantiate(Player);
        //Obj.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0);
        Obj.transform.position = transform.position;
        Obj.transform.parent = transform;
        Obj.name = "Player";

    }

    public void Left()
    {
        info.Index(-1);
        PlayerChange();
    }
    public void Right()
    {
        info.Index(1);
        PlayerChange();
    }
}
