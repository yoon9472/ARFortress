using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        
    }
    public class Items
    {
        public string itemName;
        public int damage;
        public int itemPrice;
        public int hp;
        public string itemDesc;
        //아이템이미지
        public Sprite itemImage;
        //아이템 3d 오브젝트
        public GameObject itemP;
        //아이템 형태( 탑형,인간형, 탱크형 ,, 팔형, 탑형, 어깨형 등)
        public string itemType;
    }
    public Item(string _itemName, int _itemValue, int _itemPrice, string _itemDesc, Sprite _itemImage, GameObject _itemP)
    {

    }
    public Item()
    {

    }
}
