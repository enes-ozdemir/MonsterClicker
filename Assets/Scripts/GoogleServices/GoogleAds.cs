using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class GoogleAds : MonoBehaviour
{
    private RewardedAd _rewardedAd;
    private readonly string _adUnitId = "ca-app-pub-9430454817447319/6316670378";
    [SerializeField] private ScreenManager screenObject;

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        _rewardedAd = new RewardedAd(_adUnitId);

        AdRequest request = new AdRequest.Builder().Build();

        _rewardedAd.LoadAd(request);

        _rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        _rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        _rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        _rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;
    }

    private void HandleUserEarnedReward(object sender, Reward args)
    {
        Debug.Log("GoToGameScreen with reward");
        screenObject.GoToGameScreen(true);
    }

    public void UserChoseToWatchAd()
    {
        Debug.Log("UserChoseToWatchAd");
        if (_rewardedAd.IsLoaded())
        {
            screenObject.menuScreen.SetActive(false);
            _rewardedAd.Show();
            screenObject.menuScreen.SetActive(false);
            screenObject.GoToGameScreen(true);
        }
    }

    private void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdLoaded");
    }

    private void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("HandleRewardedAdFailedToLoad" + args.LoadAdError);
    }

    private void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdOpening");
    }

    private void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        Debug.Log("HandleRewardedAdFailedToShow" + args.Message);
    }

    private void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdClosed");
    }
}