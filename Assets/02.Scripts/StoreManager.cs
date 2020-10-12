using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab.ClientModels;

//StoreManager, StoreBehaviour, StoreUI
public class StoreManager : MonoBehaviour
{
    [SerializeField]
    protected GameObject rightBtnObj;
    [SerializeField]
    protected GameObject leftBtnObj;
    // PrefabPanel item1;
    [SerializeField]
    protected PrefabPanel instantiateItemPrefab;
    [SerializeField]
    protected Transform itemPrefabParentTransform;
    
    //이것은 버튼을 누르면 중복을 줄이기 위한 변수들
    protected int allNumber=1;
    protected int weaponNumber=2;
    protected int upperBodyNumber=3;
    protected int lowerBodyNumber=4;
    public int checkNumber = 0;// 중복 버튼 눌러서 리소스 줄이기 위해서 이것을 넣음!
    
    //이것은 버튼이 선택되면 선택 되어있도록 하기 위한 변수
    public GameObject allButton;
    public GameObject weaponButton;
    public GameObject upperButton;
    public GameObject lowerButton;

    public Sprite highlightImage;
    public Sprite normalImage;

    //내 돈 표시.
    //public GameObject mymoney;
    [SerializeField]
    protected Text userMoneyText;

    //스크롤바 value값 0으로 만들기
    [SerializeField]
    protected Scrollbar scrollbar;
    // Start is called before the first frame update
    void Start()
    {
        
        NetWork.Get.onChangeMoneyDelegate += SetUsermoney;
        SetUsermoney(NetWork.Get.GetMyMoney());
        AllItem();//처음에 all 아이템 띄워야 되기 때문에 여기에 넣음!
        
    }
    // Update is called once per frame
    void Update()
    {
        if(scrollbar.value == 0)
        {
            leftBtnObj.SetActive(false);
            rightBtnObj.SetActive(true);
        }
        else if(scrollbar.value == 1)
        {
            leftBtnObj.SetActive(true);
            rightBtnObj.SetActive(false);
        }
        else
        {
            leftBtnObj.SetActive(true);
            rightBtnObj.SetActive(true);
        }
    }
    public void OnClickedRightBtn()
    {
        if(checkNumber ==1){scrollbar.value += 0.312f;}
        else{ scrollbar.value +=1;}
    }
    public void OnClikedLeftBtn()
    {
        if(checkNumber ==1){scrollbar.value -= 0.312f;}
        else{ scrollbar.value -=1;}
    }
    public void ResetScroll()
    {
        scrollbar.value = 0;
    }
    public void SetUsermoney(int currentMoney)
    {
        //if (userMoneyText == null) userMoneyText = mymoney.GetComponentInChildren<Text>();
        userMoneyText.text = currentMoney.ToString();
    }
    //checknumber에 따른 이미지 바꿈!
    void checkImage(int k)
    {
        switch(k)
        {
            case 1 :
                Debug.Log("하이");
                allButton.GetComponent<Image>().sprite = highlightImage;
                weaponButton.GetComponent<Image>().sprite = normalImage;
                upperButton.GetComponent<Image>().sprite = normalImage;
                lowerButton.GetComponent<Image>().sprite = normalImage;
                ResetScroll();
            break;
            case 2 :
                Debug.Log("되라!");
                allButton.GetComponent<Image>().sprite = normalImage;
                weaponButton.GetComponent<Image>().sprite = highlightImage;
                upperButton.GetComponent<Image>().sprite = normalImage;
                lowerButton.GetComponent<Image>().sprite = normalImage;
                ResetScroll();
            break;
            case 3 :
                Debug.Log("제발!");
                allButton.GetComponent<Image>().sprite = normalImage;
                weaponButton.GetComponent<Image>().sprite = normalImage;
                upperButton.GetComponent<Image>().sprite = highlightImage;
                lowerButton.GetComponent<Image>().sprite = normalImage;
                ResetScroll();
            break;
            case 4 :
                Debug.Log("된당!");
                allButton.GetComponent<Image>().sprite = normalImage;
                weaponButton.GetComponent<Image>().sprite = normalImage;
                upperButton.GetComponent<Image>().sprite = normalImage;
                lowerButton.GetComponent<Image>().sprite = highlightImage;
                ResetScroll();
            break;
        
        }
    }

    //뒤로가기 버튼 넣음 됨!
    public void OnClickedBackButton()
    {
        SceneManager.LoadScene("03.Lobby");
    }

    protected void UpdateItemPanel(List<CatalogItem> itemList) 
    {
         for(int i =0 ; i <itemList.Count; i++)
         {
            PrefabPanel item1 = Instantiate(instantiateItemPrefab , new Vector3(0,0,0), Quaternion.identity, itemPrefabParentTransform);
            CatalogItem networkItem = itemList[i];

            ItemData data = new ItemData();
            data.id = networkItem.ItemId;
            data.displayName = networkItem.DisplayName;
            data.descripition = networkItem.Description;
            data.price = (int)networkItem.VirtualCurrencyPrices["GD"];
            data.cost = data.price.ToString();

            item1.SetPrefabData(data);
            // item1.SetPrefabData(new ItemData(networkItem.ItemId, networkItem.DisplayName, networkItem.Description, networkItem.VirtualCurrencyPrices["GD"].ToString(),
            // networkItem.VirtualCurrencyPrices["GD"]));
         }
    }
    //allitem의 패널 생성 하는 곳!
    // void AllPanel()
    // {
    //     for(int i =0 ; i <NetWork.Get.itemList.Count; i++)
    //     {
    //         PrefabPanel item1 = Instantiate(instantiateItemPrefab , new Vector3(0,0,0), Quaternion.identity, contents);
    //         CatalogItem networkItem = NetWork.Get.itemList[i];

    //         item1.SetPrefabData(new ItemData(networkItem.ItemId, networkItem.DisplayName, networkItem.Description, networkItem.VirtualCurrencyPrices["GD"].ToString(),
    //         networkItem.VirtualCurrencyPrices["GD"]));

    //         // item1.GetComponent<PrefabPanel>().displayname = NetWork.Get.itemList[i].DisplayName;
    //         // item1.GetComponent<PrefabPanel>().description = NetWork.Get.itemList[i].Description;
    //         // item1.GetComponent<PrefabPanel>().itemcost = NetWork.Get.itemList[i].VirtualCurrencyPrices["GD"].ToString();
    //         // item1.GetComponent<PrefabPanel>().price = (int)NetWork.Get.itemList[i].VirtualCurrencyPrices["GD"];
    //         // item1.GetComponent<PrefabPanel>().itemId = NetWork.Get.itemList[i].ItemId;
    //         //item1.GetComponent<PrefabPanel>().price = System.Convert.ToInt32(item1.GetComponent<PrefabPanel>().itemcost);
    //         // for(int j =0; j < GameManager.Get.imgArr.Length; j++)
    //         // {
    //         //     if(GameManager.Get.imgArr[j].name == item1.GetComponent<PrefabPanel>().displayname)
    //         //     {
    //         //         item1.GetComponent<PrefabPanel>().image.sprite = GameManager.Get.imgArr[j];
    //         //     }
    //         // }
    //     }
    // }
    //weapon 패널 생성하는 곳!
    // void WeaponPanel()
    // {
    //     for(int i =0 ; i <NetWork.Get.weaponList.Count; i++)
    //     {
    //         item1 = Instantiate(instantiatePrefab , new Vector3(0,0,0), Quaternion.identity, contents);
    //         item1.GetComponent<PrefabPanel>().displayname = NetWork.Get.weaponList[i].DisplayName;
    //         item1.GetComponent<PrefabPanel>().description = NetWork.Get.weaponList[i].Description;
    //         item1.GetComponent<PrefabPanel>().itemcost = NetWork.Get.weaponList[i].VirtualCurrencyPrices["GD"].ToString();
    //         item1.GetComponent<PrefabPanel>().price = (int)NetWork.Get.itemList[i].VirtualCurrencyPrices["GD"];
    //         item1.GetComponent<PrefabPanel>().itemId = NetWork.Get.weaponList[i].ItemId;
    //         //item1.GetComponent<PrefabPanel>().price = System.Convert.ToInt32(item1.GetComponent<PrefabPanel>().itemcost);
    //         for(int j =0; j < GameManager.Get.imgArr.Length; j++)
    //         {
    //             if(GameManager.Get.imgArr[j].name == item1.GetComponent<PrefabPanel>().displayname)
    //             {
    //                 item1.GetComponent<PrefabPanel>().image.sprite = GameManager.Get.imgArr[j];
    //             }
    //         }
    //     }
    // }
    //upperbody 패널 생성하는 곳!
    // void UpperBodyPanel()
    // {
    //     for(int i =0 ; i <NetWork.Get.bodyList.Count; i++)
    //     {
    //         item1 = Instantiate(instantiatePrefab , new Vector3(0,0,0), Quaternion.identity, contents);
    //         item1.GetComponent<PrefabPanel>().displayname = NetWork.Get.bodyList[i].DisplayName;
    //         item1.GetComponent<PrefabPanel>().description = NetWork.Get.bodyList[i].Description;
    //         item1.GetComponent<PrefabPanel>().itemcost = NetWork.Get.bodyList[i].VirtualCurrencyPrices["GD"].ToString();
    //         item1.GetComponent<PrefabPanel>().price = (int)NetWork.Get.itemList[i].VirtualCurrencyPrices["GD"];
    //         item1.GetComponent<PrefabPanel>().itemId = NetWork.Get.bodyList[i].ItemId;
    //         //item1.GetComponent<PrefabPanel>().price = System.Convert.ToInt32(item1.GetComponent<PrefabPanel>().itemcost);
    //         for(int j =0; j < GameManager.Get.imgArr.Length; j++)
    //         {
    //             if(GameManager.Get.imgArr[j].name == item1.GetComponent<PrefabPanel>().displayname)
    //             {
    //                 item1.GetComponent<PrefabPanel>().image.sprite = GameManager.Get.imgArr[j];
    //             }
    //         }
    //     }
    // }
    // //lowerdody 패널 생성하는 곳!
    // void LowerBodyPanel()
    // {
    //     for(int i =0 ; i <NetWork.Get.legList.Count; i++)
    //     {
    //         item1 = Instantiate(instantiatePrefab , new Vector3(0,0,0), Quaternion.identity, contents);
    //         item1.GetComponent<PrefabPanel>().displayname = NetWork.Get.legList[i].DisplayName;
    //         item1.GetComponent<PrefabPanel>().description = NetWork.Get.legList[i].Description;
    //         item1.GetComponent<PrefabPanel>().itemcost = NetWork.Get.legList[i].VirtualCurrencyPrices["GD"].ToString();
    //         item1.GetComponent<PrefabPanel>().price = (int)NetWork.Get.itemList[i].VirtualCurrencyPrices["GD"];
    //         item1.GetComponent<PrefabPanel>().itemId = NetWork.Get.legList[i].ItemId;
    //         //item1.GetComponent<PrefabPanel>().price = System.Convert.ToInt32(item1.GetComponent<PrefabPanel>().itemcost);
    //         for(int j =0; j < GameManager.Get.imgArr.Length; j++)
    //         {
    //             if(GameManager.Get.imgArr[j].name == item1.GetComponent<PrefabPanel>().displayname)
    //             {
    //                 item1.GetComponent<PrefabPanel>().image.sprite = GameManager.Get.imgArr[j];
    //             }
    //         }
    //     }
    // }
    //자식들 없애는 곳!
    protected void DestroyChildObj()
    {
        Debug.Log("자식 오브젝트 삭제 시작");
        for (int i =0; i < itemPrefabParentTransform.childCount; i++)
        {
            Destroy(itemPrefabParentTransform.GetChild(i).gameObject);
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
        DestroyChildObj();
        UpdateItemPanel(NetWork.Get.itemList);
        checkNumber = allNumber;
        checkImage(checkNumber);
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
        DestroyChildObj();
        UpdateItemPanel(NetWork.Get.weaponList);
        checkNumber= weaponNumber;
        checkImage(checkNumber);
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
        DestroyChildObj();
        UpdateItemPanel(NetWork.Get.bodyList);
        checkNumber = upperBodyNumber;
        checkImage(checkNumber);
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
        DestroyChildObj();
        UpdateItemPanel(NetWork.Get.legList);
        checkNumber = lowerBodyNumber;
        checkImage(checkNumber);
    }
}