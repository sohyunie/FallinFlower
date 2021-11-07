using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PurchaseType
{
    Flower,
    Vase,
}

[System.Serializable]
public class PurchaseData
{
    public PurchaseData(string name, ShopCategory category, int count)
    {
        this.name = name;
        this.category = category;
        this.count = count;
    }
    public string name;
    public int count;
    public ShopCategory category;
}

[System.Serializable]
public class UserData
{
    public UserData(string userName, int coin, int flower, int pot, int vase)
    {
        this.userName = userName;
        this.coin = coin;
        this.currentFlower = flower;
        this.currentVase = vase;
        this.isADFree = false;

        itemList.Add(new PurchaseData("Tutorial", ShopCategory.Flower, 1));
        itemList.Add(new PurchaseData("Vase", ShopCategory.Vase, 1));
        //itemList.Add(new PurchaseData("Brown Flower Pot", 1));
    }
    public int currentFlower;
    public int currentVase;    
    public string userName;
    public int coin;
    public bool isADFree;
    public List<PurchaseData> itemList = new List<PurchaseData>();
}

