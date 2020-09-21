﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneButton : MonoBehaviour
{
    public GameObject option;
    public StoreInstantiate storeInstantiate;
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
    /// <summary>
    /// 상점 정보 가져오기
    /// </summary>
    public void OpneStore()
    {
        //NetWork.Get.GetCatalogItem("Store");
        StartCoroutine(teststore());
    }

    IEnumerator teststore()
    {
        yield return null;
        NetWork.Get.GetCatalogItem("Store");
        yield return new WaitForSeconds(1.5f);
                Debug.Log("상점불러온후");
        storeInstantiate.StoreItemList();
    }

    
}
