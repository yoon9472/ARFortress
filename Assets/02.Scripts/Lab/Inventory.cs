using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public List<GameObject> inventory = new List<GameObject>();

    public GameObject currItem;
    void Start()
    {
        currItem = inventory[2];
        inventory[2].SetActive(true);
       
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Inven0Open()
    {
        if(currItem != null)
        {
            currItem.SetActive(false);
            currItem = null;
        }
        inventory[0].SetActive(true);
        currItem = inventory[0] ;
    }
    public void Inven1Open()
    {
        if (currItem != null)
        {
            currItem.SetActive(false);
            currItem = null;
        }
        inventory[1].SetActive(true);
        currItem = inventory[1];
    }
    public void Inven2Open()
    {
        if (currItem != null)
        {
            currItem.SetActive(false);
            currItem = null;
        }
        inventory[2].SetActive(true);
        currItem = inventory[2];
    }
    public void Inven0Close()
    {
        inventory[0].SetActive(false);
    }
    public void Inven1Close()
    {
        inventory[1].SetActive(false);
    }
    public void Inven2Close()
    {
        inventory[2].SetActive(false);
    }
    public void BackToMainScene()
    {
        SceneManager.LoadScene("03.Lobby");
    }

}
