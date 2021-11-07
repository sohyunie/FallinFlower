using System.Transactions;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    protected void Start()
    {
        this.LoadData();
    }

    //////////////////////////////////////////////////////////////////////////////////////////

    public UserData UserData => this.userData;

    public void SetUserData(UserData data)
    {
        this.userData = data;
    }

    public string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    public T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }
    public void SaveJsonData(UserData pData)
    {
        // Json 변환
        string sJsonData = this.ObjectToJson(pData);

        // 파일 저장
        string sPath = string.Format(Application.persistentDataPath + "/" + StringConst.ID_PATH, pData.userName);
        System.IO.FileInfo file = new System.IO.FileInfo(sPath);
        file.Directory.Create();
        File.WriteAllText(file.FullName, sJsonData);
        Debug.Log(sJsonData);

        this.userData = pData;
    }

    public UserData LoadJsonData(string id)
    {
        string sPath = string.Format(Application.persistentDataPath + "/" + StringConst.ID_PATH, id);
        System.IO.FileInfo loadfile = new System.IO.FileInfo(sPath);
        if (loadfile.Exists == false)
        {
            Debug.LogError("파일 없음");
            return null;
        }
        string sJsonData = File.ReadAllText(loadfile.FullName);
        Debug.Log(sJsonData);

        this.userData = this.JsonToObject<UserData>(sJsonData);

        return this.userData;
    }
    private UserData userData;    

    public bool UseCoin(int coin)
    {
        int result = this.UserData.coin - coin;
        if (result >= 0)
        {
            this.UserData.coin = result;
            this.SaveJsonData(this.UserData);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddCoin(int coin)
    {
        this.UserData.coin += coin;
        this.SaveJsonData(this.UserData);
    }

    public void PurchaseItem(string itemName, ShopCategory category, int count = 1)
    {
        PurchaseData pData = this.userData.itemList.Find(l => l.name.CompareTo(itemName) == 0);
        if (pData != null)
        {
            pData.count++;
        }
        else
        {
            this.userData.itemList.Add(new PurchaseData(itemName, category, count));
        }
        this.SaveJsonData(this.userData);
    }

    public void SetItemByCategory(ShopCategory category, int itemID)
    {
        switch(category)
        {
            // case ShopCategory.Pot:
            //     this.userData.currentPot = itemID;
            //     break;
            case ShopCategory.Vase:
                this.userData.currentVase = itemID;
                break;
            case ShopCategory.Flower:
                this.userData.currentFlower = itemID;
                break;
        }
        this.SaveJsonData(this.userData);
    }

    public int GetItemByCategory(ShopCategory category)
    {
        int id = 0;
        switch(category){
            // case ShopCategory.Pot:
            //     id = this.userData.currentPot;
            //     break; 
            case ShopCategory.Vase:
                id = this.userData.currentVase;
                break; 
            case ShopCategory.Flower:
                id = this.userData.currentFlower;
                break; 
        }
        return id;
    }

    public bool IsExsistItem(string name)
    {
        return this.userData.itemList.Exists(i => i.name == name);
    }

    public PurchaseItemData FindItemMetaDataByName(string name)
    {
        return itemDataList.Find(l => l.Name == name);
    }

    public List<PurchaseItemData> FindItemMetaDataByCategory(ShopCategory category)
    {
        return itemDataList.FindAll(l => l.Category == category);
    }
    
    private List<PurchaseItemData> itemDataList = new List<PurchaseItemData>();    // item모아놓은 것

    private void LoadData()
    {
        this.itemDataList.Clear();
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Flower, 1, "Tutorial", 0));  
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Flower, 2, "Blue hydrangea", 160));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Flower, 3, "Pink hydrangea", 160));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Flower, 4, "Clematis Blue", 170));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Flower, 5, "Clematis Purple", 170));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Flower, 6, "Clematis White", 170));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Flower, 7, "Cherry Blossom", 200, true));

        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Vase, 11, "WhiteVase", 0));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Vase, 12, "GrayVase", 150));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Vase, 13, "BlackVase", 140));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Vase, 14, "BrownPot", 100));
        this.itemDataList.Add(new PurchaseItemData(ShopCategory.Vase, 15, "WhitePot", 120));

        // this.itemDataList.Add(new PurchaseItemData(ShopCategory.Pot, 21, "Brown Flower Pot", 0));
        
        // this.itemDataList.Add(new PurchaseItemData(ShopCategory.WateringCan, 31, "Watering Pot", 50));

        // this.itemDataList.Add(new PurchaseItemData(ShopCategory.PotUpgrade, 41, "Pot Upgrade", 100));
    }
}