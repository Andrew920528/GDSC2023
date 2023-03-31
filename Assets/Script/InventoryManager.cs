using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{  

    private void Awake()
    {
        UpdateItems();
    }

    public void UpdateItems()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < items.Length; ++i)
        {
        }
    }
}
