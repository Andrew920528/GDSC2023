using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    private CoinUI coinUI;
    public List<GameObject> itemPrefabs;
    public GameObject itemInfoPrefab;
    private GameObject itemInfoTab;
    private DataManager dataManager;
   

    private void Awake()
    {
        dataManager = GameObject.FindObjectOfType<DataManager>();
        dataManager.Load();
        StaticData.itemInventory = dataManager.GetGameData().itemInventory;

        coinUI = GameObject.FindObjectOfType<CoinUI>();
        int verticalSpace = 220;
        int verticalOffset = 250;

        for (int i = 0; i < itemPrefabs.Count; ++i)
        {
            Item item = StaticData.itemList[i];
            GameObject itemObj = Instantiate(itemPrefabs[i], transform);
            itemObj.transform.localPosition = new Vector3(0, verticalOffset + i * -verticalSpace, 0);
            itemObj.transform.localScale = new Vector3(1, 1, 1);



            itemObj.transform.Find("ItemCount").GetComponent<TMP_Text>().SetText(StaticData.itemInventory[item.Name].ToString());
            int index = i;
            itemObj.GetComponentInChildren<Button>().onClick.AddListener(() =>
           {
               if (itemInfoTab == null)
               {
                   itemInfoTab = Instantiate(itemInfoPrefab, transform);
               }
               itemInfoTab.SetActive(true);
               itemInfoTab.transform.Find("Title").GetComponent<TMP_Text>().SetText(item.Name);
               itemInfoTab.transform.Find("Description").GetComponent<TMP_Text>().SetText(item.Description);
               itemInfoTab.GetComponentInChildren<Image>().sprite = itemPrefabs[index].GetComponentInChildren<Image>().sprite;
           });

        }
    }

    public void UpdateItems()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < items.Length; ++i)
        {
            Item item = StaticData.itemList[i];
            items[i].transform.Find("ItemCount").GetComponent<TMP_Text>().SetText(StaticData.itemInventory[item.Name].ToString());
        }
        coinUI.UpdateUI();
    }
}
