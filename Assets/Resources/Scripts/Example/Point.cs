using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Point : MonoBehaviour
{
    public GameObject Target;
    [SerializeField] private List<MeshRenderer> RendererList = new List<MeshRenderer>();
    [HideInInspector] public Point Node;
    [SerializeField] private LayerMask mask;

    private void Awake()
    {
       // mask.value = (1<<4) | (1<<7) |(1 << 11);
        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.constraints = RigidbodyConstraints.FreezeRotation;

        Target = Resources.Load("Prefabs/Enemy") as GameObject;

        
    }


    void FindRenderer(GameObject _Obj)
    {
        for (int i = 0; i < _Obj.transform.childCount; ++i)
        {
            if (_Obj.transform.childCount > 0)
                FindRenderer(_Obj.transform.GetChild(i).gameObject);

            MeshRenderer renderer = _Obj.transform.GetChild(i).GetComponent<MeshRenderer>();

            if (renderer != null)
                RendererList.Add(renderer);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.0f);

        if (Node)
            Gizmos.DrawLine(transform.position, Node.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        int layer = (1 << collision.gameObject.layer);
        if((layer & mask ) == layer)
        {
            StartCoroutine(Create());
            return;
        }

        transform.position = new Vector3(
            Random.Range(-25.0f, 25.0f),
            Random.Range(10.0f, 25.0f),
            Random.Range(-25.0f, 25.0f));

        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

    }

    IEnumerator Create()
    {
        while(true)
        {
            yield return new WaitForSeconds(3.0f);

            if (transform.childCount >= 1)
                continue;

            GameObject Obj = Instantiate(Target);

            Obj.transform.position = transform.position;
            Obj.transform.parent = transform;
            
            RendererList.Clear();

            FindRenderer(Obj);

            foreach (MeshRenderer meshRenderer in RendererList)
            {
                meshRenderer.material.shader = Shader.Find("Transparent/VertexLit");

                if (meshRenderer.material.HasProperty("_Color"))
                {
                    Color color = meshRenderer.material.GetColor("_Color");

                    meshRenderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, 0.0f));

                    StartCoroutine(SetColor(meshRenderer, color));
                }
            }
        }
    }

    IEnumerator SetColor(MeshRenderer meshRenderer, Color color)
    {
        float fTime = 0.0f;
        while (fTime <= 255.0f)
        {
            yield return null;

            fTime += Time.deltaTime;
            meshRenderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, fTime));
        }
    }

}
