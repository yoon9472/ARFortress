using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayArcore : MonoBehaviour
{
    //public Button button;//호스팅 or 리졸브 버튼

    //void Start()
    //{
    //    if (NetWork.Get.isMaster == true)
    //    {
    //        //방장만 활성화
    //        button.gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        //참여자는 비활성화
    //        button.gameObject.SetActive(false);

    //    }
    //}

   
    //void Update()
    //{
    //    if(NetWork.Get.isMaster == true)
    //    {
    //        button.GetComponentInChildren<Text>().text = "Host";
            
    //        if (NetWork.Get.isMakeAnchor==true)
    //        {
    //            button.gameObject.SetActive(false);
    //        }
    //    }
    //    else
    //    {
    //        if(NetWork.Get.receiveId == true)//앵커 아이디 값을 받았으면
    //        {
    //            button.gameObject.SetActive(true); //버튼 활성화 하고
    //            button.GetComponentInChildren<Text>().text = "Resolve";//버튼 이름 바꿔준다
    //        }
    //    }
    //}
}
