using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour
{
    public static Information Instance;
    public int pIndex;
    public int StageNum;
    public List<int> StageScore;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        pIndex = 1;
    }
    public void PlayerSelect(int _Index)
    {
        pIndex = _Index;
    }
   
    public void StageSelect(int _num)
    {
        StageNum = _num;
    }
}

