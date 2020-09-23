using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabPanel : MonoBehaviour
{
    public Text title;
    public Text introduction;
    public GameObject buttonText;
    public Text cost;
    // Start is called before the first frame update
    void Start()
    {
        cost = buttonText.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
