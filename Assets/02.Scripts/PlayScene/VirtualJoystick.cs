using UnityEngine;
using UnityEngine.EventSystems; // 키보드, 마우스, 터치를 이벤트로 오브젝트에 보낼 수 있는 기능을 지원
public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public  RectTransform controller;//컨트롤러 렉트 트랜스폼
    public  RectTransform rectTransform;//조이스틱판 렉트 트랜스폼
    private Vector2 inputDir;//컨트롤러 입력값
    private Vector3 clampDir;//컨트롤러가 최대범위 내로 움직이게 입력값을 제한값
    public Vector2 inputVector;//캐릭터를 움직이기 위한 입력값
    public bool isInput = false;//드래그 입력값이 있는지 체크
    [SerializeField, Range(10f, 150f)]
    public float controllerRange; //컨트롤러 움직일수 있는 범위
    /// <summary>
    /// 드래그 시작할때 호출되는 이벤트
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)

    {
        Debug.Log("드래그 시작");
        ControllJoystick(eventData);
        isInput = true;
    }
    /// <summary>
    /// 드래그중 일때 호출되는 이벤트
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("드래그중");
        ControllJoystick(eventData);
       
    }
    /// <summary>
    /// 드래그가 끝났을때 호출되는 이벤트
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("드래그 끝");
        controller.anchoredPosition = Vector2.zero;
        isInput = false;
    }
    /// <summary>
    /// 이벤트 값을 받아서 컨트롤러 위치변경및 위치 벡터 가져오기
    /// </summary>
    /// <param name="eventData"></param>
    private void ControllJoystick(PointerEventData eventData)
    {
        inputDir = eventData.position - rectTransform.anchoredPosition;
        //컨트롤러 움직인 범위가 최대 움직임 범위를 넘어갔는지 체크
        if (inputDir.magnitude < controllerRange)
        {
            //최대 범위를 안지났으면 입력값 그대로 넣기
            clampDir = inputDir;
        }
        else
        {
            //최대 범위를 벗어나면 방향만 정규화한다음 최대거리 만큼 곱해서 넣기
            clampDir = inputDir.normalized * controllerRange;
        }
        //컨트롤러의 위치는 clampDir을 넣어준다
        controller.anchoredPosition = clampDir;
        //입력값은 UI 해상도 기준이라 그냥 쓰면 너무 큰 벡터값
        //최대 거리로 나눠서 0~1사이의 값을 만들어줘야한다 
        //방향만 알수 있게 정규화
        inputVector = clampDir / controllerRange;
    }
}




