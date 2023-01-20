using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Information : MonoBehaviour
{
    public static Information Instance;
    private int pIndex = 1;
    public int StageNum;
    public List<int> StageScore;
    public Dictionary<string, int> StageStar = new Dictionary<string, int>();
    [SerializeField] private Timer timer;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        for(int i = 1; i < 10; ++i)
        {
            StageStar.Add("Stage" + i.ToString(), 0);
        }
    }

    public void PlayerSelect(int _Index)
    {
        pIndex = _Index;
    }
    public int GetIndex()
    {
        return pIndex;
    }
    public void StageSelect(int _num)
    {
        StageNum = _num;
    }

    public void Index(int n)
    {
        pIndex += n;
    }

    public void SetStar()
    {
        timer = GameObject.Find("Time").GetComponent<Timer>();
        StageStar[SceneManager.GetActiveScene().name] = timer.Star;
        Debug.Log(StageStar[SceneManager.GetActiveScene().name] + "star");
    }

    public int GetStar(string Stage)
    {
        return StageStar[Stage];
    }
}

