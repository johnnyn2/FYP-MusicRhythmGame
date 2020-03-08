using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("List of items sold")]
    [SerializeField] private ShopItem[] shopItems;

    [Header("References")]
    [SerializeField] private Transform shopContainer;
    [SerializeField] private GameObject shopItemPrefab;
    private int coins;
    private int health;
    // Start is called before the first frame update
    void Start()
    {
        PopulateShop();
        if(!PlayerPrefs.HasKey("Health"))
            PlayerPrefs.SetString("Health","100");
        if(!PlayerPrefs.HasKey("Coins"))
            PlayerPrefs.SetString("Coins","0");
        Int32.TryParse(PlayerPrefs.GetString("Coins","0"),out coins);
        Int32.TryParse(PlayerPrefs.GetString("Health","100"),out health);
        GameObject.FindGameObjectWithTag("Health").GetComponent<TextMeshProUGUI>().text = health.ToString();
        GameObject.FindGameObjectWithTag("Coins").GetComponent<TextMeshProUGUI>().text = "$ " + coins;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void PopulateShop()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            ShopItem si = shopItems[i];
            GameObject itemObject = Instantiate(shopItemPrefab, shopContainer);

            // This access the prefab's component, and change it based off yourr ShopItem structure
            // ShopItem (Image, Button)
            //  - Name (TextMeshProGUI)
            //  - Sprite (Image)
            //  - Boarder (Image)
            //  - cost (TextMeshProUGUI)

            if(PlayerPrefs.GetString(si.itemName).Equals("true"))
            {
                itemObject.GetComponent<Button>().interactable = false;
                itemObject.transform.GetChild(1).gameObject.SetActive(false);
                itemObject.transform.GetChild(4).gameObject.SetActive(true);
            }
            else
            {
                itemObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(itemObject, si));
            }
            

            itemObject.GetComponent<Image>().color = si.backgroundColor;
            itemObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = si.itemName;
            itemObject.transform.GetChild(1).GetComponent<Image>().sprite = si.sprite;
            itemObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "$ "+si.cost.ToString();
        }
    }

    private void OnButtonClick(GameObject item, ShopItem si)
    {
        if(coins > si.cost){
            //GUI
            item.GetComponent<Button>().interactable = false;
            item.transform.GetChild(1).gameObject.SetActive(false);
            item.transform.GetChild(4).gameObject.SetActive(true);
            PlayerPrefs.SetString("Health", (health+si.incHealth).ToString());
            // coins
            coins -= si.cost;
            GameObject.FindGameObjectWithTag("Coins").GetComponent<TextMeshProUGUI>().text = "$ " + coins;
        } else {
            StopAllCoroutines();
            StartCoroutine(Warning());
        }
    }

    public void returnBtn(){
        SceneManager.LoadScene("Menu");
    }
    IEnumerator Warning()
    {
        GameObject warning = GameObject.Find("Warning");
        for(float ft =1f; ft>=0; ft-=0.01f)
        {
            Color c = warning.GetComponent<TextMeshProUGUI>().color;
            c.a = ft;
            warning.GetComponent<TextMeshProUGUI>().color = c;
            yield return new WaitForSeconds(.01f);
        }
    }
}
