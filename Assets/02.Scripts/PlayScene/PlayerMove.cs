using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject controller;
    private float x;
    private float z;
    public int speed;
    public Vector3 dir;
    void Start()
    {
        speed = 1;
    }

   
    void Update()
    {
        if(controller.GetComponent<VirtualJoystick>().isInput==true)
        {
            x = controller.GetComponent<VirtualJoystick>().inputVector.x;
            z = controller.GetComponent<VirtualJoystick>().inputVector.y;
            dir.Set(x, 0, z);
            transform.position += dir * speed * Time.deltaTime;
        }
    }
}
