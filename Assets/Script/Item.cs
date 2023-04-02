using System;
using UnityEngine;

[Serializable]
public class Item
{
    [SerializeField]
    public string Name { get; set; }
    [SerializeField]
    public int Quantity { get; set; }
    [SerializeField]
    public int Price { get; set; }
    [SerializeField]
    public string Description { get; set; }
    public GameObject ItemPrefab { get; }

    public Item(string name, int quantity, int price, string description)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
        Description = description;
    }

    public Item(string name, int quantity, int price, string description, GameObject itemObject)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
        Description = description;
        ItemPrefab = itemObject;
    }
}
