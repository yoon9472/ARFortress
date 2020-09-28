using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class TestClass
{
    public int age;
    public string name;

    public TestClass()
    {
        age = 0;
        name = "NoName";
    }


    public TestClass(int num, string str)
    {
        age = num;
        name = str;
    }
}




public class GameManager : MonoBehaviour
{
    [Header("무기 상세정보 담은 리스트")]
    public List<WeaponInfo> weaponInfo_List = new List<WeaponInfo>();    //무기의 상세 정보를 담을 리스트
    [Header("몸통 상세정보 담은 리스트")]
    public List<BodyInfo> bodyInfo_List = new List<BodyInfo>();    //몸통의 상세 정보를 담을 리스트
    [Header("다리 상세정보 담은 리스트")]
    public List<LegInfo> legInfo_List = new List<LegInfo>();    //다리의 상세 정보를 담을 리스트
    [Header("보유한 아이템 리스트")]
    public List<string> ownedItem_List = new List<string>();  //보유 아이템 리스트
    [Header("인트로에서 Prefab정보 담을 배열")]
    public GameObject[] weaponPartsArr ;
    [Header("인트로에서 Prefab정보 담을 배열")]
    public GameObject[] bodyPartsArr ;
    [Header("인트로에서 Prefab정보 담을 배열")]
    public GameObject[] legPartsArr ;
    [Header("상점에서 사용할 스프라이트 이미지")]
    public Sprite[] imgArr;
    [Header("랩실에서 조립할려고 현재 선택한 파츠")]
    public GameObject selectLeg;//선택한 다리
    public GameObject selectBody;//선택한 몸통
    public GameObject selectWeapon;//선택한 무기
    [Header("DB에 저장될 유저의 정보")]
    public UserInfo userinfo;//타이틀 데이터에 저장될 플레이어 정보

    //CurrentStats() 함수에서 현재 조립된 부품의 인덱스 정보를 기억한다.
    [Header("현재 조립된 부품의 인덱스 정보")]
    public int legindex;
    public int bodyindex;
    public int weaponindex;

    public int legtotalweight;
    public int legspeed;
    public int legamor;

    public int bodyhp;
    public int bodyamor;
    public string bodytype;
    public int bodyweight;

    public int weaponweight;
    public string weapontype;
    public int weaponlange;
    public int weaponattack;

    public bool isloadOver = false;//하중 초과여부
    public bool istypeNoeSame = false ;//타입 불일치 여부
    private static GameManager m_Instance = null;
    public static GameManager Get { get { return m_Instance; } set { m_Instance = value; } }
    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else if (m_Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }
    void Start()
    {
        imgArr = Resources.LoadAll<Sprite>("04.Img");
        Debug.Log("스타트함수");
        weaponPartsArr = Resources.LoadAll<GameObject>("01.Weapon");
        bodyPartsArr = Resources.LoadAll<GameObject>("02.Body");
        legPartsArr = Resources.LoadAll<GameObject>("03.Leg");
        
    }

    
    void Update()
    {
        
    }
    /// <summary>
    /// 선택된 부품에 따른 현재 능력치 표기
    /// </summary>
    public void CurrentStats()
    {
        if (selectLeg != null)
        {
            Debug.Log("다리 찾기 시작");
            for (int i = 0; i < legInfo_List.Count; i++)
            {
                if (selectLeg.name == legInfo_List[i].partName+"(Clone)")
                {
                    Debug.Log("인덱스 찾음");
                    legindex = i;
                }
            }

        }

        if (selectBody != null)
        {
            Debug.Log("몸통 찾기 시작");
            for (int i = 0; i < bodyInfo_List.Count; i++)
            {
                if (selectBody.name == bodyInfo_List[i].partName + "(Clone)")
                {
                    Debug.Log("인덱스 찾음");
                    bodyindex = i;
                }
            }

        }

        if (selectWeapon != null)
        {
            Debug.Log("무기 찾기 시작");
            for (int i = 0; i < weaponInfo_List.Count; i++)
            {
                if (selectWeapon.name == weaponInfo_List[i].partName + "(Clone)")
                {
                    Debug.Log("인덱스 찾음");
                    weaponindex = i;
                }
            }
        }

        if (selectLeg != null)
        {
            legtotalweight = legInfo_List[legindex].totalweight;
            legspeed = legInfo_List[legindex].speed;
            legamor = legInfo_List[legindex].amor;
        }
        if(selectBody !=null)
        {
            bodyhp = bodyInfo_List[bodyindex].hp;
            bodyamor = bodyInfo_List[bodyindex].amor;
            bodytype = bodyInfo_List[bodyindex].bodytype;
            bodyweight = bodyInfo_List[bodyindex].weight;
        }
        if(selectWeapon !=null)
        {
            weaponweight = weaponInfo_List[weaponindex].weight;
            weapontype = weaponInfo_List[weaponindex].weapontype;
            weaponlange = weaponInfo_List[weaponindex].lange;
            weaponattack = weaponInfo_List[weaponindex].attack;
        }
        
        if(selectLeg != null && selectBody !=null && selectWeapon !=null)
        {
            if (weaponweight + bodyweight > legtotalweight)
            {
                isloadOver = true;
            }
            else
            {
                isloadOver = false;
            }
        }

        if(selectBody !=null && selectWeapon !=null)
        {
            if (weapontype.Equals(bodytype))
            {
                istypeNoeSame = false;
            }
            else
            {
                istypeNoeSame = true;
            }
        }

    }

}

