using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 Offset = new Vector3(50.0f, -50.0f, 0.0f);
    private GameObject tempObj;
    [SerializeField] private GameObject Canvas;
    private void Start()
    {
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        transform.SetParent(Canvas.transform);
        transform.SetAsLastSibling();

        transform.localScale = new(0.8f, 0.8f, 1.0f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        tempObj = transform.parent.gameObject;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = new Vector3(0.93f, 0.93f, 1.0f);
        transform.SetAsLastSibling();
        //GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        //if (Vector2.Distance(transform.position, obj.transform.position + Offset) <= 90.0f)
        //{
        //    transform.position = obj.transform.position + Offset;
        //    transform.SetParent(obj.transform);

        //    Debug.Log(transform.position);
        //    return;
        //}
        foreach (GameObject Obj in GameObject.FindGameObjectsWithTag("Slot"))
        {
            if (Vector2.Distance(transform.position, Obj.transform.position + Offset) <= 90.0f)
            {
                transform.position = Obj.transform.position + Offset;
                transform.SetParent(Obj.transform);

                Debug.Log(transform.position);
                return;
            }
        }
        transform.SetParent(tempObj.transform);
        transform.position = tempObj.transform.position + Offset;
    }

    
}
