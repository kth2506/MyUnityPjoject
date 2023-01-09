using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickController : MonoBehaviour , IDragHandler, IPointerUpHandler, IPointerDownHandler  
{
    [Header("Move Target")]
    [Tooltip("Type : GameObject    ���̽�ƽ�� ����Ͽ� ������ ����� ����. ")]
    [SerializeField] private GameObject Target;

    [Header("JoyStickController")]
    [Tooltip("Type : RectTransform    ������ ������ ��ư")]
    [SerializeField] private RectTransform Stick;
    [Tooltip("Type : RectTransform    JoyStick Out Line")]
    [SerializeField] private RectTransform BackBoard;

    // ** ������
    private float Radius;

    // ** �̵��ӵ�
    private float Speed;

    // ** ��ġüũ
    private bool TouchCheck;

    // ** Target�� ������ ����
    private Vector2 Direction;

    // ** Target�� ������ ��
    private Vector3 Movement;  

    public void OnDrag(PointerEventData eventData)
    {
        TouchCheck = true;
        OnTouch(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TouchCheck = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TouchCheck = false;
        Stick.localPosition = Vector2.zero;
    }

    private void Awake()
    {
        Target = GameObject.Find("Player");
        Stick = GameObject.Find("FilledCircle").GetComponent<RectTransform>();
        BackBoard = GameObject.Find("OutLineCircle").GetComponent<RectTransform>();

    }

    void Start()
    {
        // ** OutLine �� �������� ����
        Radius = BackBoard.rect.width * 0.5f;

        // ** �������� 0.75
        Radius += Radius * 0.5f;
        
        // ** ��ũ���� ��ġ�� �Ǿ����� Ȯ��.
        TouchCheck = false;

        // ** ������ ���� ���·� �ʱ�ȭ 
        Direction = new Vector2(0.0f, 0.0f);

        // ** �̵� �ӵ� ����
        Speed = 5.0f;

        // ** �̵����� ���� ���·� �ʱ�ȭ
        Movement = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        if (TouchCheck)
            Target.transform.position += Movement;

        if (Input.GetMouseButtonDown(0))
        {
            BackBoard.localPosition = new Vector2(
                Input.mousePosition.x - transform.position.x,
                Input.mousePosition.y - transform.position.y);
            TouchCheck = true;

        }
        if (Input.GetMouseButton(0))
        {
            OnTouch(Input.mousePosition);
            TouchCheck = true;

        }
        if (Input.GetMouseButtonUp(0))
        {
            BackBoard.localPosition = new Vector2(
                100.0f - transform.position.x,
                100.0f - transform.position.y);
            TouchCheck = false;
            Stick.localPosition = Vector2.zero;

        }
    }

    private void OnTouch(Vector2 _eventData)
    {
        // ** Stick �� �߾����κ��� Touch�� ��ũ���� �̵��� ���̸� ����
        Stick.localPosition = new Vector2(
            _eventData.x - BackBoard.position.x, _eventData.y - BackBoard.position.y);

        // ** Stick �� Radius �� ����� ���ϰ� ��
        Stick.localPosition = Vector2.ClampMagnitude(Stick.localPosition, Radius);

        // ** ���̽�ƽ�� ����Ű�� ������ ���Ѵ�.
        Direction = Stick.localPosition.normalized;

        // ** ���̽�ƽ�� �̵������� �ִ� �Ÿ����� ���� �̵��� ���� ��ŭ �̵��ӵ��� �����Ŵ
        float Ratio = Vector3.Distance(BackBoard.position, Stick.position) / Radius;
        //Ratio *= 100 / Radius;

        // ** ���̽�ƽ�� �����̴� ���⿡ �°� Target �� �̵������ش�.
        Movement = new Vector3(
            Direction.x * Ratio * Speed * Time.deltaTime,
            0.0f,
            Direction.y * Ratio * Speed * Time.deltaTime);

        //Target.transform.LookAt(Target.transform.forward);

        // ** ���̽�ƽ�� �ٶ󺸴� ������ Target�� �ٶ󺸰� �Ѵ�.
        FieldOfView.OffsetAngle = Vector3.Angle(Vector3.forward, Direction);

        Target.transform.eulerAngles = new Vector3(
            0.0f, Mathf.Atan2(Direction.x, Direction.y) * Mathf.Rad2Deg, 0.0f);

    }

}
