using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroRotate : MonoBehaviour
{
    Transform a;
    int speed = 800;
    // Start is called before the first frame update
    void Start()
    {
        a= GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        a.Rotate(Vector3.forward,Time.deltaTime*speed,Space.World);
    }
}
