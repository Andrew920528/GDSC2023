using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    private GameObject coinCount;
    private GameObject[] items;

    private void Awake()
    {
        items = GameObject.FindGameObjectsWithTag("Item");
        coinCount = GameObject.FindGameObjectWithTag("CoinCount");
        UpdateItems();
    }

    public void UpdateItems()
    {
        for (int i = 0; i < items.Length; ++i)
        {
            GameObject item = items[i];
            item.GetComponent<TMP_Text>().text = StaticData.itemInventory[item.name].ToString();
        }

        coinCount.GetComponent<TMP_Text>().text = StaticData.PlayerStats.Coins.ToString();
    }
}
