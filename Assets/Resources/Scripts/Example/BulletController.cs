using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //[SerializeField] private float Speed;
    [SerializeField] private Vector3 FirePoint;
    [SerializeField] private GameObject BoomObject;


    private void Awake()
    {
        FirePoint = GameObject.Find("FirePoint").transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Speed = 10.0f;
        transform.position = FirePoint;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            GameObject Obj = Instantiate(BoomObject);
            Obj.transform.position = transform.position;
            Destroy(this.gameObject);
        }
        if (collision.transform.tag == "Enemy")
        {
            GameObject Obj = Instantiate(BoomObject);
            Obj.transform.position = transform.position;
            Destroy(transform.gameObject);
            Destroy(collision.gameObject);
        }
    }


    //const int MAX = (180 / 5) + 1;
    //private float Angle;
    //[SerializeField] private GameObject Ball;
    //List<GameObject> BallList = new List<GameObject>();
    //private Vector3[] Points = new Vector3[MAX];

    //// Start is called before the first frame update
    //private void Start()
    //{
    //    Angle = 0.0f;

    //    for (int i = 0; i < MAX; ++i)
    //    {
    //        Angle += 5.0f;
    //        Points[i].x = transform.position.x + Mathf.Cos(Angle * 180.0f / Mathf.PI) * 5.0f;
    //        Points[i].y = transform.position.y;
    //        Points[i].z = transform.position.z + Mathf.Sin(Angle * 180.0f / Mathf.PI) * 5.0f;

    //        GameObject Obj = Instantiate(Ball);
    //        Obj.transform.parent = GameObject.Find("BallWheel").transform;
    //        Obj.transform.name = "Ball";
    //        Obj.transform.position = Points[i];
    //        BallList.Add(Obj);

    //    }
    //}

    //// Update is called once per frame
    //private void Update()
    //{
    //    Angle = 0.0f;
    //    for (int i = 0; i < MAX; ++i)
    //        Debug.DrawLine(transform.position, Points[i], Color.red);

    //}



}
