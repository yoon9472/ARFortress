using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneButton : MonoBehaviour
{
    public GameObject option;
    // Start is called before the first frame update
    void Start()
    {
        //메인씬에 올때마다 인벤토리 정보를 갱신한다. //추가해야될 사항 랩Scene으로 넘어가기전 로딩화면 추가 2020.09.23
        NetWork.Get.GetInventory();
    }
    // Update is called once per frame
    void Update()
    { 
    }

    public void ToLab()
    {
        SceneManager.LoadScene("TestLabUI");
    }

    public void ToStore()
    {
        SceneManager.LoadScene("TestStore");
    }
    /// <summary>
    /// 방이생성 씬으로 이동
    /// </summary>
    public void ToPlayGame()
    {
        SceneManager.LoadScene("04.MakeRoom");
    }
    public void ToOption()
    {
        option.gameObject.SetActive(true);
    }
    public void ToCloseOption()
    {
        option.gameObject.SetActive(false);
    }
}
