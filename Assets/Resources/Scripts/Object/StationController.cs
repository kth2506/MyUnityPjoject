using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationController : MonoBehaviour
{
    [SerializeField] private GameObject Person;
    [SerializeField] private List<GameObject> PersonList;
    
    // Start is called before the first frame update
    void Start()
    {
       
        Person = Resources.Load("Prefabs/Person") as GameObject;
        StartCoroutine(Create());
    }


    IEnumerator Create()
    {
        while(true)
        {
            yield return new WaitForSeconds(3.0f);
            if(PersonList.Count < 10)
            {
                GameObject Obj = Instantiate(Person);

                if (transform.forward.x == 0.0f)
                {
                    Obj.transform.rotation = Quaternion.Euler(270.0f, 90.0f, 0.0f);
                    Obj.transform.position = transform.position + new Vector3(
                     Random.Range(-3.0f, 3.0f) * transform.forward.z,
                     0.5f,
                     Random.Range(-15.0f, -1.0f) * transform.forward.z);
                }
                else
                    Obj.transform.position = transform.position + new Vector3(
                      Random.Range(-15.0f, 3.0f) * transform.forward.x,
                          0.5f,
                          Random.Range(-3.0f, 5.0f) * transform.forward.x);
                Obj.transform.parent = transform;
                PersonList.Add(Obj);
            }
        }
    }
   
}
