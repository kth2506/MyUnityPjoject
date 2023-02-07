using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private List<int> CourseList;
    [SerializeField] private GameObject Arrow;
    private int Index;
    public void Start()
    {
        Index = 0;
    }


    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Stop & mesh 
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    SetIndex();
                }
            }
        }
    }


    public int GetIndex()
    {
        return CourseList[Index];
    }


    public void SetIndex()
    {

        Index++;

        if (Index >= CourseList.Count)
            Index = 0;
       
        GameObject Obj = (Resources.Load("Prefabs/Object/SwitchArrow_" + Index.ToString()) as GameObject);
        Obj.transform.position = Arrow.transform.position;
        Destroy(Arrow.gameObject);
        Arrow = Instantiate(Obj);
        Arrow.transform.parent = transform;
        Debug.Log("Changed!");
    }



}
