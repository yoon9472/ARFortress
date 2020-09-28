using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabPanel : MonoBehaviour
{
    public Text title;
    public Text introduction;
    public GameObject buttonText;
    Text cost;
    public GameObject buttonImage;
    public string displayname;
    public string description;
    public string itemcost;
    public string itemId;
    public int price;
    public Image image;
    bool canBuy = true;
    // Start is called before the first frame update
    void Start()
    {
        cost = buttonText.GetComponentInChildren<Text>();
        title.text = displayname;
        introduction.text = description;
        cost.text = itemcost;
        for(int i=0; i<GameManager.Get.ownedItem_List.Count;i++)
        {
            if(GameManager.Get.ownedItem_List[i] == displayname)
            {
                canBuy = false;
                cost.text = "Owned";
                buttonImage.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Buy()
    {
        //아이템이 구입되어 있는 상태면 바로 함수 종료하고 아래부분 미실행
        if(canBuy == false) return;
        Debug.Log("나 이거 살꺼다!");
        NetWork.Get.BuyItem("Store", itemId ,"GD", price);
        //아이템 구매 함수 호출하고 해당 슬롯의 구입 버튼을 막는다.
        canBuy = false; //false로 바꿔줌
        cost.text = "Owned";
        buttonImage.gameObject.SetActive(false);
        
    }
}
