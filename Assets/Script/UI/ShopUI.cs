using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public enum ShopCategory
{
    //WateringCan,   // 물뿌리개 
    Flower,
    Vase,   // 꽃병
    //Pot,    // 화분
    //PotUpgrade // 화분 업그레이드
}

public class ShopUI : MonoBehaviour
{
    public void OnClickClose()
    {
        SoundManager.Instance.PlaySFXSound(SFX.uiTouch);
        this.gameObject.SetActive(false);
    }

    public void OnClickLeftButton()
    {
        SoundManager.Instance.PlaySFXSound(SFX.uiTouch);
        this.currentCategory = ((int)this.currentCategory > 0) ? this.currentCategory - 1 : ShopCategory.Vase;
        this.ChangeCategory(this.currentCategory);
    }

    public void OnClickRightButton()
    {
        SoundManager.Instance.PlaySFXSound(SFX.uiTouch);
        this.currentCategory = ((int)this.currentCategory <1) ? this.currentCategory + 1: ShopCategory.Flower;
        this.ChangeCategory(this.currentCategory);
    }

    public void Init()
    {
        this.currentCategory = ShopCategory.Flower;
        this.ChangeCategory(this.currentCategory);
    }

    [SerializeField] private ShopCategory currentCategory;
    [SerializeField] private ShopItem shopItemPrefab;
    [SerializeField] private GameObject content; 
    [SerializeField] private Text categoryText; 

    private void ChangeCategory(ShopCategory category)
    {
        this.categoryText.text = category.ToString();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        this.SetItem(this.currentCategory);
    }

    private void SetItem(ShopCategory category)
    {
        List<PurchaseItemData> list = DataManager.Instance.FindItemMetaDataByCategory(category);
        Debug.Log(list.Count);
        if (list != null && list.Count != 0)
        {
            foreach (PurchaseItemData data in list) 
            { 
                ShopItem item = Instantiate<ShopItem>(this.shopItemPrefab, this.content.transform);
                item.SetItem(data, (isBuy) =>
                {
                    this.ChangeCategory(this.currentCategory);
                    GameUI.Instance.Refresh();
                });
            }
        }
    }
}
