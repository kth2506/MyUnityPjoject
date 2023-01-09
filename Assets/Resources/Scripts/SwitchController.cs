using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SwitchController : MonoBehaviour
{
    public VehiclesController vehicles;
    Root root = 0;
    enum Root
    {
        WayPoint,
        WayPoint2,
        WayPoint3
    }
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform == transform)
                {
                    Debug.Log("sss");
                    root++;
                    if (root == Root.WayPoint3)
                        root = Root.WayPoint;

                }
                
            }
        }
      

    }
}
