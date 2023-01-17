using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public struct CastInfo
    {
        // ** 맞았는지 확인
        public bool Hit;
        // ** 맞았다면 맞은 위치, 안맞았다면 Radius
        public Vector3 Point;
        // ** 도달거리
        public float Distance;
        // ** 각도
        public float Angle;

    }
    [Header("Circle")]
    [Range(0, 30)]
    public float Radius = 0;
    [Range(0, 360)]
    public float ViewAngle = 0.0f;
    [Range(0.1f, 5.0f)]
    public float Angle = 0.5f;
    static public float OffsetAngle = 0;

    [HideInInspector] public List<Transform> TargetList = new List<Transform>();
    [SerializeField] private LayerMask TargetMask;
    [SerializeField] private LayerMask ObstacleMask;
    
    public List<CastInfo> LineList = new List<CastInfo>();

    public MeshFilter _MeshFilter;
    private Mesh _Mesh;

    private void Awake()
    {
        _MeshFilter = GameObject.Find("View").transform.GetComponent<MeshFilter>();
        _Mesh = new Mesh();
        _MeshFilter.mesh = _Mesh;
    }

    private void Start()
    {
        Radius = 25.0f;
        ViewAngle = 90.0f;
        Angle = 5.0f;
        StartCoroutine(CheckTarget());
    }

    private void Update()
    {
        GetVertex();

        //if (Input.GetKey(KeyCode.LeftArrow))
        //    OffsetAngle -= 5;
        //if (Input.GetKey(KeyCode.RightArrow))
        //    OffsetAngle += 5;

        //transform.rotation = Quaternion.Lerp(
        //   transform.rotation,
        //   Quaternion.Euler(transform.up * (OffsetAngle * 0.5f)),
        //   Time.deltaTime * 2.0f);
    }
    

    public Vector3 GetEulerAngle(float _Angle)
    {
        return new Vector3(
            Mathf.Sin(_Angle * Mathf.Deg2Rad),
            0.0f,
            Mathf.Cos(_Angle * Mathf.Deg2Rad)) * Radius + transform.position;
    }
    IEnumerator CheckTarget()
    {
        while (true)
        {
            TargetList.Clear();

            Collider[] ColList = Physics.OverlapSphere(transform.position, Radius, TargetMask);
            foreach(Collider element in ColList)
            {
                Vector3 Direction = (element.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, Direction) < ViewAngle * 0.5f)
                {
                    float fDistance = Vector3.Distance(transform.position, element.transform.position);

                    if (!Physics.Raycast(transform.position, Direction, fDistance, ObstacleMask))
                    {
                        TargetList.Add(element.transform);
                    }
                }
            }

            yield return new WaitForSeconds(0.1f);

        }
    }

    public Vector3 GetAngle(float _Angle)
    {
        return new Vector3(Mathf.Sin(_Angle* Mathf.Deg2Rad),0.0f,Mathf.Cos(_Angle * Mathf.Deg2Rad));
    }

    public CastInfo GetCastInfo(float _Angle)
    {
        Vector3 Direction = GetAngle(_Angle);
        

        CastInfo Info;
        RaycastHit hit;
        Info.Angle = _Angle;

        if (Physics.Raycast(transform.position, Direction, out hit, Radius, ObstacleMask))
        {
            // ** 맞았는지 안맞았는지 확인
            Info.Hit = true;
            // ** 거리
            Info.Distance = hit.distance;
            // ** 맞은 위치
            Info.Point = hit.point ;
        }
        else
        {
            // ** 안맞음
            Info.Hit = false;
            // ** 끝 위치
            Info.Point = transform.position + Direction * Radius;
            // ** 안맞았으니 반지름
            Info.Distance = Radius;

        }

        return Info;


    }

    void GetVertex()
    {
        LineList.Clear();
        

        int Count = Mathf.RoundToInt(ViewAngle / Angle) + 1;
        float fAngle = -ViewAngle * 0.5f + transform.eulerAngles.y;

        _MeshFilter.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
        //_MeshFilter.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

        //Debug.Log(transform.eulerAngles.x + " , " + transform.eulerAngles.y + " , " + transform.eulerAngles.z);
        //Debug.Log(Vector3.Angle(GetEulerAngle((ViewAngle) * 0.5f), transform.forward));
        //List<Vector3> Points = new List<Vector3>();
        //LineList.Add();
        for (int i = 0; i < Count; ++i)
        {
            CastInfo Info = GetCastInfo(fAngle + (Angle * i));
            LineList.Add(Info);
        }


        int VertexCount = LineList.Count + 1;
        Vector3[] Vertices = new Vector3[VertexCount];
        int[] Triangles = new int[(VertexCount - 2) * 3];
        Vertices[0] = Vector3.zero;
        for (int i = 1; i < Vertices.Length ; ++i)
            Vertices[i] = LineList[i - 1].Point - transform.position;

        for (int i = 0; i < Vertices.Length - 2; ++i)
        {
           Triangles[i * 3] = 0;
           Triangles[i * 3 + 1] = i + 1;
           Triangles[i * 3 + 2] = i + 2;
        }


        
        _Mesh.Clear();
 
        _Mesh.vertices = Vertices;
        _Mesh.triangles = Triangles;

        _Mesh.RecalculateNormals();
    }
}
