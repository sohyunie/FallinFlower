using System.Diagnostics;
using System.Runtime.CompilerServices;
using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIPageName
{
    Main
};

public class GameUI : Singleton<GameUI>
{
   // public UIPageName CurrentPage => this.currentPage;
    public void Refresh()
    {
        this.heartText.text = InGameManager.Instance.Heart.ToString();
        this.leftText.text = InGameManager.Instance.GetLeftFlower();
        this.adBtn.SetActive(!DataManager.Instance.UserData.isADFree);
        this.topUI.Refresh();

        //if (this.gameUI.gameObject.activeSelf) this.gameUI.Refresh();
    }  

    public void OnClickStartBtn()
    {
        SoundManager.Instance.PlaySFXSound(SFX.uiTouch);
        InGameManager.Instance.InitGame();
    }

    public void OnClickMoveLobby()
    {
        SoundManager.Instance.PlaySFXSound(SFX.uiTouch);
        this.SetGameUI(true);
        // SoundManager.Instance.StopBGMSound();
        InGameManager.Instance.ForceDie();
    }


    public void SetGameUI(bool isLobby)
    {
        this.lobbyUIObj.SetActive(isLobby);
        this.ingameUIObj.SetActive(!isLobby);
    }

    public void OnClickShop()
    {
        SoundManager.Instance.PlaySFXSound(SFX.uiTouch);
        this.shopUI.gameObject.SetActive(true);
        this.shopUI.Init();
    }

    public void OnClickSetUp()
    {
        SoundManager.Instance.PlaySFXSound(SFX.uiTouch);
        SetUpPop.ShowPop();
        this.shopUI.Init();
    }

    public void OnClickAdFreePop()
    {
        SoundManager.Instance.PlaySFXSound(SFX.uiTouch);
        AdFreePop.ShowPop();
    }

    public void OnClickTutorial()
    {
        SoundManager.Instance.PlaySFXSound(SFX.uiTouch);
        TutorialPop.ShowPop();
    }
    public void InitGameUI()
    {
        this.animator.SetTrigger("Action");
        this.windowAnimator.SetTrigger("Action");
        this.lobbyUIObj.SetActive(false);
        this.ingameUIObj.SetActive(true);
    }

    // public void CloseWindow()
    // {   
    //     this.windowAnimator.SetTrigger("ActionClose");
    //     SoundManager.Instance.StopBGMSound();
    // }


    public void MoveUIPage(UIPageName name)
    {
        this.currentPage = name;
        this.gameUI.gameObject.SetActive(name == UIPageName.Main);
        this.Refresh();
    }
    
    protected void Awake()
    {
        if(DataManager.Instance == null)
            SceneLoadManager.MoveScene(Scene.SplashScene);
        else
            SoundManager.Instance.PlayBGMSound(BGM.main);
    }

    protected void Start()
    {
        this.lobbyUIObj.SetActive(true);
        this.ingameUIObj.SetActive(false);
        this.adBtn.SetActive(!DataManager.Instance.UserData.isADFree);
        this.topUI.Refresh();
        //////////////////////////////////////////
        //  if (SceneLoadManager.Instance == null)
        // {
        //     SceneLoadManager.MoveScene(Scene.SplashScene);
        // }
        // else
        // {
        //     this.MoveUIPage(UIPageName.Main);
        //     // this.Init();
        // }
    }

    [SerializeField] private GameObject ingameUIObj;
    [SerializeField] private GameObject lobbyUIObj;
    [SerializeField] private GameObject adBtn;


    [SerializeField] private Animator animator;
    [SerializeField] private Animator windowAnimator;
    [SerializeField] private Text leftText;
    [SerializeField] private Text heartText;
    [SerializeField] private TopUI topUI;

    
    private UIPageName currentPage;
    
   [SerializeField] private GameUI gameUI;
   [SerializeField] private ShopUI shopUI;
}
