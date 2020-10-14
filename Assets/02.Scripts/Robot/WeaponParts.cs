using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParts : MonoBehaviour
{
    [SerializeField]
    protected Transform weaponUpDownTransform;//무기 부분에서 포신의 위아래를 위한 트랜스폼
    public Transform ReturnTransform()
    {
        return weaponUpDownTransform;
    }
}
