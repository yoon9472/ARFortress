using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviour
{
    
    public InputField roomName;//입력받을 필드
    /// <summary>
    /// 방생성
    /// </summary>
    public void CreateRoom()
    {
        NetWork.Get.CreateRoom(roomName.text); //입력한 텍스트로 방생성
    }
    /// <summary>
    /// 방참가
    /// </summary>
    public void JoinRoom()
    {
        NetWork.Get.JoinRoom(roomName.text);
    }
}
