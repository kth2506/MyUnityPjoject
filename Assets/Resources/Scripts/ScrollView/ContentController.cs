using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentController : MonoBehaviour
{
   [SerializeField] private GameObject HorizontalListPrefab;
   [SerializeField] private GameObject AddHorizontalButton;
    private RectTransform ContentTransform;

    private void Awake()
    {
        ContentTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        AddHorizontalList(0.0f, 120.0f);
        for(int i= 0; i < 5; ++i)
            AddHorizontalList(0.0f, 115.0f);
    }

    private void AddHorizontalList(float _x, float _y)
    {

        ContentTransform.sizeDelta = new Vector2(
               ContentTransform.sizeDelta.x + _x,
               ContentTransform.sizeDelta.y + _y);
        GameObject Obj = Instantiate(HorizontalListPrefab);
        Obj.transform.SetParent(transform);

        AddHorizontalButton.transform.SetAsLastSibling();
    }

    public void AddHorizontal()
    {
        AddHorizontalList(0.0f, 115.0f);
        ContentTransform.position += new Vector3(0.0f, 115.0f, 0.0f);
    }
}
