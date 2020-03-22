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
    [SerializeField] private TextMeshProUGUI itemEffect;
    [SerializeField] private GameObject warning;
    [SerializeField] private GameObject confirmBtn;
    [SerializeField] private GameObject cancelBtn;
    private GameObject selectedItem;
    private ShopItem selectedShopItem;
    private int coins;
    private int health;
    private int damageRed;
    // Start is called before the first frame update
    void Start()
    {
        PopulateShop();

        if(!PlayerPrefs.HasKey("Health"))
            PlayerPrefs.SetInt("Health",100);
        if(!PlayerPrefs.HasKey("Coins"))
            PlayerPrefs.SetInt("Coins",0);
        if(!PlayerPrefs.HasKey("DamageRed"))
            PlayerPrefs.SetInt("DamageRed",0);
        // Int32.TryParse(PlayerPrefs.GetString("Coins","0"),out coins);
        coins = PlayerPrefs.GetInt("Coins");
        // Int32.TryParse(PlayerPrefs.GetString("Health","100"),out health);
        health = PlayerPrefs.GetInt("Health");
        damageRed = PlayerPrefs.GetInt("DamageRed");
        GameObject.FindGameObjectWithTag("Health").GetComponent<TextMeshProUGUI>().text = health.ToString();
        GameObject.FindGameObjectWithTag("Coins").GetComponent<TextMeshProUGUI>().text = "$ " + coins;
        
    }

    private void PopulateShop()
    {
        Debug.Log("Populate Shop");
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

            if(si.isbrought)
            {
                // itemObject.GetComponent<Button>().interactable = false;
                // itemObject.transform.GetChild(1).gameObject.SetActive(false);
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
        selectedItem = item;
        selectedShopItem = si;
        if(!si.isbrought){
            confirmBtn.SetActive(true);
            cancelBtn.SetActive(true);
        }
        itemEffect.text = si.itemEffect;
    }

    public void returnBtn(){
        SceneManager.LoadScene("Menu");
    }

    public void confirm(){
        if (coins > selectedShopItem.cost && selectedShopItem.isbrought == false)
        {
            //GUI
            selectedItem.GetComponent<Button>().interactable = false;
            //selectedItem.transform.GetChild(1).gameObject.SetActive(false);
            selectedItem.transform.GetChild(4).gameObject.SetActive(true);
            PlayerPrefs.SetInt("Health", health + selectedShopItem.incHealth);
            PlayerPrefs.SetInt("DamageRed", damageRed + selectedShopItem.damageRed);
            selectedShopItem.isbrought = true;
            // coins
            coins -= selectedShopItem.cost;
            GameObject.FindGameObjectWithTag("Coins").GetComponent<TextMeshProUGUI>().text = "$ " + coins;
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(Warning());
        }
        cancel();
    }
    public void cancel(){
        Debug.Log("cancel");
        confirmBtn.SetActive(false);
        cancelBtn.SetActive(false);
    }
    IEnumerator Warning()
    {
        warning.SetActive(true);
        for(float ft =1f; ft>=0; ft-=0.01f)
        {
            Color c = warning.GetComponent<TextMeshProUGUI>().color;
            c.a = ft;
            warning.GetComponent<TextMeshProUGUI>().color = c;
            yield return new WaitForSeconds(.01f);
        }
        warning.SetActive(false);
    }
}
