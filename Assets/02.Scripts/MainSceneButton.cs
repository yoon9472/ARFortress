using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneButton : MonoBehaviour
{
    public GameObject option;
    public GameObject exit;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    { 
    }
    public void ExitBtn()
    {
        exit.gameObject.SetActive(true);
    }
    public void YesQuitBtn()
    {
#if UNITY_EDITIOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Debug.Log("종료를 시작합니다.");
        Application.Quit();
#endif
    }
    public void NoQuitBtn()
    {
        exit.gameObject.SetActive(false);
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
