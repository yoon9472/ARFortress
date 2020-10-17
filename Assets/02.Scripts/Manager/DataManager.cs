using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;

public class DataManager : MonoBehaviour
{
    protected static DataManager instance = null;
    private void Awake()
    {
        instance = this;
    }
    public static DataManager GetInstance()
    {
        if(instance == null)
        {
            GameObject obj = new GameObject();
            obj.name = "DataManager";
            instance = obj.AddComponent<DataManager>();
        }
        return instance;
    }

    public List<CatalogItem> itemList = new List<CatalogItem>();
    //무기 리스트
    public List<CatalogItem> weaponList = new List<CatalogItem>();
    //몸통 리스트
    public List<CatalogItem> bodyList = new List<CatalogItem>();
    //다리 리스트
    public List<CatalogItem> legList = new List<CatalogItem>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
