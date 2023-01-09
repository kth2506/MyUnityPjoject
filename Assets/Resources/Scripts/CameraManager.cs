using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 Offset;
    private Vector2 MousePoint;
    private void Awake()
    {
        Target = GameObject.Find("Player").transform;

        Offset = new Vector3(-60.0f, 50.0f, -70.0f);
    }
    // Start is called before the first frame update
   
    IEnumerator Start()
    {
        float fTime = 0.0f;
        while(fTime < 2.0f)
        {
            fTime += 0.01f;
            transform.position = Vector3.Lerp(
                transform.position,
                Target.position + Offset,
                Time.deltaTime * 2.0f);
            yield return null;
        }
        StartCoroutine(Wheel());
    }

    IEnumerator Wheel()
    {
        while(true)
        {
            float wheelInput = Input.GetAxis("Mouse ScrollWheel");
            if (wheelInput > 0)
            {

                if (Camera.main.orthographicSize > 30.0f)
                {
                    Camera.main.orthographicSize -= 1.0f;
                }
            }
            else if (wheelInput < 0)
            {
                if (Camera.main.orthographicSize < 60.0f)
                {
                    Camera.main.orthographicSize += 1.0f;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                MousePoint = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 position
                     = -Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - MousePoint);

                position.z = position.y;
                position.y = .0f;

                Vector3 move = position * (Time.deltaTime * 100.0f);

                float y = transform.position.y;

                transform.Translate(move);
                transform.transform.position
                    = new Vector3(transform.position.x, y, transform.position.z);
            }
            
            yield return null;
        }
        
    }
}
