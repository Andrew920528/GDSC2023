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

    public Item(string name, int quantity, int price, string description)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
        Description = description;
    }
}
