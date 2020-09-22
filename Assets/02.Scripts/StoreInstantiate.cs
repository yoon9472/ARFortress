using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System.Linq;

public class StoreInstantiate : MonoBehaviour
{
    GameObject pos;
    public GameObject pos1;
    public GameObject pos2;
    public GameObject pos3;

    public Text title1;
    public Text title2;
    public Text title3;

    public Text introduction1;
    public Text introduction2;
    public Text introduction3;

    Text cost1;
    Text cost2;
    Text cost3;

    public GameObject costparent1;
    public GameObject costparent2;
    public GameObject costparent3;

    public GameObject tank;

    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject weapon5;
    public GameObject weapon6;
    public GameObject weapon7;

    string store="Store";
    List<CatalogItem> listofitem = new List<CatalogItem>();
    //무기 리스트
    public List<CatalogItem> listWeapon = new List<CatalogItem>();
    //몸통 리스트
    public List<CatalogItem> listUpperBody = new List<CatalogItem>();
    //다리 리스트
    public List<CatalogItem> listLowerBody = new List<CatalogItem>();
    // Start is called before the first frame update
    void Start()
    {
        cost1 = costparent1.GetComponentInChildren<Text>();
        cost2 = costparent2.GetComponentInChildren<Text>();
        cost3 = costparent3.GetComponentInChildren<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    //여기서 playfab의 아이템 리스트를 가지고 오려고 한다.
    //저거로 가져오려고 하지만 어떻게 리스트에 저장을 할까?읭?? 어떤 형식으로 저장이 될 것인가?
    public void StoreItemList()
    {
        Debug.Log("아이템 리스트 가져오기");
        //무기 코스트 순서대로 정렬하기
        //NetWork.Get.GetCatalogItem("Store");
        //var forsortweapon = new Dictionary<string, uint>(); 노필요
        //listofitem = NetWork.Get.itemList; 
        Debug.Log("무기 정렬 시작");
        listWeapon = NetWork.Get.weaponList;
        for(int i = 0; i <listWeapon.Count; i++)
        {
            //forsortweapon = listWeapon[i].VirtualCurrencyPrices;노필요 
            //forsortweapon.OrderBy(x => x.value);
            //print("정렬순서 key : {0}, value : {1}",forsortweapon.Keys , forsortweapon.Values);
            Debug.Log(i +"번째 "+listWeapon[i].VirtualCurrencyPrices["GD"]);
        }
        Debug.Log("Dictionary 정렬 중");

        listWeapon.Sort(delegate (CatalogItem A, CatalogItem B)
        {
            if (A.VirtualCurrencyPrices["GD"]>B.VirtualCurrencyPrices["GD"]) return 1;
            else if(A.VirtualCurrencyPrices["GD"]<B.VirtualCurrencyPrices["GD"]) return -1;
            else return 0;
        }
        );
        Debug.Log("Dictionary 정렬 후");
        
        for (int i = 0; i < listWeapon.Count; i++)
        {
            Debug.Log(i +"번째 "+ listWeapon[i].VirtualCurrencyPrices["GD"]);
        }

        //몸통 정렬하기
        Debug.Log("몸통 정렬 시작");
        listUpperBody = NetWork.Get.bodyList;
        //var forsortUpper = new Dictionary<string, uint>();할 필요 없었음.
        for(int i  = 0; i < listUpperBody.Count; i++)
        {
            //forsortUpper = listUpperBody[i].VirtualCurrencyPrices; 필요 없음
            Debug.Log(i +"번째 "+ listUpperBody[i].VirtualCurrencyPrices["GD"]);
            //Debug.Log(i+ "번째 " + forsortUpper.Values.ToString());
        }
        Debug.Log("Dictionary 정렬 중");

        listUpperBody.Sort(delegate (CatalogItem A, CatalogItem B)
        {
            if (A.VirtualCurrencyPrices["GD"]>B.VirtualCurrencyPrices["GD"]) return 1;
            else if(A.VirtualCurrencyPrices["GD"]<B.VirtualCurrencyPrices["GD"]) return -1;
            else return 0;
        }
        );
        Debug.Log("Dictionary 정렬 후");
        
        for (int i = 0; i < listUpperBody.Count; i++)
        {
            Debug.Log(i +"번째 "+ listUpperBody[i].VirtualCurrencyPrices["GD"]);
        }

        //forsortUpper.OrderBy(Item =>Item.Key); 이것도 모두 5000으로 됨.
        //listUpperBody.Sort(); 이 함수부터 작동을 안함.
        /*var upperItem = from pair in forsortUpper
                    orderby pair.Value ascending
                    select pair;
        foreach (KeyValuePair<string, uint> pair in upperItem)
        {
            Debug.Log(pair.Key+"와 "+pair.Value);
        }
        foreach (var i in upperItem) { Debug.Log(i.ToString()); }
        */
        /*1. 실패 Debug.Log(i +"번째 "+ forsortUpper["GD"])가 모두 (i 번째 5000)으로 나옴.
        SortedDictionary<string,uint> sortedDictionary = new SortedDictionary<string,uint>(forsortUpper);
        for(int i  = 0; i < listUpperBody.Count; i++)
        {
            Debug.Log(i +"번째 "+ forsortUpper["GD"]);
        } 
        */
        //다리 코스트 순으로 정렬하기
        Debug.Log("다리 정렬 시작");
        listLowerBody = NetWork.Get.legList;      
        for(int i  = 0; i < listLowerBody.Count; i++)
        {
            //forsortUpper = listUpperBody[i].VirtualCurrencyPrices; 필요 없음
            Debug.Log(i +"번째 "+ listLowerBody[i].VirtualCurrencyPrices["GD"]);
            //Debug.Log(i+ "번째 " + forsortUpper.Values.ToString());
        }
        Debug.Log("Dictionary 정렬 중");

        listLowerBody.Sort(delegate (CatalogItem A, CatalogItem B)
        {
            if (A.VirtualCurrencyPrices["GD"]>B.VirtualCurrencyPrices["GD"]) return 1;
            else if(A.VirtualCurrencyPrices["GD"]<B.VirtualCurrencyPrices["GD"]) return -1;
            else return 0;
        }
        );
        Debug.Log("Dictionary 정렬 후");
        
        for (int i = 0; i < listLowerBody.Count; i++)
        {
            Debug.Log(i +"번째 "+ listLowerBody[i].VirtualCurrencyPrices["GD"]);
        }
        /*for (int i=0; i<listofitem.Count;i++)
            {
                print("아이템 아이디 "+listofitem[i].ItemId);
                print("아이템 이름 "+listofitem[i].DisplayName);
                print("아이템 설명 "+listofitem[i].Description);
                print("가상화폐 가격 "+listofitem[i].VirtualCurrencyPrices);
                var catalog = listofitem[i];
                Debug.Log("아이템 아이디 "+catalog.ItemId);
                Debug.Log("아이템 이름 "+catalog.DisplayName);
                Debug.Log("아이템 설명 "+catalog.Description);
                Debug.Log("가상화폐 가격 "+catalog.VirtualCurrencyPrices);
            }*/

    }
    //얘를 껐다 킬 수 있는 것일까?
    GameObject item1;
    GameObject item2;
    GameObject item3;
    //음 하는데 밖으로 빼고 싶다...
    public void ShowAllItem(int count)
    {
        switch (count)
        {
            case 1 :
                
            break;

            case 2 :
                //title1.text = listofitem.itemid;
            break;
        }
        //이것으로 할지
        for (int i =0 ; i < 7; i++ )
        {
            if(count == i)
            {
                // for (int j =0; j < 3; j++)
                // {
                //     Instantiate(tank+i, pos+j; Quaternion.identity);
                // }요건 안된다고 하니 패스
            }//이것으로 할지 고민중 하지만 둘 다 엄청난 노가다가 필요할 것이라는 것은 똑같음.
        }
    }

    public void ShowWeaponItem(int count)
    {
        switch (count)
        {
            case 1 :
                if(item1 != null) 
                {
                    Destroy(item1);
                    Destroy(item2);
                    Destroy(item3);
                }
                //1번부터 생성시킬 것임
                //title1.text = listofitem[0].ItemId ;
                //item1 = Instantiate(weapon1, pos1.transform.position, Quaternion.identity);
                //introduction1.text = listofitem[0].Description;
                //cost1.text = listofitem[0].VirtualCurrencyPrices.ToString();

                //2번 생성
                title2.text = "머신건";
                item2 = Instantiate(weapon2, pos.transform.position, Quaternion.identity);
                introduction2.text =  "무게 : 40 " +"\r\n"
                                    + "공격 : 140 " +"\r\n"
                                    + "사정거리 : 15";
                cost2.text = "0";

                //3번 생성
                title3.text =" 핸드캐넌";
                item3 = Instantiate(weapon3, pos.transform.position, Quaternion.identity);
                introduction3.text =  "무게 : 50 " +"\r\n"
                                    + "공격 : 160 " +"\r\n"
                                    + "사정거리 : 20";
                cost3.text = "300";
            break;

            case 2 :
                if(item1 != null) 
                {
                    Destroy(item1);
                    //만약 2,3번에 null인데 destroy를 실행 시킨다면? 다음과 같이 하면 pass하려나..?
                    if(item2 == null) 
                    break;//이거 맞으려나?
                    Destroy(item2);
                    Destroy(item3);
                }
                //4번 생성
                title1.text = "팔랑스";
                item1 =Instantiate(weapon4, pos1.transform.position, Quaternion.identity);
                introduction1.text =  "무게 : 70 " +"\r\n"
                                    + "공격 : 200 " +"\r\n"
                                    + "사정거리 : 35" +"\r\n"
                                    + "방어력 무시 : 20" +"\r\n"
                                    + "체력비례 데미지 10% ";
                cost1.text = "2000";

                //5번 생성
                title2.text = "배럴";
                item2 = Instantiate(weapon5, pos.transform.position, Quaternion.identity);
                introduction2.text =  "무게 : 50 " +"\r\n"
                                    + "공격 : 200 " +"\r\n"
                                    + "사정거리 : 20" +"\r\n"
                                    + "폭발 범위 증가";
                cost2.text = "3000";

                //6번 생성
                title3.text ="데미시스";
                item3 = Instantiate(weapon6, pos.transform.position, Quaternion.identity);
                introduction3.text =  "무게 : 50 " +"\r\n"
                                    + "공격 : 160 " +"\r\n"
                                    + "사정거리 : 20" +"\r\n"
                                    + "피격시 체력 비례 지속대미지 5%";
                cost3.text = "4000";            
            break;

            case 3:
                if(item1 != null) 
                {
                    Destroy(item1);
                    Destroy(item2);
                    Destroy(item3);
                }
                //7번 생성
                title1.text = "바주카";
                item1 =Instantiate(weapon7, pos1.transform.position, Quaternion.identity);
                introduction1.text =  "무게 : 55 " +"\r\n"
                                    + "공격 : 200 " +"\r\n"
                                    + "사정거리 : 22" +"\r\n"
                                    + "폭발범위 증가";
                cost1.text = "5000";

                //0번 생성
                title2.text = " ";
                item2 = null;
                introduction2.text = " ";
                cost2.text = " ";

                //0번 생성
                title3.text =" ";
                item3 = null;
                introduction3.text = " ";
            break;
        }
        if(count == 1)
        {
            
        }
    }
    
    public void ShowUpperBodyItem(int count)
    {
        switch (count)
        {
            case 1 :

            break;

            case 2 :

            break;
        }

        if(count == 1)
        {

        }
    }
    
    public void ShowLowerBodyItem(int count)
    {
        switch (count)
        {
            case 1 :

            break;

            case 2 :

            break;
        }

        if(count == 1)
        {

        }
    }
}
