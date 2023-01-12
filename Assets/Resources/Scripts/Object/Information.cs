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
    public Dictionary<string, int> StageStar;
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

    public void SetStar()
    {
        timer = GameObject.Find("Time").GetComponent<Timer>();
        StageStar[SceneManager.GetActiveScene().name] = timer.Star;
    }

    public int GetStar(string Stage)
    {
        return StageStar[Stage];
    }
}

