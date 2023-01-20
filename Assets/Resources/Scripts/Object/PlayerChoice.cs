using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoice : MonoBehaviour
{

    
    [SerializeField] private GameObject Player;
    [SerializeField] private Information info;
    public int Index = 1;
    // Start is called before the first frame update

    public void PlayerChange()
    {
        if (Index > 5)
            Index = 1;
        if (Index < 1)
            Index = 5;
        info.PlayerSelect(Index);
        Destroy(transform.GetChild(0).gameObject);
        Player = Resources.Load("Prefabs/Vehicles/Vehicles" + Index.ToString()) as GameObject;
        GameObject Obj = Instantiate(Player);
        //Obj.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0);
        Obj.transform.position = transform.position;
        Obj.transform.parent = transform;
        Obj.name = "Player";

    }

    public void Left()
    {
        Index--;
        PlayerChange();
    }
    public void Right()
    {
        Index++;
        PlayerChange();
    }
}
