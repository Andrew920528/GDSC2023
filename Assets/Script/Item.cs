using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }

    public Item(string name, int quantity, int price, string description)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
        Description = description;
    }
}
