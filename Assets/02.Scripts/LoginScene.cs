using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoginScene : MonoBehaviour
{

    public void Login()
    {
        //버튼 클리갛면 로그인 처리후 로비로 입장
        NetWork.Get.GoogleLogin(); //구글 로그인 되면 이거 사용한다.
        //NetWork.Get.TestLogin();
        //NetWork.Get.JoinLobby();

    }
}
