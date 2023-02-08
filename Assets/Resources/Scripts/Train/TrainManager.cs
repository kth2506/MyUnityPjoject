using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class TrainManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> StationList;
    [SerializeField] private GameObject Coin;
    [SerializeField] private MeshRenderer[] mesh;
    [SerializeField] private List<MeshRenderer> meshList;
    [SerializeField] private AudioSource TempAudio;
    [SerializeField] private List<GameObject> CubeList;

    [SerializeField] private List<GameObject> BodyList;
    [SerializeField] private Transform PlayerPosition;
    [SerializeField] private List<float> BodySizeList;
    
    private GameObject BoomObject;

    private List<int> ScoreList = new List<int>();
    private bool isStop;

    // Start is called before the first frame update
    private void Awake()
    {
        //  Scene이 Stage 일때만 작동
        if (!SceneManager.GetActiveScene().name.Contains("Stage"))
            GetComponent<TrainManager>().enabled = false;
    }

    private void Start()
    {
        Initialized();
    }
    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Stop & mesh 
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == transform)
            {

                for (int i = 0; i < meshList.Count; ++i)
                {
                    meshList[i].material.shader = Shader.Find("Transparent/VertexLit");

                    if (meshList[i].material.HasProperty("_Color"))
                    {
                        Color color = meshList[i].material.GetColor("_Color");
                        meshList[i].material.SetColor("_Color", new Color(color.r, color.g, color.b, 0.5f));
                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                    SlowlyStop();
                }
            }
            else
            {
                for (int i = 0; i < meshList.Count; ++i)
                {
                    string temp = "Materials/" + meshList[i].material.name.Replace(" (Instance)", "");
                    meshList[i].material = Resources.Load(temp) as Material;
                }
            }
        }

    }


    //Person Check
    IEnumerator Plus(GameObject _Target, int _Index)
    {

        ScoreList.Add(0);
        while (true)
        {


            if (Vector3.Distance(_Target.transform.position, PlayerPosition.position) < 18.0f)
            {

                Score score = GameObject.Find("Score").GetComponent<Score>();
                for (int i = 0; i < ScoreList.Count; ++i)
                {
                    if (i != _Index && ScoreList[i] > 0)
                    {
                        score.SetScore(ScoreList[i]);
                        ScoreList[i] = 0;
                        TempAudio.PlayOneShot(Resources.Load("Audio/Coin") as AudioClip);

                        for (int j = 0; j < CubeList.Count; ++j)
                        {
                            CubeList[j].SetActive(false);
                        }

                        yield return new WaitForSeconds(0.7f);
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
                            if (childList[i] != PlayerPosition && ScoreList[_Index] < CubeList.Count)
                            {
                                ScoreList[_Index]++;
                                GameObject coin = Resources.Load("Prefabs/Coin") as GameObject;
                                coin.transform.position = PlayerPosition.position;
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
            yield return new WaitForSeconds(1.3f);

        }


    }


    // Person �̵�
    IEnumerator Move(Transform _transform)
    {
        while (true)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, PlayerPosition.position, 3);
            if (Vector3.Distance(_transform.position, PlayerPosition.position) < 0.5f)
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

            GameObject Obj = Instantiate(Resources.Load("Prefabs/Explosion") as GameObject);
            Obj.transform.position = PlayerPosition.position;

            foreach (GameObject element in BodyList)
            {
                element.GetComponent<CinemachineDollyCart>().enabled = false;
                element.GetComponent<BodyController>().enabled = false;
                element.AddComponent<Rigidbody>();
                float ran = Random.RandomRange(1000, 1500);
                element.GetComponent<Rigidbody>().AddForce(new Vector3(ran, ran, ran));
            }

            StartCoroutine(FindObjectOfType<GameManager>().EndGame());
        }
    }

    //Stop
    public void SlowlyStop()
    {
        isStop = !isStop;
        if (isStop == true)
        {
            GameObject Obj = Instantiate(BoomObject);
            Obj.transform.localScale *= 1.3f;
            Obj.transform.position = PlayerPosition.position;
            GameObject Obj2 = Instantiate(BoomObject);
            Obj2.transform.position = PlayerPosition.position + new Vector3(2.0f, 0.0f, 0.0f);
            Obj2.transform.localScale *= 1.3f;
            isStop = true;

            List<CinemachineDollyCart> dollyList = new List<CinemachineDollyCart>();
            foreach(GameObject element in BodyList)
            {
                dollyList.Add(element.GetComponentInChildren<CinemachineDollyCart>());
            }
            foreach(CinemachineDollyCart element in dollyList)
            {
                element.m_Speed = 0.0f;
            }

        }
        else
        {
            foreach (GameObject element in BodyList)
            {
                CinemachineDollyCart dolly = element.GetComponentInChildren<CinemachineDollyCart>();
                dolly.m_Speed = 20.0f;
            }
        }

    }

    private void Initialized()
    {
        isStop = false;

        foreach (GameObject element in BodyList)
        {
            meshList.Add(element.GetComponentInChildren<MeshRenderer>());
            Collider col = element.GetComponentInChildren<Collider>();

            BodySizeList.Add(col.bounds.max.z - element.transform.position.z);
            BodySizeList.Add(element.transform.position.z - col.bounds.min.z);
        }

        for (int i = 0; i < BodyList.Count; ++i)
        {
            float temp = 0;
            for (int j = i * 2 + 1; j < BodySizeList.Count - 1; ++j)
            {
                temp += BodySizeList[j];
                if (j % 2 == 0)
                    temp += 0.1f;
            }
            BodyList[i].GetComponent<BodyController>().SetOffset(temp);
        }


        BoomObject = Resources.Load("Prefabs/PlayerBoom") as GameObject;
        TempAudio = GameObject.Find("EffectSound").GetComponent<AudioSource>();


        GameObject[] stationList = GameObject.FindGameObjectsWithTag("Station");

        if (stationList != null)
        {
            foreach (GameObject element in stationList)
            {
                StationList.Add(element);
            }
            for (int i = 0; i < StationList.Count; ++i)
                StartCoroutine(Plus(StationList[i], i));
        }
    }

}
