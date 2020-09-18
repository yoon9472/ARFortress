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

public class ARCoreManager: MonoBehaviourPunCallbacks
{
    
    Camera ARCamera;
    public GameObject character;
    public GameObject centerObject;
    //public Text text;
    public bool firstAnchor = false;
    public bool timeCheck = false;
    public float time = 0;
    private void Start()
    {
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
    TrackableHit hit;
    Anchor anchor;
    AsyncTask<CloudAnchorResult> task;
    GameObject obj;
    public void MakeCloudAnchorMaster()
    {
        Touch touch = Input.GetTouch(0);
        TrackableHitFlags racastFilter = TrackableHitFlags.PlaneWithinPolygon; //평면만 검출
        if(firstAnchor == false && NetWork.Get.isMaster == true)//처음 앵커는 방장만 찍게 한다.
        {
            if (touch.phase == TouchPhase.Began && Frame.Raycast(touch.position.x, touch.position.y, racastFilter, out hit))
            {
                firstAnchor = true;
                timeCheck = true;
                anchor = hit.Trackable.CreateAnchor(hit.Pose);
                Debug.Log("방장이 생성한 앵커의 위치 = " + anchor.transform.position);
                //GameObject obj = PhotonNetwork.Instantiate("TestBot", hit.Pose.position, Quaternion.identity);
                obj = Instantiate(centerObject, anchor.transform.position, Quaternion.identity);
                obj.transform.SetParent(anchor.transform);
                Debug.Log("앵커에 자식이된 표시 오브젝트의 위치 = " + obj.transform.position);

            }
        }


    }
    public void ButtonCreateCloudAnchor()
    {
            if (NetWork.Get.isMaster == true)//방장이면 앵커 만들고 다른사람들한테 클라우드 앵커 아이디 넘겨주기
            {
                //AsyncTask<CloudAnchorResult> task = XPSession.CreateCloudAnchor(anchor);
                if (firstAnchor == true && time > 5)//방장은 첫앵커 생성하고 5초후에 버튼 누르면 호스팅 동작함
                {
                    StartCoroutine(HostCloudAnchor(anchor));
                }
            }
            else
            {
                if(NetWork.Get.receiveId==true) //앵커 아이디를 받았으면
                {
                Debug.Log(NetWork.Get.anchorId);
                StartCoroutine(ResolveCloudAnchor(NetWork.Get.anchorId));//코루틴 실행
                }
            }
        
    }
    IEnumerator HostCloudAnchor(Anchor anchor)
    {
        //anchor_ = AsyncTask < CloudAnchorResult > CreateCloudAnchor(anchor);
        task = XPSession.CreateCloudAnchor(anchor);
        //Debug.Log(anchor);
        //Debug.Log(task);
        Debug.Log("응답 머기중....");
        yield return new WaitUntil(() => task.IsComplete);
        Debug.Log(task.Result.Response); 
        Debug.Log(task.Result.Anchor);
        Debug.Log(task.Result.Anchor.CloudId);
        string id = task.Result.Anchor.CloudId;
        Debug.Log(id);
        Debug.Log("다른 사용자에게 앵커 아이디 보내는중...");
        NetWork.Get.SendAnchorId(id);
        InstantePlayer();
    }
    IEnumerator ResolveCloudAnchor(string id)
    {
        Debug.Log(id);
        task = XPSession.ResolveCloudAnchor(id);
        Debug.Log("응답 머기중....");
        yield return new WaitUntil(() => task.IsComplete);
        Debug.Log(task.Result.Response); //ErrorInternal
        Debug.Log(task.Result.Anchor); //Null
        Debug.Log(task.Result.Anchor.CloudId);//NullReferenceExceoption
        Debug.Log("전달받은 클라우드 앵커 위치 = " + task.Result.Anchor.transform.position);
        obj = Instantiate(centerObject, task.Result.Anchor.transform.position, Quaternion.identity);//앵커위치에 생성하고
        obj.transform.SetParent(task.Result.Anchor.transform);//부모로 설정
        InstantePlayer();
    }
    /// <summary>
    /// 앵커 기준으로 포톤 객체를 생성함(플레이할 객체)
    /// 앵커의 컴포넌트로 앵커 기준으로 생성될 위치가 잡혀있다.
    /// 클릭시 방장만 실행이 가능하게
    /// </summary>
    public void InstantePlayer()
    {
        Debug.Log("나의 입장 순서는 = "+NetWork.Get.myOrder);
        Debug.Log("position = " + obj.GetComponent<StartPos>().startPos[NetWork.Get.myOrder].transform.position);
        Debug.Log("localPosition = " + obj.GetComponent<StartPos>().startPos[NetWork.Get.myOrder].transform.localPosition);
        GameObject obj_ = obj.GetComponent<StartPos>().startPos[NetWork.Get.myOrder];
        Debug.Log(NetWork.Get.myOrder +"번 플레이어의 생성위치 = "+obj_.transform.position);
        PhotonNetwork.Instantiate("TestBot", obj.GetComponent<StartPos>().startPos[NetWork.Get.myOrder].transform.position, Quaternion.identity);
    }
}