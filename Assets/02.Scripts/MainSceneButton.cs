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
    public void ToPlayGame()
    {
        SceneManager.LoadScene("03.Lobby");
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
