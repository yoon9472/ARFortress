using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Permission : MonoBehaviour
{
    Permission permission;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("dobi");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*IEnumerator dobi() {
        // Ask for camera permission
        if(!Permission.HasUserAuthorizedPermission(Permission.Camera)) {
            Permission.RequestUserPermission(Permission.Camera);
        }
        yield return new WaitForSeconds(2.5f);
        // Ask for external storage permission
        if(!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite)) {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
}*/
}
