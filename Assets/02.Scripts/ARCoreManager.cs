using Unity.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;
using Photon.Pun;
using Photon.Realtime;
using GoogleARCore.Examples.CloudAnchors;
using GoogleARCore.CrossPlatform;
using System.Collections;
using System;
using System.Threading.Tasks;

public class ARCoreManager: MonoBehaviourPunCallbacks
{
    
    Camera ARCamera;
    public GameObject character;
    //public GameObject centerObject;
    //public Text text;
    public bool firstAnchor = false;
    public bool timeCheck = false;
    public float time = 0;
    private TrackableHit hit;
    public List<Anchor> localAnchor = new List<Anchor>(); //클라우드 앵커로 만들 로컬앵커들의 리스트를 담아둠
    private AsyncTask<CloudAnchorResult> task;
    private GameObject obj;
    private Pose pos; //방장이 처음 터치한 위치
    public float DistanceFromCenter; //중심점으로 부터의 거리

    private void Start()
    {
        DistanceFromCenter = 0.5f;
        ARCamera = GameObject.Find("First Person Camera").GetComponent<Camera>();

    }

    private void Update()
    {
        if(timeCheck==true)
        {
            time += Time.deltaTime;
        }
        if(Input.touchCount==0)
        {
            return;
        }

        MakeCloudAnchorMaster();


    }
    /// <summary>
    /// 방장이 먼저 앵커를 만든다.
    /// 앵커를 호스팅한다.
    /// 다음 사람은 리솔브한다?
    /// </summary>

    public void MakeCloudAnchorMaster()
    {
        Touch touch = Input.GetTouch(0);
        TrackableHitFlags racastFilter = TrackableHitFlags.PlaneWithinPolygon; //평면만 검출
        if(firstAnchor == false && NetWork.Get.isMaster == true)//처음 앵커는 방장만 찍게 한다.
        {
            if (touch.phase == TouchPhase.Began && Frame.Raycast(touch.position.x, touch.position.y, racastFilter, out hit))
            {

                firstAnchor = true;//값을 true 로 해서 이함수가 2번 실행되지 않게 한다?
                timeCheck = true;
                pos = hit.Pose;
                //anchor = hit.Trackable.CreateAnchor(hit.Pose);
                localAnchor.Add(hit.Trackable.CreateAnchor(pos));
                Debug.Log("중심점 설정");
                //유저수 +1 만큼 앵커를 만들고 싶음
               
                for(int i=0; i<NetWork.Get.localPlayer;i++)
                {
                    if (i == 0) //1번 사용자 (방장)의 위치
                    {
                        pos.position += new Vector3(DistanceFromCenter, 0, 0);
                        localAnchor.Add(Session.CreateAnchor(pos));
                        Debug.Log("1번 플레이어 위치 설정");
                    }
                    else if(i ==1) //2번 사용자의 위치
                    {
                        pos.position += new Vector3(-DistanceFromCenter, 0, 0);
                        localAnchor.Add(Session.CreateAnchor(pos));
                        Debug.Log("2번 플레이어 위치 설정");
                    }
                    else if(i==2)//3번 사용자의 위치
                    {
                        pos.position += new Vector3(0, 0, DistanceFromCenter);
                        localAnchor.Add(Session.CreateAnchor(pos));
                        Debug.Log("3번 플레이어 위치 설정");
                    }
                    else//4번 사용자의 위치
                    {
                        pos.position += new Vector3(0, 0, -DistanceFromCenter);
                        localAnchor.Add(Session.CreateAnchor(pos));
                        Debug.Log("4번 플레이어 위치 설정");
                    }
                }
                //Debug.Log("방장이 생성한 앵커의 위치 = " + anchor.transform.position);
                //GameObject obj = PhotonNetwork.Instantiate("TestBot", hit.Pose.position, Quaternion.identity);
                //obj = Instantiate(centerObject, anchor.transform.position, Quaternion.identity);
                //obj.transform.SetParent(anchor.transform);
                //Debug.Log("앵커에 자식이된 표시 오브젝트의 위치 = " + obj.transform.position);

            }
        }


    }
    public void ButtonCreateCloudAnchor()
    {
            if (NetWork.Get.isMaster == true)//방장이면 앵커 만들고 다른사람들한테 클라우드 앵커 아이디 넘겨주기
            {
                //AsyncTask<CloudAnchorResult> task = XPSession.CreateCloudAnchor(anchor);
                if (firstAnchor == true && time > 3.0f)//방장은 첫앵커 생성하고 3초후에 버튼 누르면 호스팅 동작함
                {
                    StartCoroutine(HostCloudAnchor(localAnchor));
                }
            }
            else
            {
                if(NetWork.Get.receiveId==true) //앵커 아이디를 받았으면
                {
                //Debug.Log(NetWork.Get.anchorId);
                //StartCoroutine(ResolveCloudAnchor(NetWork.Get.anchorId));//코루틴 실행
                StartCoroutine(ResolveCloudAnchor(NetWork.Get.anchorIdList));//코루틴 실행
                }
            }
        
    }
    /// <summary>
    /// 방장이 생성된 앵커리스트를 보내서 호스팅
    /// </summary>
    /// <param name="anchorlist"></param>
    /// <returns></returns>
    IEnumerator HostCloudAnchor(List<Anchor> anchorlist)
    {
        //anchor_ = AsyncTask < CloudAnchorResult > CreateCloudAnchor(anchor);
        //task = XPSession.CreateCloudAnchor(anchor);
        //Debug.Log(anchor);
        //Debug.Log(task);
        Debug.Log("응답 머기중....");
        for(int i=0; i<anchorlist.Count;i++)
        {
            task = XPSession.CreateCloudAnchor(anchorlist[i]);
            yield return new WaitUntil(() => task.IsComplete);
            NetWork.Get.anchorIdList.Add(task.Result.Anchor.CloudId);//완료된 클라우드 앵커의 아이디를 리스트에 담는다.
            NetWork.Get.hostingResultList.Add(task);//task를 담는다.
            Debug.Log(task.Result.Response);
            Debug.Log(task.Result.Anchor);
            Debug.Log(task.Result.Anchor.CloudId);
        }
        //string id = task.Result.Anchor.CloudId;
        //Debug.Log(id);
        Debug.Log("다른 사용자에게 앵커 아이디리스트 보내는중...");
        NetWork.Get.SendAnchorId(NetWork.Get.anchorIdList);
        //플레이어를 해당위치에 생성
        //hostingResultList 매개변수로
        InstantePlayer(NetWork.Get.hostingResultList);
    }
    /// <summary>
    /// 매개변수로 앵커 아이디 리스트를 받는다. 리솔브 한다.
    /// 방장아닌 참가자가 해야할것
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    IEnumerator ResolveCloudAnchor(List<string> ids)
    {
        //Debug.Log(id);
        //task = XPSession.ResolveCloudAnchor(id);
        Debug.Log("응답 머기중....");
        for (int i= 0; i < ids.Count;i++)
        {
            task = XPSession.ResolveCloudAnchor(ids[i]);
            yield return new WaitUntil(() => task.IsComplete);
            NetWork.Get.hostingResultList.Add(task);//결과를 담는다
            Debug.Log(task.Result.Response); 
            Debug.Log(task.Result.Anchor); 
            Debug.Log(task.Result.Anchor.CloudId);//NullReferenceExceoption
            Debug.Log("전달받은 클라우드 앵커 위치 = " + task.Result.Anchor.transform.position);
        }
        //yield return new WaitUntil(() => task.IsComplete);
        //obj = Instantiate(centerObject, task.Result.Anchor.transform.position, Quaternion.identity);//앵커위치에 생성하고
        //obj.transform.SetParent(task.Result.Anchor.transform);//부모로 설정
        InstantePlayer(NetWork.Get.hostingResultList);
    }
    /// <summary>
    /// 앵커 기준으로 포톤 객체를 생성함(플레이할 객체)
    /// 리스트에 0번은 가운데 기준점이다. 리스트 0번위치로 모든 오브젝트가 회전에서 돌아보게할것임
    /// 0번 유저는 리스트1번앵커위치에 1번 유저는 리스트2번앵커위치에 ...
    /// </summary>
    public void InstantePlayer(List<AsyncTask<CloudAnchorResult>> tasklist)
    {
        Debug.Log("나의 입장 순서는 = "+NetWork.Get.myOrder);
        //Debug.Log("position = " + obj.GetComponent<StartPos>().startPos[NetWork.Get.myOrder].transform.position);
        //Debug.Log("localPosition = " + obj.GetComponent<StartPos>().startPos[NetWork.Get.myOrder].transform.localPosition);
        if(NetWork.Get.myOrder ==0)
        {
            obj = PhotonNetwork.Instantiate("TestBot", tasklist[1].Result.Anchor.transform.position, Quaternion.identity);
            obj.transform.forward = tasklist[0].Result.Anchor.transform.position-obj.transform.position;//생성하고 0번앵커의 위치로 바라보게한다
        }
        else if(NetWork.Get.myOrder == 1)
        {
            obj = PhotonNetwork.Instantiate("TestBot", tasklist[2].Result.Anchor.transform.position, Quaternion.identity);
            obj.transform.forward = tasklist[0].Result.Anchor.transform.position - obj.transform.position;
        }
        else if(NetWork.Get.myOrder == 2)
        {
            obj = PhotonNetwork.Instantiate("TestBot", tasklist[3].Result.Anchor.transform.position, Quaternion.identity);
            obj.transform.forward = tasklist[0].Result.Anchor.transform.position - obj.transform.position;
        }
        else
        {
            obj = PhotonNetwork.Instantiate("TestBot", tasklist[4].Result.Anchor.transform.position, Quaternion.identity);
            obj.transform.forward = tasklist[0].Result.Anchor.transform.position - obj.transform.position;
        }
        //GameObject obj_ = obj.GetComponent<StartPos>().startPos[NetWork.Get.myOrder];
        //Debug.Log(NetWork.Get.myOrder +"번 플레이어의 생성위치 = "+obj_.transform.position);
        //PhotonNetwork.Instantiate("TestBot", obj.GetComponent<StartPos>().startPos[NetWork.Get.myOrder].transform.position, Quaternion.identity);
    }
}