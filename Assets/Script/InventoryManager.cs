using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    private GameObject[] coinCount;
    private GameObject[] items;
    public static InventoryManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
        items = GameObject.FindGameObjectsWithTag("Item");
        coinCount = GameObject.FindGameObjectsWithTag("CoinCount");
        UpdateItems();
    }

    public void UpdateItems()
    {
        // Update item UI
        for (int i = 0; i < items.Length; ++i)
        {
            GameObject item = items[i];
            item.GetComponent<TMP_Text>().text = StaticData.itemInventory[item.name].ToString();
        }

        // Update coins UI
        foreach (GameObject g in coinCount)
        {
            g.GetComponent<TMP_Text>().text = StaticData.PlayerStats.Coins.ToString();
        }
    }
}
