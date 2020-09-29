using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //키보드 마우스 터치를 오브젝트에 보낼수 있는 기능을 지원
public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(10f, 150f)]
    private float leverRange;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        leverRange = 70f;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin");
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        //lever.anchoredPosition = inputDir;
        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;

        lever.anchoredPosition = clampedDir;
    
    }
    // 오브젝트를 클릭해서 드래그 하는 도중에 들어오는 이벤트
    // 하지만 클릭을 유지한 상태로 마우스를 멈추면 이벤트가 들어오지 않음
    public void OnDrag(PointerEventData eventData)
    {
        ////Debug.Log("Begin");
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        //lever.anchoredPosition = inputDir;
        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;

        lever.anchoredPosition = clampedDir;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End");
        lever.anchoredPosition = Vector2.zero;
    }

}
