using UnityEngine;
using System;
using UnityEngine.Advertisements;
 
public class UnityAdsHelper : Singleton<UnityAdsHelper>
{
    private const string android_game_id = "4262883";
    private const string ios_game_id = "4262882";
 
    private const string rewarded_video_id = "Rewarded_Android";
 
    void Start()
    {
        Initialize();
    }
 
    private void Initialize()
    {
#if UNITY_ANDROID
        Advertisement.Initialize(android_game_id);
#elif UNITY_IOS
        Advertisement.Initialize(ios_game_id);
#endif
    }
 
    public void ShowRewardedAd(Action<bool> onRewarded = null)
    {
        Debug.Log("Show Rewarded Ad");
        if(DataManager.Instance.UserData.isADFree)
            return;
        if (Advertisement.IsReady(rewarded_video_id))
        {
            Debug.Log("Ad is ready");
            ShowOptions options = new ShowOptions { resultCallback = HandleShowResult };

 
            Advertisement.Show(rewarded_video_id, options);
        }
    }
 
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                {
                    Debug.Log("The ad was successfully shown.");
 
                    // to do ...
                    // 광고 시청이 완료되었을 때 처리
 
                    break;
                }
            case ShowResult.Skipped:
                {
                    Debug.Log("The ad was skipped before reaching the end.");
 
                    // to do ...
                    // 광고가 스킵되었을 때 처리
 
                    break;
                }
            case ShowResult.Failed:
                {
                    Debug.LogError("The ad failed to be shown.");
 
                    // to do ...
                    // 광고 시청에 실패했을 때 처리
 
                    break;
                }
        }
    }
}