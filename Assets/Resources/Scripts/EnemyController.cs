using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antenna
{
    public float Angle;
    public Vector3 Direction;
    public bool Check;
    public Color _Color;
}

public class EnemyController : MonoBehaviour
{


    private Vector3[] Forward = new Vector3[4];
    private Vector3 Direction = new Vector3();
    private List<Antenna> AntennaList = new List<Antenna>();
    private List<string> ObstacleList = new List<string>();


    [SerializeField] private GameObject NodeList;
    [SerializeField] private GameObject Player;
    [SerializeField] private Point WayPoint;

    private bool TargetColl;
    public float Angle;
    public float fTime;

    private void Awake()
    {
        NodeList = GameObject.Find("PointList");
        Player = GameObject.Find("Player");
        WayPoint = NodeList.transform.GetChild(0).GetComponent<Point>();
    }

    
    // Start is called before the first frame update
    void Start()
    {
        float fAngle = -45.0f;
        for(int i = 0; i < 5; ++i)
        {
            Antenna ant = new Antenna();
            ant.Angle = fAngle;

            ant.Direction = new Vector3(
                transform.eulerAngles.x + Mathf.Sin(ant.Angle * Mathf.Deg2Rad), 
                0.0f, 
                transform.eulerAngles.z + Mathf.Cos(ant.Angle * Mathf.Deg2Rad));
            
            ant.Check = false;
            ant._Color = Color.green;
            
            AntennaList.Add(ant);
            fAngle += 22.5f;
        }

        Direction = new Vector3(0.0f, 0.0f, 0.0f);


        transform.gameObject.GetComponent<Rigidbody>().useGravity = false;

        transform.GetComponent<BoxCollider>().isTrigger = true;
        transform.gameObject.GetComponent<SphereCollider>().isTrigger = false;

        TargetColl = false;
        fTime = 0;
        Angle = 0.0f;
        
    }

    private void FixedUpdate()
    {
        float fAngle = Angle - 45.0f;
        for (int i = 0; i < AntennaList.Count; ++i)
        {
            AntennaList[i].Angle = fAngle;
            fAngle += 22.5f;

            AntennaList[i].Direction = new Vector3(
                          transform.eulerAngles.x + Mathf.Sin(AntennaList[i].Angle * Mathf.Deg2Rad),
                          0.0f,
                          transform.eulerAngles.z + Mathf.Cos(AntennaList[i].Angle * Mathf.Deg2Rad));

        }
    }

    // Update is called once per frame
    void Update()
    {

        TargetColl = Vector3.Distance(transform.position, Player.transform.position) < 10.0f ? true : false;
       // Debug.Log(Vector3.Distance(transform.position, Player.transform.position));

        if (TargetColl)
        {
            RaycastHit hit;

            for (int i = 0; i < AntennaList.Count; ++i)
            {
                if (Physics.Raycast(transform.position, AntennaList[i].Direction, out hit, 5))
                {
                    if (hit.transform.tag == "Player")
                    {
                        AntennaList[i].Check = true;
                        AntennaList[i]._Color = Color.red;

                        switch (i)
                        {
                            case 0:
                                Angle += 1.0f;
                                break;
                            case 1:
                                Angle += 0.5f;
                                break;
                            case 3:
                                Angle -= 0.5f;
                                break;
                            case 4:
                                Angle -= 1.0f;
                                break;
                        }

                    }
                    else
                    {
                        AntennaList[i].Check = false;
                        AntennaList[i]._Color = Color.green;
                    }
                }

                Debug.DrawLine(
                        transform.position,
                        transform.position + AntennaList[i].Direction * 5,
                        AntennaList[i]._Color);
                StartCoroutine(LerpRotation());

            }

        }
        else
            Move();



    }

    private void Move()
    {
        Direction = (WayPoint.transform.position - transform.position).normalized;
        transform.position += (Direction * 5.0f * Time.deltaTime);
        //transform.LookAt(transform.position + Direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals(WayPoint.transform.name))
            WayPoint = WayPoint.Node;
    }

    IEnumerator LerpRotation()
    {
        float fTime = 0f;
        while(fTime <= 1.0f)
        {
            fTime += Time.deltaTime;
            float fAngle = Mathf.Lerp(transform.eulerAngles.y, Angle, fTime) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, fAngle, transform.eulerAngles.z);
          
            yield return null;
        }    
    }    
}
