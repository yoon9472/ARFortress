using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PlayFab.GroupsModels;

public class Player : MonoBehaviourPunCallbacks
{
    public string legname;
    public string bodyname;
    public string weaponname;
    public int actnum;

    private GameObject legObj;
    private GameObject bodyObj;
    private GameObject weaponObj;
    void Start()
    {
        for(int i=0; i<GameManager.Get.legPartsArr.Length;i++)
        {
            if(GameManager.Get.legPartsArr[i].name==legname)
            {
                legObj = Instantiate(GameManager.Get.legPartsArr[i], transform.position, Quaternion.identity, transform);
            }
        }
        for(int i=0;i<GameManager.Get.bodyPartsArr.Length;i++)
        {
            if(GameManager.Get.bodyPartsArr[i].name == bodyname)
            {
                bodyObj = Instantiate(GameManager.Get.bodyPartsArr[i], legObj.GetComponent<LegParts>().bodyPos.transform.position, Quaternion.identity, transform);
            }
        }
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
