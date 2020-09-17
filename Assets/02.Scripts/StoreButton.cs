using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class StoreButton : MonoBehaviour
{
    int allCount = 1;
    int weaponCount =1;
    int upperBodyCount =1;
    int lowerBodyCount =1;
    
    int whatItem = 1;

    //점점 산으로 가는 느낌이다.
    /*
    public bool allItem = false;
    public bool weaponItem = false;
    public bool upperBodyItem = false;
    public bool lowerBodyItem = false;
    */
    StoreInstantiate StIn;

    public GameObject noCoin;
    // Start is called before the first frame update
    void Start()
    {
        WhatItemWillBeShowed(1);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void BacktoMainScene()
    {
        SceneManager.LoadScene("03.Lobby");
        allCount =1;
        weaponCount =1;
        upperBodyCount =1;
        lowerBodyCount =1;
        whatItem =1;
    }
    //이 함수는 all/Weapon/UpperBody/LowerBody의 버튼을 눌렀을 때 보이는 아이템들을 변화할 수 있게 한다.
    //그렇다면 처음이면 상관 없지만 만약 전에 생성 된 것이 있었다면? 어떻게 해야 할까?
    public void WhatItemWillBeShowed(int k)
    {
        switch(k)
        {
            case 1:
                StIn.ShowAllItem(allCount);
            break;

            case 2:
                StIn.ShowWeaponItem(weaponCount);
            break;

            case 3:
                StIn.ShowUpperBodyItem(upperBodyCount);
            break;

            case 4:
                StIn.ShowLowerBodyItem(lowerBodyCount);
            break;
        }
    }
    //all 버튼을 눌렀을 때 모든 아이템들을 보일 수 있도록 한다.
    public void AllButton()
    {
        whatItem = 1;
        WhatItemWillBeShowed(whatItem);
    }
    //Weapon 버튼을 눌렀을 때 무기 아이템들을 보일 수 있도록 한다.
    public void WeaponItem()
    {
        whatItem = 2;
        WhatItemWillBeShowed(whatItem);
    }
    //UpperBody 버튼을 눌렀을 때 상체 아이템들을 보일 수 있도록 한다.
    public void UpperBodyItem()
    {
        whatItem = 3;
        WhatItemWillBeShowed(whatItem);
    }
    //LowerBody 버튼을 눌렀을 때 하체 아이템들을 보일 수 있도록 한다.
    public void LowerBodyItem()
    {
        whatItem = 4;
        WhatItemWillBeShowed(whatItem);
    }
    //이렇게 하는게 맞는지 의문이 든다. 아랫것은 그 아랫것으로 대체하기로 하자.
    /*public void AllItemNext()
    {
        if (allItem ==false) return;

        ++allCount;
        if(allCount>7)return;

        Debug.Log("allCount : "+ allCount);
        StIn.ShowAllItem(allCount);
    }
    public void AllItemBefore()
    {
        if (allItem ==false) return;

        --allCount;
        if(allCount<1)return;

        Debug.Log("allCount : "+ allCount);
        StIn.ShowAllItem(allCount);
    }
    //무기
    public void WeaponItemNext()
    {
        if (weaponItem ==false) return;

        ++weaponCount;
        if(weaponCount>3)return;

        Debug.Log("weaponCount : "+ weaponCount);
        StIn.ShowWeaponItem(weaponCount);
    }
    public void WeaponItemBefore()
    {
        if (weaponItem ==false) return;

        --weaponCount;
        if(weaponCount<1)return;

        Debug.Log("weaponCount : "+ weaponCount);
        StIn.ShowAllItem(weaponCount);
    }
    //상체
    public void UpperBodyItemNext()
    {
        if (upperBodyItem ==false) return;

        ++upperBodyCount;
        if(upperBodyCount>3)return;

        Debug.Log("upperBodyCount : "+ upperBodyCount);
        StIn.ShowWeaponItem(upperBodyCount);
    }
    public void UpperBodyItemBefore()
    {
        if (upperBodyItem ==false) return;

        --upperBodyCount;
        if(upperBodyCount<1)return;

        Debug.Log("upperBodyCount : "+ upperBodyCount);
        StIn.ShowAllItem(upperBodyCount);
    }
    //하체
    public void LowerBodyItemNext()
    {
        if (lowerBodyItem ==false) return;

        ++lowerBodyCount;
        if(lowerBodyCount>3)return;

        Debug.Log("lowerBodyCount : "+ lowerBodyCount);
        StIn.ShowWeaponItem(lowerBodyCount);
    }
    public void LowerBodyItemBefore()
    {
        if (lowerBodyItem ==false) return;

        --lowerBodyCount;
        if(lowerBodyCount<1)return;

        Debug.Log("lowerBodyCount : "+ lowerBodyCount);
        StIn.ShowAllItem(lowerBodyCount);
    }*/
    //오른쪽으로 넘어갈때!
    public void PlusClick()
    {
        switch(whatItem)
        {
            case 1:
                ++allCount;
                if(allCount>7)return;

                Debug.Log("allCount : "+ allCount);
                StIn.ShowAllItem(allCount);
            break;

            case 2:
                ++weaponCount;
                if(weaponCount>3)return;

                Debug.Log("weaponCount : "+ weaponCount);
                StIn.ShowWeaponItem(weaponCount);
            break;

            case 3:
                ++upperBodyCount;
                if(upperBodyCount>3)return;

                Debug.Log("upperBodyCount : "+ upperBodyCount);
                StIn.ShowUpperBodyItem(upperBodyCount);
            break;

            case 4:
                ++lowerBodyCount;
                if(lowerBodyCount>3)return;

                Debug.Log("lowerBodyCount : "+ lowerBodyCount);
                StIn.ShowLowerBodyItem(lowerBodyCount);
            break;
        }
    }
    //왼쪽으로 넘어갈때!
    public void MinusClick()
    {
        switch(whatItem)
        {
            case 1:
                --allCount;
                if(allCount<1)return;

                Debug.Log("allCount : "+ allCount);
                StIn.ShowAllItem(allCount);
            break;

            case 2:
                --weaponCount;
                if(weaponCount<1)return;

                Debug.Log("weaponCount : "+ weaponCount);
                StIn.ShowWeaponItem(weaponCount);
            break;

            case 3:
                --upperBodyCount;
                if(upperBodyCount<1)return;

                Debug.Log("upperBodyCount : "+ upperBodyCount);
                StIn.ShowUpperBodyItem(upperBodyCount);
            break;

            case 4:
                --lowerBodyCount;
                if(lowerBodyCount<1)return;

                Debug.Log("lowerBodyCount : "+ lowerBodyCount);
                StIn.ShowLowerBodyItem(lowerBodyCount);
            break;
        }
    }
    public void BuyItem()
    {
        /* 이것들은 예시이다!!! 맨 아래것 빼고 어디서 처리를 해야 하는 것일까?
        void GetVcStore() 이것은 store를 가져와서 사용자들에게 표시하는 것이다.
        {
            var primaryCatalogName = "TestCatalog-001"; // In your game, this should just be a constant matching your primary catalog
            var storeId = "Potion Store"; // In your game, this should be a constant for a permanent store, or retrieved from titleData for a time-sensitive store
            var request = new GetStoreItemsRequest
            {
                CatalogVersion = primaryCatalogName,
                StoreId = storeId
            };
            PlayFabClientAPI.GetStoreItems(request, LogSuccess, LogFailure);
        }

        void DefinePurchase() 이것은 물건을 살 때 하는 것 같은데.. 뭐지??
        {
            var primaryCatalogName = "TestCatalog-001"; // In your game, this should just be a constant matching your primary catalog
            var storeId = "Potion Store"; // At this point in the process, it's just maintaining the same storeId used above
            var request = new StartPurchaseRequest
            {
                CatalogVersion = primaryCatalogName,
                StoreId = storeId,
                Items = new List<ItemPurchaseRequest> {
                    // The presence of these lines are based on the results from GetStoreItems, and user selection - Yours will be more generic
                    new ItemPurchaseRequest { ItemId = "Small Health Potion", Quantity = 20,},
                    new ItemPurchaseRequest { ItemId = "Medium Health Potion", Quantity = 100,},
                    new ItemPurchaseRequest { ItemId = "Large Health Potion", Quantity = 2,},
                }
            };
            PlayFabClientAPI.StartPurchase(request, result => { Debug.Log("Purchase started: " + result.OrderId); }, LogFailure);
        }

        //아래 것은 물 건 살 때 통화에 대한 것 같다. 맞는 지는 모르겠지만.
        void DefinePaymentCurrency(string orderId, string currencyKey, string providerName)
        {
            var request =new PayForPurchaseRequest {
                OrderId = orderId, // orderId comes from StartPurchase above
                Currency = currencyKey, // User defines which currency they wish to use to pay for this purchase (all items must have a defined/non-zero cost in this currency)
                ProviderName = providerName // providerName comes from the PaymentOptions in the result from StartPurchase above.
            };
            PlayFabClientAPI.PayForPurchase(request, LogSuccess, LogFailure);
        }

        // Unity/C# 유니티에서 작업하는 것은 이것 뿐인가?? 맞나? 물건 살 때 마지막으로 하는 것인가?
        void FinishPurchase(string orderId)
        {
            var request = new ConfirmPurchaseRequest { OrderId = orderId };
            PlayFabClientAPI.ConfirmPurchase(request, LogSuccess, LogFailure);
        }
        */
        //만일 logFailure이 된다면
        //noCoin.gameObject.SetActive(true);
    }
    public void BacktoStore()
    {
        noCoin.gameObject.SetActive(false);
    }
}