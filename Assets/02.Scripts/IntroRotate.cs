using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroRotate : MonoBehaviour
{
    int rotateSpeed =2000;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.forward, Time.deltaTime * rotateSpeed, Space.World);   
    }
}
