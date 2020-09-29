using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviourPunCallbacks
{
    private GameObject legPos;
    private GameObject bodyPos;
    private GameObject weaponPos;

    private Transform bodyRot;
    private Transform weaponRot;
    void Start()
    {
        legPos = Instantiate(GameManager.Get.beforeLeg, transform.position, Quaternion.identity, transform);

        bodyRot = legPos.GetComponent<LegParts>().bodyPos;

        bodyPos = Instantiate(GameManager.Get.beforeBody,
            legPos.GetComponent<LegParts>().bodyPos.position,
            Quaternion.identity,
            legPos.GetComponent<LegParts>().transform);

        weaponRot = bodyPos.GetComponent<BodyParts>().weaponPos;

        weaponPos = Instantiate(GameManager.Get.beforeWeapon, bodyPos.GetComponent<BodyParts>().weaponPos.position,
            Quaternion.identity,
            bodyPos.GetComponent<BodyParts>().transform);

        transform.forward = NetWork.Get.hostingResultList[0].Result.Anchor.transform.position - transform.position;
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
