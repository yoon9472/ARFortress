using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabPanel : MonoBehaviour
{
    public Text title;
    public Text introduction;
    public GameObject buttonText;
    Text cost;
    public string displayname;
    public string description;
    public string itemcost;
    // Start is called before the first frame update
    void Start()
    {
        cost = buttonText.GetComponentInChildren<Text>();
        title.text = displayname;
        introduction.text = description;
        cost.text = itemcost;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
