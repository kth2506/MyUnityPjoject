using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
            Debug.Log("Collision!!");
        if (collision.transform.tag == "Enemy" || collision.transform.tag == "Player")
        {
            AudioSource audio = Camera.main.GetComponent<AudioSource>();
            audio.Stop();
            audio.PlayOneShot(Resources.Load("Audio/crash") as AudioClip);
            audio.PlayOneShot(Resources.Load("Audio/End Defeat") as AudioClip);
            GameObject Obj = Instantiate(Resources.Load("Prefabs/PlayerBoom") as GameObject);

            GameManager gameManager;
            gameManager = FindObjectOfType<GameManager>();
            Obj.transform.position = transform.position;
            gameManager.RestartGame();
            Destroy(transform.gameObject);

        }
    }
}
