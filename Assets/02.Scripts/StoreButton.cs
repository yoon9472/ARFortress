using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class StoreButton : MonoBehaviour
{
    public GameObject noCoin;
    GameObject item1;
    public GameObject instantiatePrefab;
    Text title1;
    Text introduction1;
    Text cost1;
    public Transform contents;
    int allNumber=1;
    int weaponNumber=2;
    int upperBodyNumber=3;
    int lowerBodyNumber=4;
    public int checkNumber;// 중복 버튼 눌러서 리소스 줄이기 위해서 이것을 넣음!

    string itemIdForPay;
    int itemPrice;
    // Start is called before the first frame update
    void Start()
    {
        AllItem();//처음에 all 아이템 띄워야 되기 때문에 여기에 넣음!
    }
    // Update is called once per frame
    void Update()
    {
    }
    //돈이 없어서 아이템을 못샀다고 한 창을 끌때!
    public void CloseNoCoin()
    {
        noCoin.gameObject.SetActive(false);
    }
    //뒤로가기 버튼 넣음 됨!
    public void BacktoMainScene()
    {
        SceneManager.LoadScene("03.Lobby");
    }
    //allitem의 패널 생성 하는 곳!
    void AllPanel()
    {
        for(int i =0 ; i <NetWork.Get.itemList.Count; i++)
        {
            item1 = Instantiate(instantiatePrefab , new Vector3(0,0,0), Quaternion.identity, contents);
            item1.GetComponent<PrefabPanel>().displayname = NetWork.Get.itemList[i].DisplayName;
            item1.GetComponent<PrefabPanel>().description = NetWork.Get.itemList[i].Description;
            item1.GetComponent<PrefabPanel>().itemcost = NetWork.Get.itemList[i].VirtualCurrencyPrices["GD"].ToString();
            item1.GetComponent<PrefabPanel>().itemId = NetWork.Get.itemList[i].ItemId;
            item1.GetComponent<PrefabPanel>().price = System.Convert.ToInt32(item1.GetComponent<PrefabPanel>().itemcost);
            for(int j =0; j < GameManager.Get.imgArr.Length; j++)
            {
                if(GameManager.Get.imgArr[j].name == item1.GetComponent<PrefabPanel>().displayname)
                {
                    item1.GetComponent<PrefabPanel>().image.sprite = GameManager.Get.imgArr[j];
                }
            }
        }
    }
    //weapon 패널 생성하는 곳!
    void WeaponPanel()
    {
        for(int i =0 ; i <NetWork.Get.weaponList.Count; i++)
        {
            item1 = Instantiate(instantiatePrefab , new Vector3(0,0,0), Quaternion.identity, contents);
            item1.GetComponent<PrefabPanel>().displayname = NetWork.Get.weaponList[i].DisplayName;
            item1.GetComponent<PrefabPanel>().description = NetWork.Get.weaponList[i].Description;
            item1.GetComponent<PrefabPanel>().itemcost = NetWork.Get.weaponList[i].VirtualCurrencyPrices["GD"].ToString();
            item1.GetComponent<PrefabPanel>().itemId = NetWork.Get.weaponList[i].ItemId;
            item1.GetComponent<PrefabPanel>().price = System.Convert.ToInt32(item1.GetComponent<PrefabPanel>().itemcost);
            for(int j =0; j < GameManager.Get.imgArr.Length; j++)
            {
                if(GameManager.Get.imgArr[j].name == item1.GetComponent<PrefabPanel>().displayname)
                {
                    item1.GetComponent<PrefabPanel>().image.sprite = GameManager.Get.imgArr[j];
                }
            }
        }
    }
    //upperbody 패널 생성하는 곳!
    void UpperBodyPanel()
    {
        for(int i =0 ; i <NetWork.Get.bodyList.Count; i++)
        {
            item1 = Instantiate(instantiatePrefab , new Vector3(0,0,0), Quaternion.identity, contents);
            item1.GetComponent<PrefabPanel>().displayname = NetWork.Get.bodyList[i].DisplayName;
            item1.GetComponent<PrefabPanel>().description = NetWork.Get.bodyList[i].Description;
            item1.GetComponent<PrefabPanel>().itemcost = NetWork.Get.bodyList[i].VirtualCurrencyPrices["GD"].ToString();
            item1.GetComponent<PrefabPanel>().itemId = NetWork.Get.bodyList[i].ItemId;
            item1.GetComponent<PrefabPanel>().price = System.Convert.ToInt32(item1.GetComponent<PrefabPanel>().itemcost);
            for(int j =0; j < GameManager.Get.imgArr.Length; j++)
            {
                if(GameManager.Get.imgArr[j].name == item1.GetComponent<PrefabPanel>().displayname)
                {
                    item1.GetComponent<PrefabPanel>().image.sprite = GameManager.Get.imgArr[j];
                }
            }
        }
    }
    //lowerdody 패널 생성하는 곳!
    void LowerBodyPanel()
    {
        for(int i =0 ; i <NetWork.Get.legList.Count; i++)
        {
            item1 = Instantiate(instantiatePrefab , new Vector3(0,0,0), Quaternion.identity, contents);
            item1.GetComponent<PrefabPanel>().displayname = NetWork.Get.legList[i].DisplayName;
            item1.GetComponent<PrefabPanel>().description = NetWork.Get.legList[i].Description;
            item1.GetComponent<PrefabPanel>().itemcost = NetWork.Get.legList[i].VirtualCurrencyPrices["GD"].ToString();
            item1.GetComponent<PrefabPanel>().itemId = NetWork.Get.legList[i].ItemId;
            item1.GetComponent<PrefabPanel>().price = System.Convert.ToInt32(item1.GetComponent<PrefabPanel>().itemcost);
            for(int j =0; j < GameManager.Get.imgArr.Length; j++)
            {
                if(GameManager.Get.imgArr[j].name == item1.GetComponent<PrefabPanel>().displayname)
                {
                    item1.GetComponent<PrefabPanel>().image.sprite = GameManager.Get.imgArr[j];
                }
            }
        }
    }
    //자식들 없애는 곳!
    void DestroyChild()
    {
        Debug.Log("자식 오브젝트 삭제 시작");
        for (int i =0; i < contents.childCount; i++)
        {
            Destroy(contents.GetChild(i).gameObject);
        }
    }
    //all 버튼에 넣는 것!
    public void AllItem()
    {
        if(checkNumber == 1 ) 
        {
            Debug.Log("중복은 앙대요!");
            return;
        }
        Debug.Log("중복 실험 되는지 안되는지!");
        DestroyChild();
        AllPanel();
        checkNumber = allNumber;
    }
    //weapon 버튼에 넣는 것!
    public void WeaponItem()
    {
        if(checkNumber == 2 ) 
        {
            Debug.Log("중복은 앙대요!");
            return;
        }
        Debug.Log("중복 실험 되는지 안되는지!");
        DestroyChild();
        WeaponPanel();
        checkNumber= weaponNumber;
    }
    //upperbody버튼에 넣는 것!
    public void UpperBodyItem()
    {
        if(checkNumber == 3 ) 
        {
            Debug.Log("중복은 앙대요!");
            return;
        }
        Debug.Log("중복 실험 되는지 안되는지!");
        DestroyChild();
        UpperBodyPanel();
        checkNumber = upperBodyNumber;
    }
    //lowerbody에 넣는 것!
    public void LowerBodyItem()
    {
        if(checkNumber == 4 ) 
        {
            Debug.Log("중복은 앙대요!");
            return;
        }
        //Debug.Log("중복 실험 되는지 안되는지!");
        DestroyChild();
        LowerBodyPanel();
        checkNumber = lowerBodyNumber;
    }
}