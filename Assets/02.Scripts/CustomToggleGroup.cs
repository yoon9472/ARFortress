using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomToggleGroup : ToggleGroup
{
    /*
        여기서 toggle 그룹의 모든 이벤트를 여기서 처리를 할 것
        음 상속받는 것이니 나머진 필요 없고 내가 할 것만 할 것임.
        하나를 누르면 다른 곳에서도 알아야 되잖아
        그렇다면 ㅇㅅㅇ 여기서 스위치문을 쓰자!
        스토어 매니저에서는 if else문을 쓰고 될 것 같음.
    */
    [SerializeField]
    protected Image allButtonImage;
    [SerializeField]
    protected Image weaponButtonImage;
    [SerializeField]
    protected Image bodyButtonImage;
    [SerializeField]
    protected Image legButtonImage;
    [SerializeField]
    protected Sprite highlightImage;
    [SerializeField]
    protected Sprite normalImage;
    [SerializeField]
    protected StoreManager sm;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
    }

    public void ForChangingPanel(int a)
    {//스위치문을 만들 것 임.
        switch(a)
        {
            case 0:
                //all을 눌렀을 때!
                OffAllToggle();
                m_Toggles[0].isOn = true;
                m_Toggles[0].image.sprite = highlightImage;
                sm.UpdateItemPanel(DataManager.Instance.itemList);
                sm.ResetScroll();
                break;
            case 1:
                //weapon을 눌렀을 때!
                OffAllToggle();
                m_Toggles[1].isOn = true;
                m_Toggles[1].image.sprite = highlightImage;
                sm.UpdateItemPanel(DataManager.Instance.weaponList);
                sm.ResetScroll();
                break;
            case 2:
                //body을 눌렀을 때!
                OffAllToggle();
                m_Toggles[2].isOn = true;
                m_Toggles[2].image.sprite = highlightImage;
                sm.UpdateItemPanel(DataManager.Instance.bodyList);
                sm.ResetScroll();
                break;
            case 3:
                //leg을 눌렀을 때!
                OffAllToggle();
                m_Toggles[3].isOn = true;
                m_Toggles[3].image.sprite = highlightImage;
                sm.UpdateItemPanel(DataManager.Instance.legList);
                break;
        }
    }
    protected void OffAllToggle()
    {
        for(int i =0; i< m_Toggles.Count; i++)
        {
            m_Toggles[i].isOn = false;
            m_Toggles[i].GetComponent<Image>().sprite = normalImage;
        }
        sm.DestroyChildObj();
    }
}
