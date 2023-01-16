using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 Offset;
    private Vector3 StartPos = new Vector3();
    private Vector3 EndPos = new Vector3();
    private Vector3 CameraAngles;
    private Quaternion CameraQuaternion;
    private GameObject head;
    private GameObject JoyStick;

    private Camera main;

   

    private void Awake()
    {
        main = GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindWithTag("Player").transform;

        Offset = new Vector3(-60.0f, 70.0f, -70.0f);
        //transform.Rotate(0.0f, 0.0f, 0.0f);
        StartPos = new Vector3(0.0f, 0.0f, 0.0f);
        EndPos = new Vector3(0.0f, 0.0f, 0.0f);
        //transform.parent = Target.transform;
        CameraQuaternion = new Quaternion();
        CameraAngles = new Vector3(90.0f, 0.0f, 0.0f);
        CameraQuaternion = Quaternion.Euler(CameraAngles);
       
    }

    // Update is called once per frame
    void Update()
    {
        //Offset = new Vector3(Target.transform.position.x, 20.0f, Target.transform.position.z - 1.5f);
        //transform.position = Offset;
        // **부드러운 이동
        if (Target)
        {
            //if (main.orthographicSize < Obj.transform.localScale.x + 2.0f)
            //    main.orthographicSize += 1.3f;
            transform.position = Vector3.Lerp(
                transform.position ,
                Target.position + Offset,
                Time.deltaTime * 2.0f);
        }
        else
        {
            Target = GameObject.FindWithTag("Player").transform;
        }
        //**부드러운 회전
        //if (Input.GetMouseButtonDown(0))
        //{
        //    CameraAngles = transform.rotation.eulerAngles;
        //}
        //if (Input.GetMouseButton(0))
        //{
        //    CameraAngles.y += Input.GetAxis("Mouse X") * 5.0f;
        //    CameraQuaternion = Quaternion.Euler(CameraAngles);
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    CameraAngles.y = 0.0f;
        //    CameraQuaternion = Quaternion.Euler(CameraAngles);
        //}

        //transform.rotation = Quaternion.Slerp(
        //    transform.rotation,
        //    CameraQuaternion,
        //    Time.deltaTime * 10.0f);

    }
}
