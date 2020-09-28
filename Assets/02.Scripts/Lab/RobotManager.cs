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
        NetWork.Get.SetData(GameManager.Get.userinfo);
    }
    private void Start()
    {
        if(GameManager.Get.userinfo.selectedLeg !=null)
        {
            for (int i = 0; i < GameManager.Get.legPartsArr.Length; i++)
            {
                if (GameManager.Get.userinfo.selectedLeg == GameManager.Get.legPartsArr[i].name)
                {
                    GameManager.Get.selectLeg = Instantiate(GameManager.Get.legPartsArr[i], pivot.transform.position, Quaternion.identity);
                }
            }
            for (int i = 0; i < GameManager.Get.bodyPartsArr.Length; i++)
            {
                if (GameManager.Get.userinfo.selectedBody == GameManager.Get.bodyPartsArr[i].name)
                {
                    GameManager.Get.selectBody = Instantiate(GameManager.Get.bodyPartsArr[i],
                        GameManager.Get.selectLeg.GetComponent<LegParts>().bodyPos.position,
                        Quaternion.identity);
                }
            }
            for (int i = 0; i < GameManager.Get.weaponPartsArr.Length; i++)
            {
                if (GameManager.Get.userinfo.selectedWeapon == GameManager.Get.weaponPartsArr[i].name)
                {
                    GameManager.Get.selectWeapon = Instantiate(GameManager.Get.weaponPartsArr[i],
                        GameManager.Get.selectBody.GetComponent<BodyParts>().weaponPos.position,
                        Quaternion.identity);
                }
            }
        }

    }
}
