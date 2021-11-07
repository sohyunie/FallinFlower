using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopItem : MonoBehaviour
{
    public void SetItem(PurchaseItemData data, Action<bool> isBuy)
    {
        this.onRefresh = isBuy;
        this.data = data;
        this.itemName.text = data.Name; 
        this.price.text = data.Price.ToString();
        this.image.sprite = ImageManager.Instance.GetShopIcon(data.Category, data.ID);
        PurchaseData purchaseData = DataManager.Instance.UserData.itemList.Find(i => this.data.Name.Equals(i.name));

        this.lockedObj.SetActive(purchaseData == null && this.data.IsAdLock);
        if((data.Category == ShopCategory.Flower || data.Category == ShopCategory.Vase))
        {
            this.countText.gameObject.SetActive(false);
            this.selectedObj.SetActive(this.data.ID.Equals(DataManager.Instance.GetItemByCategory(data.Category)));
            this.selectedBtn.SetActive(DataManager.Instance.IsExsistItem(data.Name));
        } 
        else
        {
            this.selectedObj.SetActive(false);
            this.selectedBtn.SetActive(false);
            // if(data.Category == ShopCategory.WateringCan)
            //     this.countText.text = "Count : ";
            // else if (data.Category == ShopCategory.PotUpgrade)
            //     this.countText.text = "Upgrade : ";

            this.countText.text += (purchaseData == null) ? "0" : purchaseData.count.ToString();
        }
    }

    public void OnClickBuy()
    {
        // if(this.data.Category == ShopCategory.PotUpgrade)
        // {
        //     PurchaseData purchaseData = DataManager.Instance.UserData.itemList.Find(i => this.data.Name.Equals(i.name));
        //     if(purchaseData != null && purchaseData.count >= 9)
        //     {
        //         //[TODO] 구매 불가능한거를 알려줘야하긴 할거 같음.
        //         return;
        //     }
        // }

        if (DataManager.Instance.UseCoin(this.data.Price))
        {
            DataManager.Instance.PurchaseItem(this.data.Name, this.data.Category);
            SoundManager.Instance.PlaySFXSound(SFX.buy);
            this.onRefresh(true);
        }
    }

    public void OnClickUnLock()
    {
        SoundManager.Instance.PlaySFXSound(SFX.uiTouch);
        UnityAdsHelper.Instance.ShowRewardedAd();

        DataManager.Instance.PurchaseItem(this.data.Name, this.data.Category);
        SoundManager.Instance.PlaySFXSound(SFX.buy);
        this.onRefresh(true);
    }

    public void OnClickSelect()
    {
        SoundManager.Instance.PlaySFXSound(SFX.uiTouch);
        DataManager.Instance.SetItemByCategory(this.data.Category, this.data.ID);
        this.onRefresh(true);
    }

    [SerializeField] private GameObject selectedObj;
    [SerializeField] private GameObject selectedBtn;
    [SerializeField] private GameObject lockedObj;
    [SerializeField] private Text itemName;
    [SerializeField] private Text price;
    [SerializeField] private Text countText;
    [SerializeField] private Image image;

    private PurchaseItemData data;
    private Action<bool> onRefresh;
}
