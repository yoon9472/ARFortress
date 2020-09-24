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
    public string itemId;
    public int price;
    public Image image;
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
    public void Buy()
    {
        Debug.Log("나 이거 살꺼다!");
        NetWork.Get.BuyItem("store", itemId ,"GD", price);
        
    }
}
