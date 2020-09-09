using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine.SceneManagement;
using System;
#if GOOGLEGAMES
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif

public class NetWork : MonoBehaviourPunCallbacks
{

    private static NetWork m_Instance = null;
    public static NetWork Get { get { return m_Instance; } set { m_Instance = value; } }
    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else if (m_Instance != this)
        {
            Destroy(gameObject);
        }
        PhotonNetwork.AutomaticallySyncScene = true;
        DontDestroyOnLoad(gameObject);
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
    #region PhotonNetwork
    [Header("포톤 관련 변수")]
    public int[] actornums = new int[4];//방에 입장한 유저의 엑터 넘버를 저장할 배열
    public string[] userArr = new string[4]; //유저 이름을 저장하기위한 배열
    public bool[] userReady = new bool[4];//룸안에 유저가 학습이 됬는지 준비여부 체크
    public int readyCnt = 0;//방안에 몇명의 사용자가 클라우드 앵커를 생성 공유했는가 체크
    public int myOrder = 99;// 내 입장 순서 체크
    public bool inIntro = true;//시작은 인트로부터 니까 true로
    public bool inLogin = false; //로그인 창 상태
    public bool inLobby = false;//로비 입장  상태
    public bool inRoom = false;//룸 입장 상태
    public bool isMaster = false;//방장인지 확인
    public bool isMakeAnchor = false;//클라우드 앵커가 만들어 졌는지 확인
    public string anchorId;
    public bool receiveId = false;//클라우드 앵커 아이디를 받았는지 체크
    public int localPlayer;
    private void Start()
    {
        if (!PhotonNetwork.IsConnected && inIntro == true)//인트로 상태에서 시작하고 연결안되있으면->처음 시작이면
        {
            Connect();//연결하고
        }
    }
    private void Update()
    {
        if (inRoom == true)
        {
            if (PhotonNetwork.IsMasterClient == true)
            {
                isMaster = true;
            }
            else
            {
                isMaster = false;
            }
        }
        if (inRoom == true)
        {
            localPlayer = PhotonNetwork.CurrentRoom.PlayerCount;
        }
    }
    public void Connect()
    {
        Debug.Log("마스터 서버 접속시도중.....");
        //마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }
    /// <summary>
    /// 마스터 서버 연결되면 호출됨
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("마스터 서버 연결됨");
        inIntro = false;
        inLogin = true;
        inLobby = false;
        inRoom = false;
        Debug.Log("마스터 서버 연결상태 : " + inLogin);
        //로그인 화면으로 전환
        if (inLogin == true)
        {
            SceneManager.LoadScene("02.Login");
        }
    }
    /// <summary>
    /// 포톤 연결 끊겼을때
    /// </summary>
    /// <param name="cause"></param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("연결 끊김");
    }
    /// <summary>
    /// 호출하면 로비로
    /// </summary>
    public void JoinLobby()
    {
        Debug.Log("로비 입장시도");
        PhotonNetwork.JoinLobby();
    }
    /// <summary>
    /// 로비 입장 성공하면 호출됨
    /// </summary>
    public override void OnJoinedLobby()
    {
        Debug.Log("로비입장 성공");
        //룸에서 사용하는 변수들은 로비에 입장하면 전부 초기화 시켜준다 그래야 다시 룸에 들어갔을때 처음부터 사용할수 있다
        inIntro = false;
        inLogin = false;
        inLobby = true;
        inRoom = false;
        isMaster = false;
        receiveId = false;
        isMakeAnchor = false;
        anchorId = null;
        myOrder = 99;
        readyCnt = 0;
        localPlayer = 0;
        Debug.Log(inLobby);
        //룸에 입장해서 사용할 배열들과 값들은 로비로 입장했을때 초기화가 되게한다.
        for (int i = 0; i < userArr.Length; i++)
        {
            userArr[i] = "empty";
            actornums[i] = -1;
        }
        if (inLobby == true)
        {
            SceneManager.LoadScene("03.Lobby");
        }
        receiveId = false;
    }
    /// <summary>
    /// 방이름으로 방에 참가하기
    /// </summary>
    /// <param name="m_Roomname"></param>
    public void JoinRoom(string m_Roomname)
    {
        PhotonNetwork.JoinRoom(m_Roomname);
    }
    /// <summary>
    /// 방 이름 정하고 방생성하기
    /// </summary>
    /// <param name="m_Roomname"></param>
    public void CreateRoom(string m_Roomname)
    {
        //마스터 서버에 연결이 된상태에서만 방을 만들어야한다 연결상태인지 먼저 체크.
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("방 생성중...");
            //방옵션 세팅..
            RoomOptions roomOptions = new RoomOptions();
            //방을 리스트에 노출시킬지
            roomOptions.IsVisible = true;
            //공개방으로 설정
            roomOptions.IsOpen = true;
            //최대 입장인원
            roomOptions.MaxPlayers = 4;
            // 방 떠나면 방정보 날리기
            roomOptions.CleanupCacheOnLeave = true;
            //커스텀 프로퍼티 생성 엑터넘버를 저장할 배열
            roomOptions.CustomRoomProperties = new Hashtable() { { "roomMember", userArr }, { "actors", actornums }, { "userReady", userReady } };
            PhotonNetwork.CreateRoom(m_Roomname, roomOptions);

        }
        //마스터 서버에 접속이 안되있는경우 중간에 접속이 끝긴다든가 기타등등의 문제..
        else
        {
            Debug.Log("마스터 서버 연결 끝김 재접속중.....");
            //마스터 서버 접속 시도
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    /// <summary>
    /// 방입장에 실패했을때 호출됨.
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("방 입장 실패");
    }
    /// <summary>
    /// 방입장에 성공했을때 호출 방생성에 성공했을때도 호출
    /// </summary>
    public override void OnJoinedRoom()
    {
        Debug.Log("방 입장 OR 생성 성공");
        inIntro = false;
        inLogin = false;
        inLobby = false;
        inRoom = true;
        Debug.Log(inRoom);
        //룸에 입장 성공하면 자신의 엑터 넘버 저장
        myOrder = PhotonNetwork.LocalPlayer.ActorNumber;
        Hashtable cp = PhotonNetwork.CurrentRoom.CustomProperties; //현재 방의 커스텀 프로퍼티를 받아서 cp에 담음
        userArr = (string[])cp["roomMember"]; //룸의 커스텀 프로퍼티 roomMember를 useArr에 덮어씌운다
        actornums = (int[])cp["actors"];//룸의 커스텀 프로퍼티 actors를 배열 actornums에 덮어씌운다
        for (int i = 0; i < userArr.Length; i++)
        {
            //배열 크기만큼 반목문을 돌아서 배열에서 empty를찾고 내이름과 엑터 넘버 추가
            if (userArr[i] == "empty")
            {
                myOrder = i;
                userArr[i] = "testUser";//플레이어의 이름 Playfab에서 받아온 DisplayName을 넣을 예정
                actornums[i] = i;//나의 넘버 추가
                break;
            }
        }
        //배열이 수정이 되었으니까 수정된 배열을 덮어씌우라는 명령을  RPC함수를 통해 룸안에 모든 사용자가 실행하게함
        photonView.RPC("UpdateRoomCustomProperty", RpcTarget.All, userArr, actornums, userReady);
        if (inRoom == true)
        {
            SceneManager.LoadScene("04.Room");
        }
    }
    //룸에 입장해서 커스텀 프로퍼티 변경을 요청하는 함수
    [PunRPC]
    public void UpdateRoomCustomProperty(string[] Arr, int[] actorarr, bool[] userready)
    {
        //RPC로 업데이트된 커스텀 프로퍼티를 다른유저에게도 뿌린다
        Hashtable cp = PhotonNetwork.CurrentRoom.CustomProperties; //현재 방의 커스텀 프로퍼티를 받아서 CP에 담음
        cp["roomMember"] = Arr;//받은 매개변수로 현재 방의 커스텀 프로퍼티 수정
        cp["actors"] = actorarr;//받은 매개변수로 현재 방의 커스텀 프로퍼티 수정
        cp["userReady"] = userready;
        PhotonNetwork.CurrentRoom.SetCustomProperties(cp);
        userArr = Arr;//매개변수로 받은 배열을 덮어쓰기
        actornums = actorarr;//매개변수로 받은 배열을 덮어쓰기
        userReady = userready;//매개변수로 받은 배열을 덮어쓰기
    }

    /// <summary>
    /// 룸에서 나감
    /// </summary>
    public void LeaveRoom()
    {
        Debug.Log("룸에서 퇴장중..");
        //방에서 퇴장하기 전에 cp["roomMember"] 과 cp["actors"] 에서 내정보 제거한다.
        for (int i = 0; i < userArr.Length; i++)
        {
            if (userArr[i] == name)
            {
                userArr[i] = "empty";
                actornums[i] = -1;
            }
        }
        //변경된 배열을 방사용자에게 RPC로 변경하도록 명령하고
        photonView.RPC("UpdateRoomCustomProperty", RpcTarget.All, userArr, actornums);
        //퇴장
        PhotonNetwork.LeaveRoom();//방퇴장 ->로비로 돌아감
    }
    /// <summary>
    /// 방장이 시작을 누르면 다른 사용자와 함께 씬이동 autosynce를 사용할지 rpc로 사용할지 정해야함 아직 미정
    /// </summary>
    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("05.PlayArcore");
        }
    }
    /// <summary>
    /// string 앵커 아이디를 룸안에 다른 사용자에게 보내기위한 rpc 함수를 호출함
    /// </summary>
    /// <param name="anchorid"></param>
    public void SendAnchorId(string id)
    {
        photonView.RPC("RPC_SendAnchorId", RpcTarget.All, id);
        isMakeAnchor = true;//클라우드 앵커가 호스팅 됬다는거 체크
    }
    [PunRPC]
    public void RPC_SendAnchorId(string anchorid)
    {
        anchorId = anchorid;
        receiveId = true;//앵커 아이디 보낸거 확인
    }

    #endregion





    #region Playfab
    [Header("플레이팹 변수")]
    public string email;//입력받은 이메일값 이메일 형식으로 @.com으로 적어
    public string password;//입력받은 비밀번호
    public string userName;//입력받은 사용자 이름
    public string nickName;//입력받은 닉네임
    public Userinfo userinfo;
    public string loginMsg;
    string playfabid;


    /// <summary>
    /// 구글 로그인 하는 함수
    /// </summary>
    public void GoogleLogin()
    {
        print("GoogleLogin");
        Social.localUser.Authenticate((bool success) => {
            Debug.Log("Social.localUser.Authenticate : " +success);
            if (success)
            {
                loginMsg = "Google Signed In";
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
                    loginMsg = "Signed In as " + result.PlayFabId;//플레이팹 아이디를 받아옴 결과로 
                    Debug.Log(loginMsg);
                    playfabid = result.PlayFabId;
                    JoinLobby();

                }, PlayFab_GoogleLogin_Error);
            }
            else
            {
                Debug.Log("Social.localUser.Authenticate : " + success);
                loginMsg = "Google Failed to Authorize your login";
                Debug.Log(loginMsg);
            }

        });

    }

    private void PlayFab_GoogleLogin_Error(PlayFabError obj)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 회원가입
    /// </summary>
    public void Register()
    {
        var request = new PlayFab.ClientModels.RegisterPlayFabUserRequest { Email = email, Password = password, Username = userName, DisplayName = nickName };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegistFail);
    }

    private void OnRegistFail(PlayFabError obj)
    {
        Debug.Log("회원가입 실패");
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult obj)
    {
        Debug.Log("회원가입 성공");
        //회원가입성고앟면 생성된 플레이팹 고유의 아이디와 내가 입력한 이메일 패스워드 유저네임 닉네임을 클래스에 담기
        userinfo.playfabId = obj.PlayFabId;
        userinfo.email = email;
        userinfo.password = password;
        userinfo.userName = userName;
        userinfo.nickName = nickName;
        SetData(userinfo);//유저의 정보를 세팅하는 값 설정하기
    }
    /// <summary>
    /// 로그인 할떄 호출할것
    /// </summary>
    public void Login()
    {
        var request = new PlayFab.ClientModels.LoginWithEmailAddressRequest { Email = email, Password = password };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFail);
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
        //로그인 성공하면 앞으로 유저의 정보가 수정될수 있으니 유저의 정보를 불러와서 userinfo에 담아서 가지고 있는다
        Getdata(obj.PlayFabId);
    }

    /// <summary>
    /// 유저의 정보를 저장하기 변경사항이 있으면 Userinfo 클래스를 변경하고 매개변수로 변경된 클래스를 넣어서 호출하면
    /// 변경된 클래스가 저장이 된다
    /// </summary>
    /// <param name="info"></param>
    public void SetData(Userinfo info)
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
        var yourObject = JsonUtility.FromJson<Userinfo>(obj.Data["Info"].Value);
        userinfo = yourObject;
    }

    /// <summary>
    /// 인벤토리 정보 불러오기
    /// </summary>
    public void GetInventory()
    {
        PlayFabClientAPI.GetUserInventory(new PlayFab.ClientModels.GetUserInventoryRequest(), (result) =>
        {
            Debug.Log("인벤토리 불러오기 성공");
            //인벤토리 불러오기 성공하면 동작해야할것 작성....
            Debug.Log(result.VirtualCurrency);//가상화폐 종류별로  잔액불러오기(배열) 
        },
        (error) => Debug.Log("인벤토리 불러오기 실패"));
    }

/// <summary>
/// 상점 불러오기 상점의 내용은 Playfab 홈페이지에서 설정할수 있다(아이템 가격 중첩여부 기간 등..)
/// </summary>
/// <param name="storeName"></param>
    public void GetCatalogItem(string storeName)
    {
        //CatalogVersion 은 홈페이지에서 내가 만드 CatalogVersion 의 이름이다
        //CatalogVersion 은 string으로 언제는 변경될수 있음
        PlayFabClientAPI.GetCatalogItems(new PlayFab.ClientModels.GetCatalogItemsRequest() { CatalogVersion = storeName }, (result) =>
        {
            Debug.Log("상점 불러오기 성공");
            for(int i=0; i<result.Catalog.Count;i++)
            {
                var catalog = result.Catalog[i];
                Debug.Log("아이템 아이디 "+catalog.ItemId);
                Debug.Log("아이템 이름"+catalog.DisplayName);
                Debug.Log("아이템 설명"+catalog.Description);
                Debug.Log("가상화폐 가격"+catalog.VirtualCurrencyPrices);
            }
            //상점 불러오기 성공하면 동작해야할것 작성.....
        },
        (error) => Debug.Log("상점 불러오기 실패"));
    }

    /// <summary>
    /// 상점에서 아이텝 구입
    /// </summary>
    /// <param name="storeName"></param> <=스토어 이름
    /// <param name="itemid"></param> <=아이템이 고유의 아이디 playfab 홈페이지에서 앙템 설정해줄때 생성됨 받아올수 아이템 정보를 받아올때 가져 올수 있음
    /// <param name="virtualCurrency"></param><=게임내 화폐의 종류 playfab 홈페이지에서 화폐 설정가능하다 
    /// <param name="price"></param><= 100원? 200원? 얼마나 게임내 화페를 사용할지 
    public void BuyItem(string storeName,string itemid, string virtualCurrency,int price)
    {
        var request = new PurchaseItemRequest() { CatalogVersion = storeName, ItemId = itemid, VirtualCurrency = virtualCurrency, Price = price };
        PlayFabClientAPI.PurchaseItem(request, BuyOk, BuyFail);
    }

    private void BuyFail(PlayFabError obj)
    {
        Debug.Log("구입 실패");
    }

    private void BuyOk(PurchaseItemResult obj)
    {
        Debug.Log("구입 성공");
    }
    #endregion
}

/// <summary>
/// 사용자 정보
/// </summary>
public class Userinfo
{
    //기본적인 사용자의 정보 필요에 따라 나중에 더 추가될수도 있음
    public string email;
    public string password;
    public string playfabId;
    public string userName;
    public string nickName;
    public bool firstLogin = true;
}

