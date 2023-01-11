using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject hitPoint;
    [SerializeField] private Vector3 offSet;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private List<GameObject> Enemys;
    [SerializeField] private List<GameObject> hitPoints;

    private void Awake()
    {
        //mainCamera = GameObject.Find("Main Camera");
        mainCamera = Camera.main.transform.gameObject;
        offSet = new Vector3(0.0f, 0.0f, 0.5f);
        //    hitPoint = Instantiate(Resources.Load("Prefabs/Image") as GameObject);
        //    hitPoint.transform.parent = GameObject.Find("Canvas").transform;
      
    }

    private void Start()
    {
        GameObject Obj = GameObject.Find("Image");
        Obj.transform.name = "Image";

        Image Img = Obj.GetComponent<Image>();
        Img.color = Color.red;
        for (int i = 0; i < 5; ++i)
        {
        //Obj.transform.SetParent(GameObject.Find("Canvas").transform);
            GameObject obj2 = Instantiate(Obj);
            obj2.transform.SetParent(GameObject.Find("Canvas").transform);
            hitPoints.Add(obj2);
        }
    }
    void Update()
    {
        mainCamera.transform.position = new Vector3(
            transform.position.x,
            transform.position.y + 15.0f,
            transform.position.z - 3.0f);

        for(int i = 0; i < hitPoints.Count; ++i)
        {
            hitPoints[i].transform.position = Camera.main.WorldToScreenPoint(Enemys[i].transform.position + offSet * 2);
        }
    }
}
