using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointList : MonoBehaviour
{
    [SerializeField] private GameObject SpawnPoint;
    List<GameObject> PointList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        transform.name = "SpawnPointList";
        for(int i = 0; i < 15; ++i)
        {
            GameObject Obj = Instantiate(SpawnPoint);
            Obj.transform.parent = transform;
            Obj.transform.name = "Point";
            Obj.transform.position = new Vector3(Random.Range(-25.0f, 25.0f),
                10.0f, Random.Range(-25.0f, 25.0f));
            PointList.Add(Obj);
        }
    }


}
