using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RoomScene : MonoBehaviour
{
    public void StartGame()
    {
        PhotonManager.Instance.StartGame();
    }
    public void ToLobby()
    {
        PhotonManager.Instance.LeaveRoom();
    }

    [SerializeField]
    protected Image ProfilePhoto0;
    [SerializeField]
    protected Image ProfilePhoto1;
    [SerializeField]
    protected Image ProfilePhoto2;
    [SerializeField]
    protected Image ProfilePhoto3;
    [SerializeField]
    protected Text nickName0;
    [SerializeField]
    protected Text nickName1;
    [SerializeField]
    protected Text nickName2;
    [SerializeField]
    protected Text nickName3;

    protected DataManager dtm;
    private void Awake()
    {
        //여기서 포톤으루 몇번째 순서인지 알게 하고.

    }
    private void Start()
    {
        //여기서 닉네임과 이미지에 띄어 두자!
               
    }
}
