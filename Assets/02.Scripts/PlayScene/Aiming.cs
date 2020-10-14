using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Aiming : MonoBehaviour
{
    //컨트롤러에서 위아래 값을 받아 x축으로 회전시킬것임
    //컨트롤러의 좌우 값을 받아 y축으로 회전시킬것임

    public float x; //x축기준으로 위아래 회전위한 값
    public float y;//y 축 기준 좌우 회전을 위한 값
    public bool isRotateUpDown = false; //위아래 버튼을 눌렀는지
    public bool isRotateLeftRight = false; //좌우 버튼을 눌렀는지

    private void Start()
    {
        isRotateUpDown = false;
        isRotateLeftRight = false;
    }
    private void Update()
    {
        if(NetWork.Get.isMaster == true)
        {
            if(isRotateUpDown == true)
            {
                //위아래 회전중일때는 회전중인 불값과 방향에 대한 정보를 포톤으로 넘겨준다
                NetWork.Get.isRotateUpDown = true;
                NetWork.Get.rotateX = x;
            }
            else
            {
                //회전중이 아니일때 불값을 동기화한다
                NetWork.Get.isRotateUpDown = false;
            }

            if(isRotateLeftRight == true)
            {
                //좌우 회전중일때는 회전중인 불값과 방향에 대한 정보를 포톤으로 넘겨준다
                NetWork.Get.isRotateLeftRight = true;
                NetWork.Get.rotateY = y;
            }
            else
            {
                //회전중이 아닐때 불값을 동기화한다
                NetWork.Get.isRotateLeftRight = false;
            }
        }

    }
    //private void Update()
    //{
    //    if(isRotateUpDown == true)
    //    {
    //        //방향에 따른 위아래 회전 포신돌리는 부븐
    //        cube.Rotate(Vector3.right * -x );
    //    }
    //    if(isRotateLeftRight==true)
    //    {
    //        //방향에 따른 좌우 회전 포탑 돌리는 부분
    //        cube.Rotate(Vector3.up * -y);
    //    }
    //}

}
