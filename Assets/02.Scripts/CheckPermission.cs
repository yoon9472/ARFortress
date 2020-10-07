using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class CheckPermission : MonoBehaviour
{
    //어떻게 퍼미션을 얻을지 고민이 많다.
    bool finalCheck = false;
    bool cameraCheck = false;
    bool storageCheck = false;
    float timeCheck =0;
    //이것은 우리 권한 요청 할 때 띄워주는것
    public GameObject forRequestPermission;
    //이것은 우리 권한 요청할 때 확인 하는 것.
    bool btnCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        forRequestPermission.gameObject.SetActive(false);
        StartCoroutine("PermissionCheck");//먼저 무조건 퍼미션 체크를 시작하자.
    }

    // Update is called once per frame
    void Update()
    {   
    }
    public void YesCheck()
    {
        Debug.Log("우리 것 접근권한 ok");
        btnCheck = true;
        forRequestPermission.gameObject.SetActive(false);
    }
    IEnumerator PermissionCheck() 
    {
        //여기서 권한 확인. 하나하나 해야 되나 싶기도 하다. 
        if(Permission.HasUserAuthorizedPermission(Permission.Camera)==false)//카메라 권한 체크 시작
        {
            Debug.Log("카메라 권한이 없습니다.");
            cameraCheck = false;
        }
        yield return null;
        /*if(Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead)==false) //저장공간 읽는 권한 체크 시작 
        {
            Debug.Log("저장공간 읽는 권한이 없습니다.");
            storageCheck = false;
        }
        yield return null;
        if(Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite)==false) //저장공간 쓰는 권한 체크 시작 
        {
            Debug.Log("저장공간 쓰는 권한이 없습니다.");
            storageCheck = false;
        }
        yield return null;*/ //이 아이들은 스크린샷이 들어가면 넣기 위한 초석
        if(/*storageCheck == false ||*/ cameraCheck == false)//얘는 나중에 누가 권한 껐을 때를 대비해서 아래 반복문으로 넣기 위한 작은 초석.
        {
            Debug.Log("접근권한이 없단댜~!");
            finalCheck = false;
        }
        yield return new WaitForSeconds(0.1f);
        if(finalCheck == true)//코루틴 
        {
            Debug.Log("권한 요청 다 되어있었네! 코루틴 꺼라 애들아!");
            StopCoroutine("PermissionCheck");
        }
        yield return StartCoroutine("RequestPermission");
    }
    IEnumerator RequestPermission()
    {
        timeCheck += Time.deltaTime;
        //여기서 부터 반복을 시작한다.
        while (finalCheck == false)
        {
            Debug.Log("애들아 접근권한 없다잖아! 빨리 따내와!");
            //여기서 권한 요청 시작 ( 우리 것 부터 권한 요청 시작을 하자. 그 다음에 안드로이드에서 자동 권한 요청을 하도록 )
            //이것은 우리 권한 요청 한다고 창을 띄워주는 것.
            forRequestPermission.gameObject.SetActive(true);
            yield return new WaitUntil(()=>btnCheck);//이것은 btncheck가 true값을 받을 때까지 기다리겠다고 하는 것임. false값일때까지면 waitwhile임.
            
            Debug.Log("카메라 권한 내놓으라구!");
            if(cameraCheck==false)
            {
                Permission.RequestUserPermission(Permission.Camera);//안드로이드의 카메라 
            }
            yield return new WaitForSeconds(0.2f);
            
            if(Permission.HasUserAuthorizedPermission(Permission.Camera)==true)//카메라 권한 체크 다시 시작 권한 있음 true로 바꿈.
            {
                Debug.Log("카메라 권한은 받았자냐~");
                cameraCheck = true;
            }
            /*
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);//저장공간 쓰는 권한 체크 시작 
            yield return new WaitForSeconds(0.2f);
            
            if(Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite)==true)//저장공간 쓰는 권한 체크 다시 시작 권한 있음 true로 바꿈.
            {
                storageCheck = true;
            }
            */

            //이 반복을 끝내기 위해서 
            if(cameraCheck ==true /*&& storageCheck ==true*/)
            {
                Debug.Log("이 반복을 드디어 끝낼 수 있는 기회야!");
                finalCheck = true;
            }
            else if( timeCheck > 15)
            //여기는 혹시 만약에 시간이 너무 오래 걸린다 싶으면 그냥 경고창 하고 강제종료시키자. 
            {
                Debug.Log("너무 오랜 시간 필수권한을 동의하지 않아 강제 종료하겠습니다.");//근데 이렇게 해도 되는건가?
                //여기에 창 하나 띄워줄까 생각 중; 창 띄우면 stopcoroutine 넣어야함.
                Application.Quit();
            }
            //else 해봤자 어차피 돌리는것밖에 안남으니까 그냥 else는 안씀.
        }
        //deltatime이 계속 쌓이면 안되니까 0으로 하고. 그냥 뒀다가 
        timeCheck = 0;
    }
}
