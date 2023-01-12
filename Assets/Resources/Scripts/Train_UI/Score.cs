using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Score : MonoBehaviour
{
    private int MaxScore;
     private int PlayerScore;
    [SerializeField] private Text ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        MaxScore = 20;
        PlayerScore = 0;
        ScoreText = transform.GetComponent<Text>();

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        ScoreText.text = PlayerScore.ToString() + " / " + MaxScore.ToString();

        if (PlayerScore >= 20)
            GameManager.Instance.StageClear();
    }

    public void Increase(int _Score)
    {
        PlayerScore += _Score;
    }

    
}
