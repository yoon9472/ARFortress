using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : MonoBehaviour
{
    //이번튼을 누르면 가져올 프리팹 정보
    public GameObject partobj;
    public void SelectPart()
    {
        if(partobj !=null)
        {
        GameManager.Get.SelectPart(partobj);
        }
    }
}
