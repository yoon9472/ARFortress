using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.GroupsModels;
using PlayFab.ClientModels;
using PlayFab;
using TMPro.EditorUtilities;
using System;
#if GOOGLEGAMES
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif
public class LoginManager : MonoBehaviour
{
    protected static LoginManager instance =null;
    protected DBManager dbManager;
    private void Awake()
    {
        instance = this;
#if GOOGLEGAMES
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
    .AddOauthScope("profile")
    .RequestServerAuthCode(false)
    .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
#endif
    }
    public static LoginManager GetInstance()
    {
        if(instance == null)
        {
            GameObject obj = new GameObject(); //새로운 오브젝트
            obj.name = "LoginManager"; //오브젝트 이름 바꿔준다
           instance = obj.AddComponent<LoginManager>();//오브젝트에 스크립트를 붙이고 인스턴스에 넣는다
        }
        return instance;
    }
    private void Start()
    {
        //디비 메니져를 가져와야한다
        dbManager = DBManager.GetInstance();
    }
    public void GoogleLogin()
    {
        print("GoogleLogin");
        Social.localUser.Authenticate((bool success, string msg) => {
            Debug.Log("Social.localUser.Authenticate : " + success);
            if (success)
            {
                var serverAuthCode = PlayGamesPlatform.Instance.GetServerAuthCode();
                Debug.Log("Server Auth Code: " + serverAuthCode);

                PlayFabClientAPI.LoginWithGoogleAccount(new LoginWithGoogleAccountRequest()
                {
                    TitleId = PlayFabSettings.TitleId,
                    ServerAuthCode = serverAuthCode,
                    CreateAccount = true
                }, (result) =>
                {
                    //로그인 성공시 콜백 부분
                    dbManager.playfabid = result.PlayFabId;
                    Debug.Log("로그인 성공");
                    dbManager.GetInventory(); //로그인 성공하고 유저의 인벤토리 정보 바로 호출
                    dbManager.Getdata(dbManager.playfabid);//타이틀데이터 불러오기->타이틀데이터가 없으면 새로 생성해서 기본값 넣어주기
                    StartCoroutine(dbManager.LoadItemList());// 상점 리스트 불러와서 정리하고 완료되면 씬전환
                    //JoinLobby();

                    if (dbManager.onChangeMoneyDelegate != null) dbManager.onChangeMoneyDelegate(dbManager.myMoney);

                }, PlayFab_GoogleLogin_Error);
            }
            else
            {
                Debug.Log("Social.localUser.Authenticate : " + success);
                Debug.Log(msg);
            }

        });

    }

    private void PlayFab_GoogleLogin_Error(PlayFabError obj)
    {
        throw new NotImplementedException();
    }
}
