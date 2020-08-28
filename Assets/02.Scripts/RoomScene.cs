using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScene : MonoBehaviour
{
    public void StartGame()
    {
        NetWork.Get.StartGame();
    }
}
