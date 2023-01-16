using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoice : MonoBehaviour
{

    
    [SerializeField] private GameObject Player;
    [SerializeField] private Information info;
    public int Index;
    // Start is called before the first frame update
    private void Awake()
    {
        Index = 1;
        Player = Resources.Load("Prefabs/Vehicles/Vehicles1") as GameObject;
        GameObject Obj = Instantiate(Player);
        Obj.transform.position = transform.position;
        Obj.transform.parent = transform;
        info = GameObject.Find("Information").GetComponent<Information>();
    }

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
        transform.position = new Vector3(15.0f, 0.0f, 50.0f);
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
