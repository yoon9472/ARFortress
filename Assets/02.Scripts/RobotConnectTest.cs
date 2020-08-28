using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotConnectTest : MonoBehaviour
{

    public GameObject testLeg;
    public GameObject testBody;
    public GameObject testWeapon;
    void Start()
    {
        testBody.transform.GetComponent<BodyTest>().connectToLeg.position = testLeg.GetComponent<LegTest>().connectTobody.position;
        testBody.transform.SetParent(testLeg.transform);
        testWeapon.transform.GetComponent<TestWeapon>().connectWeapon.position = testBody.transform.GetComponent<BodyTest>().connectToWeapon.position;
        testWeapon.transform.SetParent(testBody.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
