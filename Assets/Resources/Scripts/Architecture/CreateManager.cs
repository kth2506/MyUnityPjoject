using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateManager : MonoBehaviour
{
    static private CreateManager Instance = null;
    [Range(3.0f, 10.0f)]
    private float time;

    public List<Object> ObjectList = new List<Object>();
    [SerializeField] private Person PersonObj;
    //[SerializeField] private Object PlayerObj;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        time = 3.0f;
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        time -= Time.deltaTime;

        if (time < 0.0f)
        {
            time = 3.0f;
            CreateObject(PersonObj);
        }
    }

    public void CreateObject(Person _Object)
    {
        Object tObj = Instantiate(_Object);
        tObj.Initialize();
        tObj.transform.SetParent(transform);
        tObj.transform.position = transform.position;
        tObj.name = tObj.GetKey();
        switch(tObj.GetKey())
        {
        case "Person":
                tObj._Object = Instantiate(_Object.GetComponent<Person>()._Object);
                break;
        case "Enemy":
                tObj._Object = Instantiate(_Object.GetComponent<Enemy>()._Object);
                break;
        }
        tObj._Object.transform.position = transform.position;
        tObj._Object.transform.SetParent(tObj.transform);
        
        ObjectList.Add(tObj);
    }

    
}
