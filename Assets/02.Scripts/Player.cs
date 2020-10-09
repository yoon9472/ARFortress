using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PlayFab.GroupsModels;

public class Player : MonoBehaviourPunCallbacks
{
    public GameObject isMine;
    public string legname;//다리 이름
    public string bodyname;//몸체 이름
    public string weaponname; //무기 이름
    public int actnum; //이객체가 누구의 소유인지 체크

    public int attack;//공격력
    public int hp;//체력
    public int amor;//방어력
    public float speed;//이동속도
    public int lange;//사거리

    private GameObject legObj;
    private GameObject bodyObj;
    private GameObject weaponObj;

    public Vector3 dir;
    void Start()
    {
        attack = GameManager.Get.weaponattack;
        hp = GameManager.Get.bodyhp;
        amor = GameManager.Get.legamor + GameManager.Get.bodyamor;
        speed = GameManager.Get.legspeed;
        lange = GameManager.Get.weaponlange;
        //다리 정보 찾아서 생성
        for(int i=0; i<GameManager.Get.legPartsArr.Length;i++)
        {
            if(GameManager.Get.legPartsArr[i].name==legname)
            {
                legObj = Instantiate(GameManager.Get.legPartsArr[i], transform.position, Quaternion.identity, transform);
            }
        }
        //몸체 정보 찾아서 생성
        for(int i=0;i<GameManager.Get.bodyPartsArr.Length;i++)
        {
            if(GameManager.Get.bodyPartsArr[i].name == bodyname)
            {
                bodyObj = Instantiate(GameManager.Get.bodyPartsArr[i], legObj.GetComponent<LegParts>().bodyPos.transform.position, Quaternion.identity, transform);
            }
        }
        //무기 정보 찾아서 생성
        for(int i=0; i<GameManager.Get.weaponPartsArr.Length;i++)
        {
            if(GameManager.Get.weaponPartsArr[i].name == weaponname)
            {
                weaponObj = Instantiate(GameManager.Get.weaponPartsArr[i], bodyObj.GetComponent<BodyParts>().weaponPos.transform.position, Quaternion.identity, transform);
            }
        }
        transform.forward = NetWork.Get.hostingResultList[0].Result.Anchor.transform.position - transform.position;
        if(actnum == NetWork.Get.myOrder)
        {
            isMine.SetActive(true);
        }
    }

    
    void Update()
    {
        //현재 사용자가 마스터 클라이언트고 현재턴과 로봇의 엑터넘버를 비교해서 일치하고 입력값이 들어왔을때만 움직인다
        if(NetWork.Get.nowTurn == actnum && NetWork.Get.isInput==true&&NetWork.Get.isMaster==true)
        {

            Debug.Log("nowTurn : " + NetWork.Get.nowTurn);
            dir.Set(NetWork.Get.x, 0, NetWork.Get.z);
            Debug.Log("움직임 적용중" + dir);
            Debug.Log("다리속도: " + speed);
            Debug.Log("인게임 움직임 속도: " + speed / 100);
            transform.localPosition += dir * (speed/100) * Time.deltaTime;
            Debug.Log("현재 위치: " + transform.position);
            
        }
    }

}
