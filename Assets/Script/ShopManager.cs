using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TMP_Text warningText;
    private InventoryManager inventoryManager;
    private GameObject[] coinUIObjects;

    private void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        coinUIObjects = GameObject.FindGameObjectsWithTag("CoinCount");
        int verticalSpace = 220;
        int verticalOffset = 250;

        List<Item> itemList = StaticData.itemList;

        for (int i = 0; i < itemList.Count; ++i)
        {
            Item item = itemList[i];
            GameObject itemObj = Instantiate(item.ItemPrefab, transform);
            itemObj.transform.localPosition = new Vector3(0, verticalOffset + i * -verticalSpace, 0);
            itemObj.transform.localScale = new Vector3(1, 1, 1);
                
            Button buyButton = itemObj.GetComponentInChildren<Button>();

            buyButton.onClick.AddListener(() =>
            {
                BuyItem(item);
            });
            
        }
    }
    public void BuyItem(Item item)
    {
        if (StaticData.PlayerStats.Coins >= item.Price)
        {
            // Able to buy item

            if (StaticData.itemInventory.ContainsKey(item.Name))
            {
                StaticData.itemInventory[item.Name]++;
            } else
            {
                // Initialize item if not found in dictionary
                StaticData.itemInventory.Add(item.Name, 1);
            }
            StaticData.PlayerStats.Coins -= item.Price;
            foreach (GameObject g in coinUIObjects)
            {
                g.GetComponent<TMP_Text>().text = StaticData.PlayerStats.Coins.ToString();
            }

            warningText.text = "";
            Debug.Log("Successfully bought item!");
            inventoryManager.UpdateItems();
        }
        else
        {
            // Unable to buy item
            
            warningText.text = "Not enough coins!";
            Debug.Log("Can't buy this item");
        }

    }
}
