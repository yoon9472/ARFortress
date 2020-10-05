﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PlayFab.GroupsModels;

public class Player : MonoBehaviourPunCallbacks
{
    public string legname;//다리 이름
    public string bodyname;//몸체 이름
    public string weaponname; //무기 이름
    public int actnum; //이객체가 누구의 소유인지 체크

    public int attack;//공격력
    public int hp;//체력
    public int amor;//방어력
    public int speed;//이동속도
    public int lange;//사거리

    private GameObject legObj;
    private GameObject bodyObj;
    private GameObject weaponObj;
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

    }

    
    void Update()
    {

    }
}
