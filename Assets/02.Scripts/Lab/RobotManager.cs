using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour
{
    public GameObject pivot;
    /// <summary>
    ///  GameManager의 Userinfo형태를 저장한다
    /// </summary>
    public void ConfirmAssemble()
    {
        if(GameManager.Get.isloadOver == true)
        {
            GameManager.Get.Msg = "하중이 초과했습니다";
        }
        else if(GameManager.Get.istypeNoeSame == true)
        {
            GameManager.Get.Msg = "무기와 몸체의 타입이 일치하지 않습니다.";
        }
        else
        {
            NetWork.Get.SetData(GameManager.Get.userinfo);
            GameManager.Get.ChangeBeforePrefab();
            GameManager.Get.Msg = "저장되었습니다.";
        }
    }
    private void Start()
    {
        GameManager.Get.RecallBeforePrefab(); //이전 프리팹 정보를 가져오는것
        GameManager.Get.BeforeStats(); //이전프리팹 정보로 능력치를 찾아온다.
        GameManager.Get.Stats(); //능력치를 표시해준다

        if(GameManager.Get.legPrefab !=null)
        {
            //이전 프리팹 정보로 다리를 만들어서 생성시킨다.
            GameManager.Get.selectLeg = Instantiate(GameManager.Get.beforeLeg, pivot.transform.position,
                Quaternion.identity);
            //이전 몸통 프리팹을 생성한다.
            GameManager.Get.selectBody = Instantiate(GameManager.Get.beforeBody, 
                GameManager.Get.selectLeg.GetComponent<LegParts>().bodyPos.position,
               Quaternion.identity);
            //이전 무기 프리팹을 생성한다.
            GameManager.Get.selectWeapon = Instantiate(GameManager.Get.beforeWeapon,
                GameManager.Get.selectBody.GetComponent<BodyParts>().weaponPos.position,
                Quaternion.identity);
        }
    }
}
