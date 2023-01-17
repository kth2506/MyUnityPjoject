using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickController : MonoBehaviour , IDragHandler, IPointerUpHandler, IPointerDownHandler  
{
    [Header("Move Target")]
    [Tooltip("Type : GameObject    조이스틱을 사용하여 움직일 대상을 정함. ")]
    [SerializeField] private GameObject Target;

    [Header("JoyStickController")]
    [Tooltip("Type : RectTransform    실제로 움직일 버튼")]
    [SerializeField] private RectTransform Stick;
    [Tooltip("Type : RectTransform    JoyStick Out Line")]
    [SerializeField] private RectTransform BackBoard;

    // ** 반지름
    private float Radius;

    // ** 이동속도
    private float Speed;

    // ** 터치체크
    private bool TouchCheck;

    // ** Target이 움직일 방향
    private Vector2 Direction;

    // ** Target이 움직일 값
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
        // ** OutLine 의 반지름을 구함
        Radius = BackBoard.rect.width * 0.5f;

        // ** 반지름의 0.75
        Radius += Radius * 0.5f;
        
        // ** 스크린에 터치가 되었는지 확인.
        TouchCheck = false;

        // ** 방향이 없는 상태로 초기화 
        Direction = new Vector2(0.0f, 0.0f);

        // ** 이동 속도 설정
        Speed = 5.0f;

        // ** 이동값이 없는 상태로 초기화
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
        // ** Stick 의 중앙으로부터 Touch가 스크린을 이동한 길이를 구함
        Stick.localPosition = new Vector2(
            _eventData.x - BackBoard.position.x, _eventData.y - BackBoard.position.y);

        // ** Stick 이 Radius 를 벗어나지 못하게 함
        Stick.localPosition = Vector2.ClampMagnitude(Stick.localPosition, Radius);

        // ** 조이스틱이 가리키는 방향을 구한다.
        Direction = Stick.localPosition.normalized;

        // ** 조이스틱이 이동가능한 최대 거리에서 실제 이동한 비율 만큼 이동속도를 적용시킴
        float Ratio = Vector3.Distance(BackBoard.position, Stick.position) / Radius;
        //Ratio *= 100 / Radius;

        // ** 조이스틱이 움직이는 방향에 맞게 Target 을 이동시켜준다.
        Movement = new Vector3(
            Direction.x * Ratio * Speed * Time.deltaTime,
            0.0f,
            Direction.y * Ratio * Speed * Time.deltaTime);

        //Target.transform.LookAt(Target.transform.forward);

        // ** 조이스틱이 바라보는 방향을 Target이 바라보게 한다.
        FieldOfView.OffsetAngle = Vector3.Angle(Vector3.forward, Direction);

        Target.transform.eulerAngles = new Vector3(
            0.0f, Mathf.Atan2(Direction.x, Direction.y) * Mathf.Rad2Deg, 0.0f);

    }

}
