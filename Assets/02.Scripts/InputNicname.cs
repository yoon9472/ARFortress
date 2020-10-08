using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNicname : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField inputname;
    public GameObject errormsg;
    public GameObject completemsg;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 버튼에 연결할 함수 닉네임을 변경한다.
    /// </summary>
    public void ButtonModify()
    {
        StartCoroutine(ModifyUserName());
    }
    public IEnumerator ModifyUserName()
    {
        //닉네임을 수정하는 함수를 호출한다.
        NetWork.Get.ModifyDisplayName(inputname.text);
        //변경후 콜백으로 실패후 성공이 넘어 올때까지 기다린다.
        yield return new WaitUntil(() => NetWork.Get.modifyOk == true||NetWork.Get.modifyFail == true);
        //넘어온 결과에 따라 메세지를 띠워준다.
        if(NetWork.Get.modifyOk == true)
        {
            //변경 성공 메세지
          completemsg.SetActive(true);
            //0.5초후 닉네임 변경창이 꺼진다.
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
        }

       if(NetWork.Get.modifyFail == true)
       {
            //변경 실패시 메세지
            errormsg.SetActive(true);
            //0.5초후 에러메세지는 다시 끈다.
            yield return new WaitForSeconds(0.5f);
            errormsg.SetActive(false);
        }
    }
}
