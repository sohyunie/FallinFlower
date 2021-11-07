using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseItemData
{
    public ShopCategory Category => this.category;
    public string Name => this.name;
    public int Price => this.price;
    public int ID => this.id;
    public bool IsAdLock => this.isAdLock;
    public PurchaseItemData(ShopCategory category, int id, string name, int price, bool isAdLock = false)
    {
        this.category = category;
        this.id = id;
        this.price = price;
        this.name = name;
        this.isAdLock = isAdLock;
    }
    private int id;
    private string name;
    private int price;
    private bool isAdLock ;
    private ShopCategory category;
}
