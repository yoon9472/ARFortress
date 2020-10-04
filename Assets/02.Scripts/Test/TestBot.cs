using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.forward = NetWork.Get.hostingResultList[0].Result.Anchor.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
