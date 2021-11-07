using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TopUI : MonoBehaviour
{
    public void Init()
    {
        this.Refresh();
    }

    public void Refresh()
    {
        this.userCoin.text = DataManager.Instance.UserData.coin.ToString();
    }

    public void OnClickSetupBtn()
    {
        // SoundManager.Instance.PlaySFXSound(SFX.click2);
        SoundManager.Instance.PlaySFXSound(SFX.uiTouch);
        SetUpPop.ShowPop();
    }

    // [SerializeField] GameObject setUpBtn;
    [SerializeField] private Text userCoin;
}
