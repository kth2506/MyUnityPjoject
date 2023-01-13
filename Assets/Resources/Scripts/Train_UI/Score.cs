using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ������ ȹ���ϰ� MaxScore�� ������ StageClear�� ����
public class Score : MonoBehaviour
{
    //Stage�� MaxScore
    [SerializeField] private float MaxScore;
    public float PlayerScore;
    [SerializeField] private Text ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        PlayerScore = 0;
        ScoreText = transform.GetComponent<Text>();
        ScoreText.text = string.Format("{0:n0}", (int)PlayerScore) + " / " + string.Format("{0:n0}", (int)MaxScore);

    }

    public void SetScore(int _PlayerScore)
    {
        PlayerScore += _PlayerScore;
        StartCoroutine(Increase(PlayerScore, PlayerScore - _PlayerScore));
    }

    public IEnumerator Increase(float target, float current)
    {

        float duration = 0.5f;
        float offset = (target - current) / duration;

        while(current < target)
        {
            current += offset * Time.deltaTime*0.1f;
            ScoreText.text = string.Format("{0:n0}", (int)target) + " / " + string.Format("{0:n0}", (int)MaxScore);
            yield return null;
        }

        target = current;

        ScoreText.text = string.Format("{0:n0}", (int)target) + " / " + string.Format("{0:n0}", (int)MaxScore);
       
        if (PlayerScore >= MaxScore)
            FindObjectOfType<GameManager>().StageClear();
    }


}
