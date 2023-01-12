using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
  
   
    private Vector2 MousePoint;

    private void Update()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0)
        {

            if (Camera.main.orthographicSize > 25.0f)
            {
                Camera.main.orthographicSize -= 1.0f;
            }
        }
        else if (wheelInput < 0)
        {
            if (Camera.main.orthographicSize < 70.0f)
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
    }
  
}
