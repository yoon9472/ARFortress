using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    public List<GameObject> inventory = new List<GameObject>();

    public GameObject currItem;

    //시작시 레그 인벤토리가 켜져있으므로 Leg 메뉴를 Selected 상태로 시작. 
    public bool buttonDown; 
    void Start()
    {
        //시작할때 curritem에 다리 인벤토리를 담아둠 - 인벤토리 켠상태로 시작시 curritem에 안담겨서 아래코드 대로할시 버그생김
        //[09.28] 피드백 -  시작할떄 default 인벤토리 [먼저 조립되야하는 다리부분을] 켜놔야 좋을것 같다고 해서 수정
        currItem = inventory[2];
        inventory[2].SetActive(true);

    }
/*    public void SelectedButton()
    {
        //레그 인벤토리 의 selected 가켜지게
        buttonDown = true;
    }*/
    void Update()
    {
    }
    // Leg, Weapon , body 해당 메뉴마다 Onclick 시 해당 인벤토리 활성화 해놓음
    // inventory[0] = Weapon , [1] = Body [2] = Weapon   curritem = 현재 선택된 인벤토리
    public void Inven0Open()
    {
        if(currItem != null)
        {
            currItem.SetActive(false);
            currItem = null;
        }
        inventory[0].SetActive(true);
        currItem = inventory[0] ;
    }
    public void Inven1Open()
    {
        if (currItem != null)
        {
            currItem.SetActive(false);
            currItem = null;
        }
        inventory[1].SetActive(true);
        currItem = inventory[1];
    }
    public void Inven2Open()
    {
        if (currItem != null)
        {
            currItem.SetActive(false);
            currItem = null;
        }
        inventory[2].SetActive(true);
        currItem = inventory[2];
    }
    public void Inven0Close()
    {
        inventory[0].SetActive(false);
    }
    public void Inven1Close()
    {
        inventory[1].SetActive(false);
    }
    public void Inven2Close()
    {
        inventory[2].SetActive(false);
    }
    //좌 상단 Back Key  클릭시 로비씬으로 이동
    public void BackToMainScene()
    {
        SceneManager.LoadScene("03.Lobby");
    }

}
