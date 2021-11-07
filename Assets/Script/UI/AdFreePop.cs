using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AdFreePop : UEPopup
{
    public static AdFreePop Instance;
    public static AdFreePop ShowPop()
    {
        Instance = UEPopup.GetInstantiateComponent<AdFreePop>();
        Instance.ShowPopUp();
        Instance.Init();
        return Instance;
    }

    public void OnClickBuy()
    {
        DataManager.Instance.UserData.isADFree = true;
        DataManager.Instance.SaveJsonData(DataManager.Instance.UserData);
        GameUI.Instance.Refresh();
        this.DestroyPopUp();
    }

    private void Init()
    {
        this.priceText.text = (500).ToString("n0");
    }

    [SerializeField] private Text priceText;
}
