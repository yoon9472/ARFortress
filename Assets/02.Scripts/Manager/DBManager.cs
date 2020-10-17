﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using Newtonsoft.Json;

/// <summary>
/// 플레이팹(DB)에 관련된 메소드들 데이터를 저장하고 받을때 사용할 메소드
/// </summary>
public class DBManager : MonoBehaviour
{
    protected static DBManager instance = null;
    protected DataManager dataManager;
    protected PhotonManager photonManager;
    private void Awake()
    {
        instance = this;
    }
    public static DBManager GetInstance()
    {
        if(instance == null)
        {
            GameObject obj = new GameObject();
            obj.name = "DBManager";
            instance = obj.AddComponent<DBManager>();
        }
        return instance;
    }
    private void Start()
    {
        //데이타매니저 가져와야함
        dataManager = DataManager.GetInstance();
        //포톤 매니저도 가져와야함
        photonManager = PhotonManager.GetInstance();
        
    }
    public string email;//입력받은 이메일값 이메일 형식으로 @.com으로 적어
    public string password;//입력받은 비밀번호
    public string userName;//입력받은 사용자 이름
    public string nickName;//입력받은 닉네임
    public string buylastItem;//상점에서 방금 구입한 아이템 이름
    public bool modifyOk = false;
    public bool modifyFail = false;
    public int buyItemPice;//방금 구입한 아이템의 가격
    public int myMoney;//현재 내가 가진돈
    public string displayName;//플레이어 이름
    public string playfabid;

    /// <summary>
    /// 유니티 에디터에서 로그인되도록 테스트용으로 만들어 놓음
    /// </summary>
    public void TestLogin()
    {
        var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFail);
    }
    /// <summary>
    /// 로그인 실패시 호출됨
    /// </summary>
    /// <param name="obj"></param>
    private void OnLoginFail(PlayFabError obj)
    {
        Debug.Log("로그인 실패");
    }
    /// <summary>
    /// 로그인 성공시 호출됨
    /// </summary>
    /// <param name="obj"></param>
    private void OnLoginSuccess(LoginResult obj)
    {
        Debug.Log("로그인 성공");
        StartCoroutine(LoadItemList());
        //로그인 성공하면 앞으로 유저의 정보가 수정될수 있으니 유저의 정보를 불러와서 userinfo에 담아서 가지고 있는다
        //JoinLobby();
        GetInventory(); //로그인 성공하고 유저의 인벤토리 정보 바로 호출
        Getdata(obj.PlayFabId);
    }

    /// <summary>
    /// 유저의 정보를 저장하기 변경사항이 있으면 Userinfo 클래스를 변경하고 매개변수로 변경된 클래스를 넣어서 호출하면
    /// 변경된 클래스가 저장이 된다
    /// </summary>
    /// <param name="info"></param>
    public void SetData(UserInfo info)
    {
        string json = JsonUtility.ToJson(info);
        var request = new PlayFab.ClientModels.UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                 { "Info", json },
            },
            Permission = PlayFab.ClientModels.UserDataPermission.Public //수정가능하게 퍼미션을 퍼블릭으로
        };
        PlayFabClientAPI.UpdateUserData(request, OnSetdateSuccess, OnSetdataFail);
    }

    private void OnSetdataFail(PlayFabError obj)
    {
        Debug.Log("데이터 저장 실패");
    }

    private void OnSetdateSuccess(PlayFab.ClientModels.UpdateUserDataResult obj)
    {
        Debug.Log("데이터 저장 성공");
    }

    /// <summary>
    /// 사용자의 정보 불러오기
    /// </summary>
    public void Getdata(string myId)
    {
        //myId 를 이용해서 플레이팹에서 나의 정보를 불러올수 있다.
        var request = new PlayFab.ClientModels.GetUserDataRequest() { PlayFabId = myId };
        PlayFabClientAPI.GetUserData(request, OnGetdataSuccess, OnGetdataFail);
    }
    private void OnGetdataFail(PlayFabError obj)
    {
        Debug.Log("가져오기 실패");
    }
    private void OnGetdataSuccess(PlayFab.ClientModels.GetUserDataResult obj)
    {
        Debug.Log("가져오기 성공");
        if (obj.Data.ContainsKey("Info"))
        {
            //가져올 정보가 있을때
            var yourObject = JsonUtility.FromJson<UserInfo>(obj.Data["Info"].Value);
            GameManager.Get.userinfo = yourObject;
            //선택된 부품 프리팹 가져오기
            GameManager.Get.SelecPrefabs();
        }
        else
        {
            SetData(GameManager.Get.userinfo);
            Debug.Log("유저 기본정보 세팅");
            //가져올 정보가 없을때 -> 기본 데이터 세팅한다 최초 로그인한 회원인 상태이다.

        }

        //키가 없다고 에러가 뜨면 -> 기본 정보 세팅

    }
    /// <summary>
    /// 가상화폐 얻기 한번 호출될때마다 100씩
    /// </summary>
    public void GetMoney(int amount)
    {
        PlayFabClientAPI.AddUserVirtualCurrency(new AddUserVirtualCurrencyRequest { Amount = amount, VirtualCurrency = "GD" }, (result) =>
        {
            Debug.Log("돈 추가");
            myMoney = result.Balance;//현재 돈 변경
        },
        (error) => Debug.Log("돈 얻기 실패"));
    }
    /// <summary>
    /// 가상화폐 감소시키기 매개변수로 감소 시키고 싶은 양
    /// </summary>
    /// <param name="amount"></param>
    public void SubMoney(int amount)
    {
        PlayFabClientAPI.SubtractUserVirtualCurrency(new SubtractUserVirtualCurrencyRequest { Amount = amount, VirtualCurrency = "GD" }, (result) =>
        {
            Debug.Log("돈 감소");
            myMoney = result.Balance;//현재 돈 변경
            if (myMoney < 0)
            {
                myMoney = 0;
            }
        },
        (error) => Debug.Log("돈 감소 실패"));
    }
    /// <summary>
    /// 인벤토리 정보 불러오기
    /// </summary>
    public void GetInventory()
    {
        PlayFabClientAPI.GetUserInventory(new PlayFab.ClientModels.GetUserInventoryRequest(), (result) =>
        {
            Debug.Log("인벤토리 불러오기 성공");
            if (result.VirtualCurrency["GD"] < 0)
            {
                myMoney = 0;
            }
            else
            {
                myMoney = result.VirtualCurrency["GD"]; //인벤토리 열면서 현재 돈 가져오기
            }
            //인벤토리 불러오기 성공하면 동작해야할것 작성....
            Debug.Log(result.VirtualCurrency);//가상화폐 종류별로  내가 가지고있는 잔액불러오기(배열) 
            for (int i = 0; i < result.Inventory.Count; i++)
            {
                //인벤토리 리스트에 있는 아이템들의 각정보들
                var inventory = result.Inventory[i];
                GameManager.Get.ownedItem_List.Add(result.Inventory[i].DisplayName);
                /*  Debug.Log(inventory.DisplayName);
                  Debug.Log(inventory.ItemId);
                  Debug.Log(inventory.ItemInstanceId);
                  Debug.Log(inventory.UnitCurrency);
                  Debug.Log(inventory.CustomData);
                  Debug.Log(inventory.PurchaseDate);*/
            }
        },
        (error) => Debug.Log("인벤토리 불러오기 실패"));
    }

    /// <summary>
    /// 상점 불러오기 상점의 내용은 Playfab 홈페이지에서 설정할수 있다(아이템 가격 중첩여부 기간 등..)
    /// itemList에 정체 리스트가 담긴다
    /// </summary>
    /// <param name="storeName"></param>
    public void GetCatalogItem(string storeName)
    {
        //CatalogVersion 은 홈페이지에서 내가 만드 CatalogVersion 의 이름이다
        //CatalogVersion 은 string으로 언제는 변경될수 있음
        PlayFabClientAPI.GetCatalogItems(new PlayFab.ClientModels.GetCatalogItemsRequest() { CatalogVersion = storeName }, (result) =>
        {
            Debug.Log("상점 불러오기 성공");

            dataManager.itemList = result.Catalog;
            //리스트 낮은 가격순으로 정렬
            //SortItemByPrice(itemList);
            for (int i = 0; i < result.Catalog.Count; i++)
            {

                var catalog = result.Catalog[i];
                //태그로 비교한다.
                if (catalog.Tags[0] == "Weapon")
                {
                    dataManager.weaponList.Add(catalog);//무기는 무기리스트에 추가
                    //아이템의 customData 불러오는 부분
                    //GetCustomIteminfo(catalog);
                }
                else if (catalog.Tags[0] == "LowerBody")
                {
                    dataManager.legList.Add(catalog);//다리는 다리 리스트에 추가
                }
                else
                {
                    dataManager.bodyList.Add(catalog);//그외에 것들은 몸통 리스트에 추가
                }
            }
            StartCoroutine("OrganizeItem_2");
        },
        (error) => Debug.Log("상점 불러오기 실패"));
    }
    /// <summary>
    /// 리스트에서 각 아이템의 상세 능력치 딕셔너리로 가져오기 + GameManager에 있는 아이템 상세정보리스트에 정보 넣어주기
    /// </summary>
    public void GetCustomIteminfo(CatalogItem item)
    {
        Debug.Log("아이템 세부 정보 받기");
        var iteminfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(item.CustomData);

        if (item.Tags[0] == "Weapon")
        {
            WeaponInfo info = new WeaponInfo();
            info.partName = item.DisplayName;//이름 넣기
            info.partType = item.Tags[0];//무기인지 몸인지 다리인지 널기
            info.weapontype = iteminfo["type"]; //무기가 어깨형인지 탑형인지 팔형인지
            info.attack = int.Parse(iteminfo["attack"]);
            info.weight = int.Parse(iteminfo["weight"]);
            info.lange = int.Parse(iteminfo["lange"]);
            GameManager.Get.weaponInfo_List.Add(info);
            Debug.Log("무기 정보 추가");
        }
        else if (item.Tags[0] == "LowerBody")
        {
            LegInfo info = new LegInfo();
            Debug.Log(item.DisplayName + "의 이동속도: " + iteminfo["speed"]);
            Debug.Log(item.DisplayName + "의 총 하중: " + iteminfo["totalweight"]);
            Debug.Log(item.DisplayName + "의 방어력: " + iteminfo["amor"]);
            info.partName = item.DisplayName;
            info.partType = item.Tags[0];
            info.totalweight = int.Parse(iteminfo["totalweight"]);
            info.speed = int.Parse(iteminfo["speed"]);
            info.amor = int.Parse(iteminfo["amor"]);
            GameManager.Get.legInfo_List.Add(info);
            Debug.Log("다리 정보 추가");

        }
        else
        {
            BodyInfo info = new BodyInfo();
            info.partName = item.DisplayName;//이름 넣기
            info.partType = item.Tags[0]; //무기인지 몸인지 다리인지 넣기
            info.bodytype = iteminfo["type"];//몸통이 어깨인지 탑형인지 팔형인지
            info.weight = int.Parse(iteminfo["weight"]);//무게
            info.hp = int.Parse(iteminfo["hp"]);//체력
            info.amor = int.Parse(iteminfo["amor"]);//방어력
            GameManager.Get.bodyInfo_List.Add(info);
            Debug.Log("몸통 정보 추가");
        }

    }
    /// <summary>
    /// 아이템 리스트를 받아와서 가격에 따라 낮은 가격부터 정렬하기
    /// </summary>
    /// <param name="list"></param>
    public void SortItemByPrice(List<CatalogItem> list)
    {
        //#if UNITY_EDITOR
        //        Debug.Log("---정렬전 리스트---");
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            Debug.Log(list[i].VirtualCurrencyPrices["GD"]);
        //        }
        //        Debug.Log("------");
        //#endif
        list.Sort(delegate (CatalogItem A, CatalogItem B)
        {

            if (A.VirtualCurrencyPrices["GD"] > B.VirtualCurrencyPrices["GD"]) return 1;
            else if (A.VirtualCurrencyPrices["GD"] < B.VirtualCurrencyPrices["GD"]) return -1;
            return 0;
        });
        //#if UNITY_EDITOR
        //        Debug.Log("---정렬후---");
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            Debug.Log(list[i].VirtualCurrencyPrices["GD"]);
        //        }
        //        Debug.Log("------");
        //#endif
    }
    /// <summary>
    /// 상점에서 아이텝 구입
    /// </summary>
    /// <param name="storeName"></param> <=스토어 이름
    /// <param name="itemid"></param> <=아이템이 고유의 아이디 playfab 홈페이지에서 앙템 설정해줄때 생성됨 받아올수 아이템 정보를 받아올때 가져 올수 있음
    /// <param name="virtualCurrency"></param><=게임내 화폐의 종류 playfab 홈페이지에서 화폐 설정가능하다 
    /// <param name="price"></param><= 100원? 200원? 얼마나 게임내 화페를 사용할지 
    public void BuyItem(string storeName, string itemid, string virtualCurrency, int price)
    {
        buylastItem = itemid;
        buyItemPice = price;
        var request = new PurchaseItemRequest() { CatalogVersion = storeName, ItemId = itemid, VirtualCurrency = virtualCurrency, Price = price };
        PlayFabClientAPI.PurchaseItem(request, BuyOk, BuyFail);
    }

    private void BuyFail(PlayFabError obj)
    {
        Debug.Log("구입 실패");
    }
    //델리게이트 
    public delegate void ChangeMoneyDelegate(int totalMoney);
    public ChangeMoneyDelegate onChangeMoneyDelegate;
    public int GetMyMoney()
    {
        return myMoney;
    }

    private void BuyOk(PurchaseItemResult obj)
    {
        Debug.Log("구입 성공");
        //GameManager의 보유 아이템 리스트(스트링타입)에 방금 구입한 아이템의 이름을 넣어준다
        GameManager.Get.ownedItem_List.Add(buylastItem);
        myMoney -= buyItemPice;//방금 구입한 아이템의 가격만큼 현재 가진돈을 빼준다
        if (onChangeMoneyDelegate != null) onChangeMoneyDelegate(myMoney);
    }
    /// <summary>
    /// 플레이어 디스플레이 네임 수정하기 첫로그인 시에만 수정창을 한번띄워주게한다
    /// </summary>
    /// <param name="nickname"></param>
    public void ModifyDisplayName(string nickname)
    {
        displayName = nickname;
        var request = new UpdateUserTitleDisplayNameRequest() { DisplayName = nickname };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, UpdatenameSuccess, GetprofileFail);
    }

    private void GetprofileFail(PlayFabError obj)
    {
        Debug.Log("닉네임 변경 실패");
        modifyFail = true;
    }

    private void UpdatenameSuccess(UpdateUserTitleDisplayNameResult obj)
    {
        Debug.Log("닉네임 변경 성공");
        GameManager.Get.userinfo.nickName = obj.DisplayName;
        GameManager.Get.userinfo.firstLogin = false;
        SetData(GameManager.Get.userinfo);
        Debug.Log("변경된 이름은 ; " + obj.DisplayName);
        modifyOk = true;

    }


    #region 상점아이템 받아오기 -> 아이템 가격순 정렬->아이템별 상세 정보 담아서 리스트 만들기
    public IEnumerator LoadItemList()
    {
        Debug.Log("LoadItemList");
        yield return StartCoroutine("OrganizeItem_1");
    }
    /// <summary>
    /// 상점 목록을 불러와 태그별로 구분해서 3개의 리스트에 나눠서 담는다.
    /// </summary>
    /// <returns></returns>
    public IEnumerator OrganizeItem_1()
    {
        yield return null;
        Debug.Log("OrganizeItem_1");
        GetCatalogItem("Store");

    }
    /// <summary>
    /// 부위별로 정리된 리스트 3개를 가격에 따라 낮은순서대로 정렬하기
    /// </summary>
    /// <returns></returns>
    public IEnumerator OrganizeItem_2()
    {
        yield return null;
        Debug.Log("OrganizeItem_2");
        //List<CatalogItem> list 형태 리스트가 매개변수로 사용됨
        SortItemByPrice(dataManager.itemList); //전체아이템 리스트 가격 낮은 순으로 정렬
        SortItemByPrice(dataManager.weaponList);//무기리스트 가격낮은 순으로 정렬
        SortItemByPrice(dataManager.bodyList);//몸통 리스트 가격낮은 순으로 정렬
        SortItemByPrice(dataManager.legList);//다리 리스트 가격낮은 순으로 정렬

        StartCoroutine("OrganizeItem_3");
    }
    /// <summary>
    /// 각 아이템별 상세 정보를 담는다.
    /// GameManaget 스크립트의 weaponInfo_List ,bodyInfo_List, legInfo_Listdp 에 정보를 담기 위함
    /// </summary>
    /// <returns></returns>
    public IEnumerator OrganizeItem_3()
    {
        yield return null;
        Debug.Log("OrganizeItem_3");
        //GetCustomIteminfo 함수는 리스트에 있는 아이템들의 이름 타입 커스텀 데이터를 받아와서
        //GameManager에 있는 무기.몸,다리 상세정보리스트에 정보를 담는다.
        for (int i = 0; i < dataManager.weaponList.Count; i++)
        {
            GetCustomIteminfo(dataManager.weaponList[i]);
        }
        for (int i = 0; i < dataManager.bodyList.Count; i++)
        {
            GetCustomIteminfo(dataManager.bodyList[i]);
        }
        for (int i = 0; i < dataManager.legList.Count; i++)
        {
            GetCustomIteminfo(dataManager.legList[i]);
        }
        yield return new WaitUntil(() => GameManager.Get.legInfo_List.Count == 7);
        //무기 정보 리스트가 차면->기존에 조립된 것이 있다면 바로 정보를 찾아서 불러온다
        GameManager.Get.BeforeStats();
        StartCoroutine("MoveToLobby");
    }
    public IEnumerator MoveToLobby()
    {
        yield return null;
        photonManager.JoinLobby();
    }

    #endregion
}
