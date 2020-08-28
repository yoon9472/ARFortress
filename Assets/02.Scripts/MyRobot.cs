using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MyRobot : MonoBehaviourPunCallbacks
{
    public GameObject me;
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine == true)
        {
            me.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
