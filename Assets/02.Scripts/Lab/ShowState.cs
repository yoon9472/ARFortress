using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowState : MonoBehaviour
{
    public Text state1_1;
    public Text state1_2;
    public Text state1_3;
    public Text state1_4;
    public Text state1_5;
    public Text state1_6;
    public Text state1_7;

    public Text state2_1;
    public Text state2_2;
    public Text state2_3;
    public Text state2_4;
    public Text state2_5;
    public Text state2_6;
    public Text state2_7;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Get.isloadOver == true)
        {
            state1_1.color = Color.red;
        }
        else
        {
            state1_1.color = Color.white;
        }
        if(GameManager.Get.istypeNoeSame == true)
        {
            state2_2.color = Color.red;
        }
        else
        {
            state2_2.color = Color.white;
        }

        state1_1.text = (GameManager.Get.bodyweight + GameManager.Get.weaponweight).ToString();//몸+무기 무게
        state2_1.text = GameManager.Get.legtotalweight.ToString();//다리 하중량
        state1_2.text = GameManager.Get.bodytype;//몸통 타입
        state2_2.text = GameManager.Get.weapontype;//무기 타입
        state1_3.text = GameManager.Get.bodyhp.ToString();
        state2_3.text = " ";
        state1_4.text = (GameManager.Get.legamor + GameManager.Get.bodyamor).ToString();
        state2_4.text = " ";
        state1_5.text = GameManager.Get.legspeed.ToString();
        state2_5.text = " ";
        state1_6.text = GameManager.Get.weaponattack.ToString();
        state2_6.text = " ";
        state1_7.text = GameManager.Get.weaponlange.ToString();
        state2_7.text = " ";
    }
}
