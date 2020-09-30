using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PlayFab.GroupsModels;

public class Player : MonoBehaviourPunCallbacks
{
    private GameObject legPos;
    private GameObject bodyPos;
    private GameObject weaponPos;

    private Transform bodyRot;
    private Transform weaponRot;
    void Start()
    {
        if(photonView.IsMine)
        {
            Debug.Log("내 정보 불러오기");
            legPos = Instantiate(GameManager.Get.beforeLeg, transform.position, Quaternion.identity, transform);

            bodyRot = legPos.GetComponent<LegParts>().bodyPos;

            bodyPos = Instantiate(GameManager.Get.beforeBody,
                legPos.GetComponent<LegParts>().bodyPos.position,
                Quaternion.identity,
                transform);


            weaponRot = bodyPos.GetComponent<BodyParts>().weaponPos;

            weaponPos = Instantiate(GameManager.Get.beforeWeapon,
                bodyPos.GetComponent<BodyParts>().weaponPos.position,
                Quaternion.identity,
                transform);

            transform.forward = NetWork.Get.hostingResultList[0].Result.Anchor.transform.position - transform.position;
        }
        else //다른 클라이언트에 존재하는 나의 클론들한테도 같은 초기화 명령을 내려준다
        {
            legPos = Instantiate(GameManager.Get.beforeLeg, transform.position, Quaternion.identity, transform);

            bodyRot = legPos.GetComponent<LegParts>().bodyPos;

            bodyPos = Instantiate(GameManager.Get.beforeBody,
                legPos.GetComponent<LegParts>().bodyPos.position,
                Quaternion.identity,
                transform);


            weaponRot = bodyPos.GetComponent<BodyParts>().weaponPos;

            weaponPos = Instantiate(GameManager.Get.beforeWeapon,
                bodyPos.GetComponent<BodyParts>().weaponPos.position,
                Quaternion.identity,
                transform);

            transform.forward = NetWork.Get.hostingResultList[0].Result.Anchor.transform.position - transform.position;
        }

    }

    
    void Update()
    {
        if(photonView.IsMine)
        {
            //움직인다.
            //공격한다.
        }
    }
}
