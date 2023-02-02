using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private List<int> CourseList;

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
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform);
                Debug.Log(transform);
                if (hit.transform == transform)
                    SetIndex();
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

        Debug.Log("Changed!");
    }



}
