using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
public class VehiclesController : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] private List<Transform> target;
    [SerializeField] private List<GameObject> StationList;
    [SerializeField] private GameObject BoomObject;

    private MeshRenderer mesh;
    public int Index;
    public string Root;
    //private int tScore;
    // Start is called before the first frame update
    private void Awake()
    {
        Index = 0;
        //tScore = 0;
        Root = "WayPoint";
        
        GameObject Obj = GameObject.Find(Root);
        mesh = transform.GetComponent<MeshRenderer>();
        for (int i = 0; i < Obj.transform.childCount; ++i)
            target.Add(Obj.transform.GetChild(i));
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target[Index].position);
        agent.updateRotation = false;
        StationList.Add(GameObject.Find("Station_0"));
        StationList.Add(GameObject.Find("Station_1"));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log()
        if (agent.remainingDistance < 2.0f)
        {
            SetRoot();
        }
        
        Vector3 lookrotation = agent.steeringTarget - transform.position;
        transform.rotation = Quaternion.Lerp(
            transform.rotation, Quaternion.LookRotation(lookrotation), 10.0f * Time.deltaTime);

        //float Angle = Vector3.Angle(transform.forward, agent.nextPosition);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {

                mesh.material.shader = Shader.Find("Transparent/VertexLit");

                if (mesh.material.HasProperty("_Color"))
                {
                    Color color = mesh.material.GetColor("_Color");
                    mesh.material.SetColor("_Color", new Color(color.r, color.g, color.b, 0.9f));
                }
                if (Input.GetMouseButtonDown(0))
                {
                
                    agent.isStopped = !agent.isStopped;
                }
            }
            else
            {
                string temp = "Materials/" + mesh.material.name.Replace(" (Instance)", "");
                mesh.material = Resources.Load(temp) as Material;
            }
        }


        for(int i = 0; i < StationList.Count; ++i)
        {
            float Distance = Vector3.Distance(agent.transform.position, StationList[i].transform.position);
            if (Distance < 20.0f)
            {
                StartCoroutine(Plus(i));
            }
        }
    }

    public void SetRoot()
    {
        Index++;
        if (Index == target.Count)
            Index = 0;
        agent.SetDestination(target[Index].position);
        if (target[Index].tag == "Station")
        {
            agent.autoBraking = true;
            Score score = GameObject.Find("Score").GetComponent<Score>();
            //score.Increase(tScore);
            //tScore = 0;
        }
        else
            agent.autoBraking = false;
    }
    
    IEnumerator Plus(int _Index)
    {
        yield return new WaitForSeconds(1.0f);

        Transform[] childList = GameObject.Find("Station_" + _Index.ToString()).
            gameObject.GetComponentsInChildren<Transform>();
        if (childList.Length - 1 > 1)
        {
            Debug.Log(childList.Length - 1);
            
            if (childList != null)
            {
                for (int i = 1; i < childList.Length; ++i)
                {
                    if (childList[i] != transform)
                    {
                        StartCoroutine(Move(childList[i].transform));
                        Destroy(childList[i].gameObject, 0.2f);
                    }
                }
            }
        }

    }

    IEnumerator Move(Transform _transform)
    {

        while(true)
        {
         _transform.position = Vector3.MoveTowards(_transform.position, transform.position, 5);
            if (Vector3.Distance(_transform.position, transform.position) < 0.5f)
            {
               
                break;
            }
            yield return null;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            
            GameObject Obj = Instantiate(BoomObject);
            Obj.transform.position = transform.position;
            Destroy(transform.gameObject);
        }
    }
}
