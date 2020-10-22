using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileShoot : MonoBehaviour
{
    public bool isFire = false;
    public Vector3 dir;//포신이 향하고 있는 방향
    public float speed;// 속도
    [SerializeField]
    protected Rigidbody rb;

    private void Start()
    {
        rb.velocity = dir * speed*10;
    }

}
