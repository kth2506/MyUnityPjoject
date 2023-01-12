using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject coinObj;
    static public Coin Instance = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public IEnumerator Explosion(GameObject _Obj)
    {
        GameObject Obj = Instantiate(coinObj);

        float time = 0.0f;
        while(time <= 1.0f)
        {
            time += Time.deltaTime;
            Obj.transform.position = Vector3.Lerp(transform.position, _Obj.transform.position, time);
            yield return null;
        }
    }
}
