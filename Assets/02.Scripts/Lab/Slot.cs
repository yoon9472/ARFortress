using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    //이 스크립트는 Panel_Leg,Panel_Weapon,Panel_Body 의 PartsSlot 안의 Slot 안에 각각 붙어있음.
    //슬롯 테두리안의 로봇이미지 img
    public Image img;
    //선택시 큰화면으로 들고갈 이미지 저장 변수 
    public Image selectImage;
    //select 이미지 안에있는 각각 Spider , Ammogun 등 파츠별 실제 이름= DB에서 불러오기 위해 이름을 정확하게써야됨.
    public string itemName;
    //0 = 무기 1 = 바디  2 = 다리
    public int itemType;
    //텍스트를 정렬하기 위해 그룹으로 만듦. 텍스트루트에 GetChild(0~5) 하여 하위 항목에 하나씩 아이템 설명을 받아오면됨. 
    public GameObject textRoot;
    //WeaponList 의 카운트만큼 for문 돌려서 string네임과 같은걸찾음
    //잠금화면 이미지
    public Image lockitem;
    public bool activeSlot;
    void Start()
    {
        activeSlot = false;
        //인벤토리에 아이템이 들어와 있으면 아이템의 버튼 활성화 ( Default 상태의 잠금 해제)
        for (int i = 0; i < GameManager.Get.ownedItem_List.Count; i++)
        {
            if (itemName == GameManager.Get.ownedItem_List[i])
            {
                //버튼 활성화 시키기  활성화 되면 잠금이미지의 알파값 을 0으로 (투명하게)
                activeSlot = true;
                Color color = lockitem.color;
                color.a = 0;
                lockitem.color = color;
                //비활성화 일때 노말칼라가 빨강색인데 활성화되면 흰색으로 변경
                img.color = new Color(255, 255, 255);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetSlotInfo()
    {
        //액티브 상태일때
        if (activeSlot == true)
        {
            selectImage.sprite = null;
            selectImage.sprite = img.sprite;
            //무기 리스트일때
            if (itemType == 0)
            {
                for (int i = 0; i < GameManager.Get.weaponInfo_List.Count; i++)
                {
                    if (itemName == GameManager.Get.weaponInfo_List[i].partName)
                    {
                        textRoot.transform.GetChild(0).GetComponent<Text>().text = "<color=#99ff99>Name : </color>" + GameManager.Get.weaponInfo_List[i].partName;
                        textRoot.transform.GetChild(1).GetComponent<Text>().text = "<color=#80ff80>Type : </color>" + GameManager.Get.weaponInfo_List[i].weapontype;
                        textRoot.transform.GetChild(2).GetComponent<Text>().text = "<color=#66ff66>Weight : </color>" + GameManager.Get.weaponInfo_List[i].weight.ToString();
                        textRoot.transform.GetChild(3).GetComponent<Text>().text = "<color=#4dff4d>Attack : </color>" + GameManager.Get.weaponInfo_List[i].attack.ToString();
                        textRoot.transform.GetChild(4).GetComponent<Text>().text = "<color=#33ff33>Range : </color>" + GameManager.Get.weaponInfo_List[i].lange.ToString();
                    }


                }
            }
            //바디 일때
            else if (itemType == 1)
            {
                for (int i = 0; i < GameManager.Get.bodyInfo_List.Count; i++)
                {
                    if (itemName == GameManager.Get.bodyInfo_List[i].partName)
                    {
                        textRoot.transform.GetChild(0).GetComponent<Text>().text = "<color=#99ff99>Name : </color>" + GameManager.Get.bodyInfo_List[i].partName;
                        textRoot.transform.GetChild(1).GetComponent<Text>().text = "<color=#80ff80>Type : </color>" + GameManager.Get.bodyInfo_List[i].bodytype;
                        textRoot.transform.GetChild(2).GetComponent<Text>().text = "<color=#66ff66>Weight : </color>" + GameManager.Get.bodyInfo_List[i].weight.ToString();
                        textRoot.transform.GetChild(3).GetComponent<Text>().text = "<color=#4dff4d>HP : </color>" + GameManager.Get.bodyInfo_List[i].hp.ToString();
                        textRoot.transform.GetChild(4).GetComponent<Text>().text = "<color=#33ff33>Armor : </color>" + GameManager.Get.bodyInfo_List[i].amor.ToString();
                    }
                }
            }
            //다리 일때
            else
            {
                for (int i = 0; i < GameManager.Get.legInfo_List.Count; i++)
                {
                    if (itemName == GameManager.Get.legInfo_List[i].partName)
                    {
                        textRoot.transform.GetChild(0).GetComponent<Text>().text = "<color=#99ff99>Name : </color>" + GameManager.Get.legInfo_List[i].partName;
                        textRoot.transform.GetChild(1).GetComponent<Text>().text = "<color=#80ff80>Load : </color>" + GameManager.Get.legInfo_List[i].totalweight.ToString();
                        textRoot.transform.GetChild(2).GetComponent<Text>().text = "<color=#66ff66>Speed : </color>" + GameManager.Get.legInfo_List[i].speed.ToString();
                        textRoot.transform.GetChild(3).GetComponent<Text>().text = "<color=#4dff4d>Armor : </color>" + GameManager.Get.legInfo_List[i].amor.ToString();
                    }
                }
            }

        }
        //string itemName으로 입력한 이름이   게임매니저의 아이템List와 일치하면 아이템 생성
        //if (this.itemName == GameManager.Get.legInfo_List[].partName)
       //
    }
}
