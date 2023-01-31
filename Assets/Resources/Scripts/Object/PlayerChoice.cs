using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoice : MonoBehaviour
{

    
    [SerializeField] private GameObject Player;
    [SerializeField] private Information info;

    // Start is called before the first frame update

    private void Start()
    {
        info = FindObjectOfType<Information>();
    }
    public void PlayerChange()
    {
        
        if (info.GetIndex() > 5)
            info.PlayerSelect(1);
        if (info.GetIndex() < 1)
            info.PlayerSelect(5);
        Destroy(transform.GetChild(0).gameObject);
        Player = Resources.Load("Prefabs/Vehicles/Vehicles" + info.GetIndex().ToString()) as GameObject;
        GameObject Obj = Instantiate(Player);
        //Obj.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0);

        
        Obj.transform.parent = transform;
        Obj.transform.position = new Vector3(15.0f, 0.0f, 20.0f);
        Obj.name = "Player";

        StartCoroutine(Set(Obj));

    }

    IEnumerator Set(GameObject _Object)
    {
        float time = 0.0f;
        while(time < 1.0f)
        {
            time += Time.deltaTime;
            _Object.transform.position = Vector3.Lerp
           (_Object.transform.position, new Vector3(15.0f, 0.0f, -20.0f), Time.deltaTime * 2);
            yield return null;
        }
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
