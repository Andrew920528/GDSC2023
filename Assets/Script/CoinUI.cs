using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("CoinCount").GetComponent<TMP_Text>().SetText(StaticData.PlayerStats.Coins.ToString());
    }

    public void UpdateUI()
    {
        gameObject.transform.Find("CoinCount").GetComponent<TMP_Text>().SetText(StaticData.PlayerStats.Coins.ToString());
    }
}
