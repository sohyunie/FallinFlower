using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameOverPop : UEPopup
{
    public static GameOverPop Instance;
    public static GameOverPop ShowPop(int count)
    {
        Instance = UEPopup.GetInstantiateComponent<GameOverPop>();
        Instance.ShowPopUp();
        Instance.Init(count);
        return Instance;
    }

    public void Init(int count)
    {
        this.ContinueBtn.SetActive(!InGameManager.Instance.IsKeepGoing && UnityEngine.Random.Range(0.0f, 1.0f) < 0.33f);
        
        if(count % 10 == 0)
        {
            if(UnityEngine.Random.Range(0.0f, 1.0f) < 0.2f)
                UnityAdsHelper.Instance.ShowRewardedAd();
        }

        this.count = count;
        DataManager.Instance.AddCoin(this.count);
        this.text.text = count.ToString();
        this.flowerText.text = count.ToString();
        this.topText.text = DataManager.Instance.UserData.coin.ToString();
        GameUI.Instance.Refresh();
        this.grade1Obj.SetActive(this.count < 30);
        this.grade2Obj.SetActive(this.count >= 30 && this.count < 100);
        this.grade3Obj.SetActive(this.count >= 100 && this.count < 200);
    }

    public void OnClickMoveRestart()
    {
        SoundManager.Instance.PlaySFXSound(SFX.uiTouch);
        if(UnityEngine.Random.Range(0.0f, 1.0f) < 0.5f) // 재시작
            UnityAdsHelper.Instance.ShowRewardedAd();
        InGameManager.Instance.InitGame();
        GameObject.Destroy(this.gameObject); 
    }

    public void OnClickMoveLobby()
    {
        SoundManager.Instance.PlaySFXSound(SFX.uiTouch);
        InGameManager.Instance.DeleteFlower();
        GameUI.Instance.SetGameUI(true);
        GameObject.Destroy(this.gameObject); 
    }

    public void OnKeepGoing()   // 이어서 -> 무조건 광고 보도록
    {
        SoundManager.Instance.PlaySFXSound(SFX.uiTouch);
        UnityAdsHelper.Instance.ShowRewardedAd();
        InGameManager.Instance.InitGame(true);
        GameObject.Destroy(this.gameObject); 
    }
    [SerializeField] private GameObject grade1Obj;
    [SerializeField] private GameObject grade2Obj;
    [SerializeField] private GameObject grade3Obj;
    [SerializeField] private Text text;
    [SerializeField] private Text flowerText;
    [SerializeField] private Text topText;

    [SerializeField] private GameObject ContinueBtn;

    private int count;
}
