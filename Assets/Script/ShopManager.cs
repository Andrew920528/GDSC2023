using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    private CoinUI coinUI;
    public TMP_Text warningText;
    public List<GameObject> itemPrefabs;
    private DataManager dataManager;

    private void Awake()
    {
        dataManager = GameObject.FindObjectOfType<DataManager>();
        coinUI = GameObject.FindObjectOfType<CoinUI>();
        int verticalSpace = 220;
        int verticalOffset = 250;

        for (int i = 0; i < itemPrefabs.Count; ++i)
        {
            Item item = StaticData.itemList[i];
            GameObject itemObj = Instantiate(itemPrefabs[i], transform);
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
        if (StaticData.Coins >= item.Price)
        {
            // Able to buy item

            if (StaticData.itemInventory.ContainsKey(item.Name))
            {
                StaticData.itemInventory[item.Name]++;
            } else
            {
                StaticData.itemInventory[item.Name] = 1;
            }
            StaticData.Coins -= item.Price;
            warningText.text = "";
            coinUI.UpdateUI();
            Debug.Log("Successfully bought item!");

            dataManager.SetCoins(StaticData.Coins);
            dataManager.SetItems(StaticData.itemInventory);
            dataManager.Save();
        }
        else
        {
            // Unable to buy item
            
            warningText.text = "Not enough coins!";
            Debug.Log("Can't buy this item");
        }

    }
}
