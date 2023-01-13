using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class CinemachineController : MonoBehaviour
{
    [SerializeField] private List<Transform> target;
    [SerializeField] private List<GameObject> StationList;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Coin;
    [SerializeField] private MeshRenderer[] mesh;
    [SerializeField] private AudioSource TempAudio;
    [SerializeField] private List<GameObject> CubeList;
   //private List<GameObject> BodyList;
    private GameObject BoomObject;
    [SerializeField] private GameObject[] BodyList;
    private CinemachineDollyCart dolly;
    private int Index;
    private string Root;
    
    private List<int> ScoreList = new List<int>();
    private bool isStop;
    // Start is called before the first frame update
    private void Awake()
    {
        Index = 0;
        Root = "WayPoint";
        isStop = false;

        GameObject Obj = GameObject.Find(Root);
        mesh = transform.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < Obj.transform.childCount; ++i)
            target.Add(Obj.transform.GetChild(i));
        BoomObject = Resources.Load("Prefabs/PlayerBoom") as GameObject;
        Enemy = GameObject.Find("Enemy");
        TempAudio = GameObject.Find("EffectSound").GetComponent<AudioSource>();
        dolly = transform.GetComponent<CinemachineDollyCart>();
        dolly.m_Path = target[Index].gameObject.GetComponent<CinemachinePath>();
        dolly.m_Speed = 20.0f;



        // 해당 Scene 이 Stage 일 경우에만 Station 관련 함수를 시작함
        if ((SceneManager.GetActiveScene()).name.Contains("Stage"))
        {
            StationList.Add(GameObject.Find("Station_0"));
            StationList.Add(GameObject.Find("Station_1"));
            for (int i = 0; i < StationList.Count; ++i)
                StartCoroutine(Plus(StationList[i], i));
        }
    }




    // Update is called once per frame
    void Update()
    {
       
        if (dolly.m_Position >= dolly.m_Path.PathLength)
        {
            Index++;
            dolly.m_Path = target[Index].gameObject.GetComponent<CinemachinePath>();
            dolly.m_Position = 0.0f;
        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Stop & mesh 
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {

                for(int i = 0; i < 5; ++i)
                {
                    mesh[i].material.shader = Shader.Find("Transparent/VertexLit");

                    if (mesh[i].material.HasProperty("_Color"))
                    {
                        Color color = mesh[i].material.GetColor("_Color");
                        mesh[i].material.SetColor("_Color", new Color(color.r, color.g, color.b, 0.5f));
                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                    isStop = !isStop;
                    if (isStop == true)
                    {
                        StartCoroutine(SlowlyStop());
                        GameObject Obj = Instantiate(BoomObject);
                        Obj.transform.localScale *= 1.3f;
                        Obj.transform.position = transform.position;
                        GameObject Obj2 = Instantiate(BoomObject);
                        Obj2.transform.position = transform.position + new Vector3(2.0f, 0.0f, 0.0f);
                        Obj2.transform.localScale *= 1.3f;

                    }
                    else
                        dolly.m_Speed = 20.0f;
                }
            }
            else
            {
                for (int i = 0; i < 5; ++i)
                {
                    string temp = "Materials/" + mesh[i].material.name.Replace(" (Instance)", "");
                    mesh[i].material = Resources.Load(temp) as Material;
                }
            }
        }

    

    }


    //Person Check
     IEnumerator Plus(GameObject _Target, int _Index)
     {

        ScoreList.Add(0);
        while(true)
        {
            if (Vector3.Distance(_Target.transform.position, transform.position) < 20.0f)
            {
                Score score = GameObject.Find("Score").GetComponent<Score>();
                for(int i = 0; i < ScoreList.Count; ++i)
                {
                    if(i != _Index && ScoreList[i] > 0)
                    {
                        score.SetScore(ScoreList[i]);
                        ScoreList[i] = 0;
                        TempAudio.PlayOneShot(Resources.Load("Audio/Coin") as AudioClip);
                        GameObject coin = Resources.Load("Prefabs/Coin") as GameObject;
                        coin.transform.position = transform.position;
                        for (int j = 0; j < CubeList.Count; ++j)
                        {
                            CubeList[j].SetActive(false);
                            //CubeList[j].transform.position
                            
                        }

                        yield return new WaitForSeconds(0.5f);
                    }
                }
                Transform[] childList = GameObject.Find(_Target.name).
                gameObject.GetComponentsInChildren<Transform>();
                if (childList.Length > 2)
                {
                    if (childList != null)
                    {
                        for (int i = 1; i < childList.Length; ++i)
                        {
                            if (childList[i] != transform && ScoreList[_Index] < 9)
                            {
                                
                                ScoreList[_Index]++;
                                TempAudio.PlayOneShot(Resources.Load("Audio/Swipe2") as AudioClip);
                                CubeList[ScoreList[_Index] - 1].SetActive(true);
                                GameObject coinEffect = Instantiate(Coin);
                                coinEffect.transform.position = new Vector3(
                                    CubeList[ScoreList[_Index] - 1].transform.position.x,
                                    CubeList[ScoreList[_Index] - 1].transform.position.y + 3.0f,
                                    CubeList[ScoreList[_Index] - 1].transform.position.z);
                                Destroy(coinEffect.gameObject, 0.5f);
                                StartCoroutine(Move(childList[i].transform));
                            }
                        }
                    }
                }
            }
            yield return new WaitForSeconds(1.0f);

        }
        

    }


    // Person 이동
    IEnumerator Move(Transform _transform)
    {

        while (true)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, transform.position, 3);
            if (Vector3.Distance(_transform.position, transform.position) < 0.5f)
                break;
            yield return null;
        }
        Destroy(_transform.gameObject, 0.2f);

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy" || collision.transform.tag == "Player")
        {
            Debug.Log("Collision!!");
           
            FindObjectOfType<GameManager>().EndGame();
            GameObject Obj = Instantiate(Resources.Load("Prefabs/Explosion") as GameObject);
            Obj.transform.position = transform.position;

        }
    }

    //Stop
    IEnumerator SlowlyStop()
    {
        isStop = true;
        while (dolly.m_Speed > 0.0f)
        {
            dolly.m_Speed -= Time.deltaTime * 50;
            transform.Rotate(0.0f, 0.0f, 15);

            yield return null;
        }
        dolly.m_Speed = 0.0f;

    }

}
