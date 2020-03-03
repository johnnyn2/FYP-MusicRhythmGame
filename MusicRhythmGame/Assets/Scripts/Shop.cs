using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("List of items sold")]
    [SerializeField] private ShopItem[] shopItems;

    [Header("References")]
    [SerializeField] private Transform shopContainer;
    [SerializeField] private GameObject shopItemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        PopulateShop();
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
            //  - SoldOut (Image)

            if(si.isSold)
            {
                itemObject.GetComponent<Button>().interactable = false;
                itemObject.transform.GetChild(1).gameObject.SetActive(false);
                itemObject.transform.GetChild(4).gameObject.SetActive(true);
            }
            else
            {
                itemObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(si));
            }
            

            itemObject.GetComponent<Image>().color = si.backgroundColor;
            itemObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = si.itemName;
            itemObject.transform.GetChild(1).GetComponent<Image>().sprite = si.sprite;
            itemObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "$ "+si.cost.ToString();
        }
    }

    private void OnButtonClick(ShopItem item)
    {
        
    }
}
