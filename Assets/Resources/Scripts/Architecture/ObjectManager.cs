using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ObjectManager : MonoBehaviour
{
    static private ObjectManager Instance = null;
    public List<Object> ObjectList;
    private List<int> ScoreList = new List<int>();
    [SerializeField] private AudioSource TempAudio;
    [SerializeField] private List<GameObject> CubeList;
    public GameObject Player;
    [SerializeField] private List<GameObject> StationList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        //if ((SceneManager.GetActiveScene()).name.Contains("Stage"))
        //{
        //    StationList.Add(GameObject.Find("Station_0"));
        //    StationList.Add(GameObject.Find("Station_1"));
        //    for (int i = 0; i < StationList.Count; ++i)
        //        StartCoroutine(Plus(StationList[i], i));
        //}
    }


    public void DeleteObject(Object _Object)
    {
        ObjectList.Remove(_Object);
    }


    IEnumerator Plus(GameObject _Target, int _Index)
    {
        ScoreList.Add(0);
        while (true)
        {
            if (Vector3.Distance(_Target.transform.position, Player.transform.position) < 20.0f)
            {
                Score score = GameObject.Find("Score").GetComponent<Score>();
                for (int i = 0; i < ScoreList.Count; ++i)
                {
                    if (i != _Index && ScoreList[i] > 0)
                    {
                        //score.Increase(ScoreList[i]);
                        ScoreList[i] = 0;
                        TempAudio.PlayOneShot(Resources.Load("Audio/Coin") as AudioClip);

                        GameObject coin = Resources.Load("Prefabs/Coin") as GameObject;
                        coin.transform.position = Player.transform.position;
                        for (int j = 0; j < CubeList.Count; ++j)
                            CubeList[j].SetActive(false);

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
                            if (childList[i] != Player.transform && ScoreList[_Index] < 9)
                            {

                                ScoreList[_Index]++;
                                TempAudio.PlayOneShot(Resources.Load("Audio/Swipe2") as AudioClip);
                                CubeList[ScoreList[_Index] - 1].SetActive(true);
                                StartCoroutine(Move(childList[i].transform));
                            }
                        }
                    }
                }
            }
            yield return new WaitForSeconds(1.0f);

        }


    }


    IEnumerator Move(Transform _transform)
    {

        while (true)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, Player.transform.position, 3);
            if (Vector3.Distance(_transform.position, Player.transform.position) < 0.5f)
                break;
            yield return null;
        }
        Destroy(_transform.gameObject, 0.2f);

    }
}
