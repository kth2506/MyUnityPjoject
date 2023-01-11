using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class FrustumLine : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3[] CameraFrustum = new Vector3[4];
    [SerializeField] private List<MeshRenderer> RendererList = new List<MeshRenderer>();
    [SerializeField] private List<GameObject> CullingList = new List<GameObject>();
    private List<MeshRenderer> TempList = new List<MeshRenderer>();

    [SerializeField] private LayerMask mask;
    [SerializeField] private float Distance;
    public Shader testShader;
    [Range(0.0f, 1.0f)]
    public float x, y, cx, cy;

    private void Awake()
    {
        x = y = 0.45f;
        cx = cy = 0.1f;
        Distance = 20.0f;
        mainCamera = transform.GetComponent<Camera>();

        for(int i = 0; i < CameraFrustum.Length; ++i)
            CameraFrustum[i] = new Vector3(0.0f, 0.0f, 0.0f);

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        mainCamera.CalculateFrustumCorners(
            new Rect(x, y, cx, cy), 
            mainCamera.farClipPlane, 
            Camera.MonoOrStereoscopicEye.Mono,
            CameraFrustum);

        CullingList.Clear();

        for (int i = 0; i < CameraFrustum.Length; ++i)
        {
            var worldSpaceCorner = mainCamera.transform.TransformVector(CameraFrustum[i]).normalized;
            Debug.DrawRay(mainCamera.transform.position, worldSpaceCorner * Distance, Color.black);

            Ray ray = new Ray(mainCamera.transform.position, worldSpaceCorner);

            RaycastHit[] hits = Physics.RaycastAll(ray, Distance, mask);

            foreach (RaycastHit hit in hits)
                if(!CullingList.Contains(hit.transform.gameObject))
                    CullingList.Add(hit.transform.gameObject);

        }

        RendererList.Clear();

        foreach (GameObject Element in CullingList)
            StartCoroutine(FindRenderer(Element));

        if(RendererList.Count > 0)
            TempList = RendererList.ToList();
        else if(TempList.Count > 0)
        {
            foreach (MeshRenderer Element in TempList)
            {
                 string temp = "Materials/" + Element.material.name.Replace(" (Instance)", "");
                 Element.material = Resources.Load(temp) as Material;
            }
            TempList.Clear();
        }

        foreach (MeshRenderer Element in RendererList)
        {
            Element.material.shader = Shader.Find("Transparent/VertexLit");

            if (Element.material.HasProperty("_Color"))
            {
                Color color = Element.material.GetColor("_Color");

                StartCoroutine(SetColor(Element, color));
            }
        }
    }

    IEnumerator FindRenderer(GameObject _Obj)
    {
        int i = 0;

        do
        {
            if (_Obj.transform.childCount > 0)
                FindRenderer(_Obj.transform.GetChild(i).gameObject);

            MeshRenderer renderer = _Obj.transform.GetComponent<MeshRenderer>();

            if (renderer != null)
                RendererList.Add(renderer);
            i++;
        } while (i < _Obj.transform.childCount);


        yield return null;
    }
   
    IEnumerator SetColor(MeshRenderer meshRenderer, Color color)
    {
        float fTime = 1.0f;
        while (fTime >= 0.5f)
        {
            yield return null;

            fTime -= Time.deltaTime * 1.3f;
            meshRenderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, fTime));
        }
    }
   

}
