using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("랩실에서 조립할려고 현재 선택한 무기")]
    public GameObject selectLeg;//선택한 다리
    public GameObject selectBody;//선택한 몸통
    public GameObject selectWeapon;//선택한 무기

    GameObject nowLeg;
    GameObject nowBody;
    GameObject nowWeapon;
    float nowLeftWeight;
    float nowWeight;
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
        
    }

    
    void Update()
    {
        
    }
    /// <summary>
    /// 부품을 선택하면 부품을 생성되야할 위치에 생성을하여 화면에 미리보기형태로 띄워준다
    /// </summary>
    public void SelectPart(GameObject part)
    {

        //선택된 부품이 다리이면
        if(part.GetComponent<Item>().itemType==0)
        {
            //Veector3.zero위치에 다리는 그냥 생성
            nowLeg = Instantiate(part, Vector3.zero, Quaternion.identity);
            nowLeftWeight = part.GetComponent<Item>().totalWeight; //현재 남은무게 = 다리의 총 하중
        }
        //선택된 부품이 몸통이면
        else if(part.GetComponent<Item>().itemType ==1)
        {
            //남은무게가 몸통의 무게보다 많다면 조립가능
            if(nowLeg.GetComponent<Item>().totalWeight >=part.GetComponent<Item>().body_Weight)
            {
            nowBody = Instantiate(part, nowLeg.GetComponent<Item>().nextParts.position, Quaternion.identity);
                nowLeftWeight -= part.GetComponent<Item>().body_Weight; //남은 무게 - 몸통무게 해서 남은 무게(하중)을 변화시킨다
            }
            else
            {
                Debug.Log("하중 초과입니다.");
            }
        }
        //선택된 부품이 무기이면
        else if(part.GetComponent<Item>().itemType ==2)
        {
            //무기의 무게가 남은 하중을 초과하지 않고 && 몸통의 타입과 무기의 타입이 일치할때
            if(nowLeftWeight >=part.GetComponent<Item>().weapon_Weight 
                && nowBody.GetComponent<Item>().bodyType == part.GetComponent<Item>().weaponType)
            {
            nowWeapon = Instantiate(part, nowBody.GetComponent<Item>().nextParts.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("하중이 초과했거나 몸통과 호환되지 않는 무기입니다.");
            }
        }
        else
        {

        }
    }
}
