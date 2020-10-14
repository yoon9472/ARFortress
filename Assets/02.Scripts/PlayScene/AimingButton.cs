using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;//이벤트 시스템 사용

public class AimingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ButtonRoll
    {
        up,//0
        down,//1
        left,//2
        right//3
    }
    [SerializeField]
    protected ButtonRoll roll;
    [SerializeField]
    protected Aiming aiming;
    public void OnPointerDown(PointerEventData eventData)
    {
        //버튼을 누르고 있을때
        switch(roll)
        {
            case ButtonRoll.up:
                //위버튼을 눌렀을때 
                aiming.isRotateUpDown = true;
                aiming.x = 1;
                break;
            case ButtonRoll.down:
                //아래 버튼을 눌렀을때
                aiming.isRotateUpDown = true;
                aiming.x = -1;
                break;
            case ButtonRoll.left:
                //왼쪽 버튼을 눌렀을때
                aiming.isRotateLeftRight = true;
                aiming.y = 1;
                break;
            case ButtonRoll.right:
                //오른쪽 버튼을 눌렀을때
                aiming.isRotateLeftRight = true;
                aiming.y = -1;
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //버튼에서 떼고 있을때
        aiming.isRotateUpDown = false;
        aiming.isRotateLeftRight = false;
    }
}

