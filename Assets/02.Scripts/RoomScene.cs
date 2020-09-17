using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoomScene : MonoBehaviour
{
    public void StartGame()
    {
        NetWork.Get.StartGame();
    }
    public void ToLobby()
    {
        SceneManager.LoadScene("03.Lobby");
    }
}
