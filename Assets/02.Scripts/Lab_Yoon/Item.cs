using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //아이템의 기본정보를 정의하는곳

    [Header("아이템 타입")]
    public int itemType;//아이템 타입 0=다리 1=몸통 2=무기
    [Header("다음 부품 결합될 위치")]
    public Transform nextParts; //다음 부품이 결합될위치
    [Header("부품의 중심점")]
    public Transform pivot;//부품의 중심점
    [Header("무기 관련 정보")]
    public float lange;//사거리
    public float attack;//공격력
    public int weaponType;//무기타입 0=탑형 1=팔형 2=어깨형
    public int weapon_Weight;//무기무게
    [Header("몸통 관련 정보")]
    public float hp;//체력
    public float amor;//방어력
    public int bodyType;//장착가능 무기 타입 0=탑형 1=팔형 2=어깨형
    public int body_Weight;//몸통무게
    [Header("다리 관련 정보")]
    public float moveSpeed;//이동속도
    public float totalWeight;//하중

}
