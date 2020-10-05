using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabPanel : MonoBehaviour
{
    public Text title;
    public Text introduction;
    public GameObject buttonText;
    public Text cost1;
    public GameObject buttonImage;
    public string displayname;
    public string description;
    public string itemcost;
    public string itemId;
    public int price;
    public Image image;
    public bool canBuy = true;

    public GameObject buy;
    GameObject blackCanvas;
    public GameObject noCoin;
    // Start is called before the first frame update
    void Start()
    {
        cost1 = buttonText.GetComponentInChildren<Text>();
        title.text = displayname;
        introduction.text = description;
        cost1.text = itemcost;
        for(int i=0; i<GameManager.Get.ownedItem_List.Count;i++)
        {
            if(GameManager.Get.ownedItem_List[i] == displayname)
            {
                canBuy = false;
                cost1.text = "Owned";
                buttonImage.gameObject.SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void Choice()
    {
        //아이템이 구입되어 있는 상태면 바로 함수 종료하고 아래부분 미실행
        if(canBuy == false) 
        {
            Debug.Log("이미 사신 아이템입니다.");
            return;
        }
        else if (NetWork.Get.myMoney < int.Parse(itemcost))
        {
            Debug.Log("돈이 부족합니다.");
            Instantiate(noCoin, new Vector3(0,0,0), Quaternion.identity);
            return;
        }
        Debug.Log("만들어지자!!!");
        //사는 창을 만들 때, 창에 정보도 넘겨준다.
        blackCanvas = Instantiate(buy, new Vector3(0,0,0), Quaternion.identity);
        blackCanvas.GetComponent<ForBuying>().itemId1 = itemId;
        blackCanvas.GetComponent<ForBuying>().price1 = price;
        blackCanvas.GetComponent<ForBuying>().panel = this.gameObject;
    }
    public void Close()
    {
        Destroy(blackCanvas, 0.1f);
    }
    
}