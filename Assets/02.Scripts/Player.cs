using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PlayFab.GroupsModels;

public class Player : MonoBehaviourPunCallbacks
{
    public GameObject isMine;
    public string legname;//다리 이름
    public string bodyname;//몸체 이름
    public string weaponname; //무기 이름
    public int actnum; //이객체가 누구의 소유인지 체크

    public int attack;//공격력
    public int hp;//체력
    public int amor;//방어력
    public float speed;//이동속도
    public int lange;//사거리

    private GameObject legObj;
    private GameObject bodyObj;
    private GameObject weaponObj; //좌우회전때 회전시킬 대상
    private Transform barrel; //포신 위아래로 움직이는 대상

    void Start()
    {
        attack = GameManager.Get.weaponattack;
        hp = GameManager.Get.bodyhp;
        amor = GameManager.Get.legamor + GameManager.Get.bodyamor;
        speed = GameManager.Get.legspeed;
        lange = GameManager.Get.weaponlange;
        //다리 정보 찾아서 생성
        for(int i=0; i<GameManager.Get.legPartsArr.Length;i++)
        {
            if(GameManager.Get.legPartsArr[i].name==legname)
            {
                legObj = Instantiate(GameManager.Get.legPartsArr[i], transform.position, Quaternion.identity, transform);
            }
        }
        //몸체 정보 찾아서 생성
        for(int i=0;i<GameManager.Get.bodyPartsArr.Length;i++)
        {
            if(GameManager.Get.bodyPartsArr[i].name == bodyname)
            {
                bodyObj = Instantiate(GameManager.Get.bodyPartsArr[i], legObj.GetComponent<LegParts>().bodyPos.transform.position, Quaternion.identity, transform);
            }
        }
        //무기 정보 찾아서 생성
        for(int i=0; i<GameManager.Get.weaponPartsArr.Length;i++)
        {
            if(GameManager.Get.weaponPartsArr[i].name == weaponname)
            {
                weaponObj = Instantiate(GameManager.Get.weaponPartsArr[i], bodyObj.GetComponent<BodyParts>().weaponPos.transform.position, Quaternion.identity, transform);
            }
        }
        transform.forward = NetWork.Get.hostingResultList[0].Result.Anchor.transform.position - transform.position;
        if(actnum == NetWork.Get.myOrder)
        {
            isMine.SetActive(true);
        }
        barrel = weaponObj.GetComponent<WeaponParts>().ReturnTransform();
    }

    
    void Update()
    {
        //이로봇 객체의 엑터 넘버와 현재 턴이 일치할때 움직임값을 받아서 움직인다
        if(NetWork.Get.nowTurn == actnum )
        {
            if(NetWork.Get.isInput == true) MovePlayer();
            //현재 턴과 이 로봇 객체의 엑터넘버가 일치하고 위아래 회전중일때
            if (NetWork.Get.isRotateUpDown == true) RotateUpDown();
            //현재턴과 이 로봇 객체의 엑터넘버가 일치하고 좌우 회전중일때
            if (NetWork.Get.isRotateLeftRight == true) RotateLeftRight();
        }
    }

    /// <summary>
    /// NetWork의 x값은 컨트롤러의 좌우 값을, z는 앞뒤값을 변경하는데 사용한다
    /// </summary>
    public void MovePlayer()
    {
        //좌우 회전에 관한 x 값의 변화가 있을때
        if(NetWork.Get.x !=0)
        {
            //전진중일때
            if (NetWork.Get.z > 0)
            {
                transform.Rotate(Vector3.up * NetWork.Get.x);
            }
            //후진중일때
            if (NetWork.Get.z < 0)
            {
                transform.Rotate(Vector3.up * -NetWork.Get.x);
            }
        }
        //전진 후진 에 대한 z축값에 변화가 있을때
        if (NetWork.Get.z !=0)
        {
            Vector3 dir = transform.forward * NetWork.Get.z;
            dir.Normalize();
            transform.position += dir * (speed / 200) * Time.deltaTime;
        }
    }
    /// <summary>
    /// 좌우 회전할때
    /// </summary>
    public void RotateLeftRight()
    {
        weaponObj.transform.Rotate(Vector3.up * -NetWork.Get.rotateY);
    }
    /// <summary>
    /// 포신 위아래 회전할때
    /// </summary>
    public void RotateUpDown()
    {
        barrel.Rotate(Vector3.right * -NetWork.Get.rotateX);
    }
}
