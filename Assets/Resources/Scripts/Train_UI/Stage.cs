using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Stage : MonoBehaviour
{
    [SerializeField] private Text StageText;
    // Start is called before the first frame update
    void Awake()
    {
        StageText = transform.GetComponent<Text>();
        Scene scene = SceneManager.GetActiveScene();
        StageText.text = scene.name;
    }

}
