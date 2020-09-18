using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour
{
    public List<GameObject> leg = new List<GameObject>();
    public List<GameObject> bodie = new List<GameObject>();
    public List<GameObject> weapon = new List<GameObject>();

    public Transform createSpot;
    public void Start()
    {
   
       
    }

    public void LegSpiderCreate()
    {
        Vector3 createPos = createSpot.position;

        if (GameObject.FindGameObjectsWithTag("Leg") !=null)
        {
          GameObject obj = Instantiate(Resources.Load("03.Leg/Spider[4legs]") as GameObject, createPos, Quaternion.identity);
            obj.transform.localScale = new Vector3(5, 5, 5);
        }
        else
        {
            print("다리(하체) 부품이 이미 존재합니다.");
        }

    }
}
