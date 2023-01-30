using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StageSelect : MonoBehaviour
{
    private void Start()
    {
        SelectStage();
    }
    public void SelectStage()
    {
        Transform group = GameObject.Find("StageSelect").transform;
        int count = group.childCount;

        for (int i = 0; i < count; ++i)
        {
            Transform btn = group.GetChild(i);
            int index = i + 1;
            btn.GetComponent<Button>().onClick.AddListener(() => OnLoadLevel(index));
        }
    }

    public void OnLoadLevel(int level)
    {
        SceneManager.LoadScene("Stage" + level);
    }
}
