using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;

public class AdmobManager : Singleton<AdmobManager>
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    private bool isWatchedVideoComplete;
    private Action WatchedVideoCompleteAction { get; set; }


    private void Start()
    {
        //this.InitAdmob();
    }

    private void InitAdmob()
    {
        string appId = "";
#if UNITY_ANDROID
        appId = ConfigManager.Instance.admobConfig.AdmobAppID_Android;
#elif UNITY_IOS
        appId = ConfigManager.Instance.admobConfig.AdmobAppID_iOS;
#else
        appId = "unexpected_platform";
#endif
        MobileAds.Initialize(initStatus =>
        {

        });


        if (PlayerPrefs.GetInt("purchased") != 1)
        {
            this.RequestBanner();
            this.RequestInterstitial();
        }
        this.RequestRewardBasedVideo();
    }

    public void ShowBannerAd()
    {
        if (this.bannerView != null)
        {
            if (PlayerPrefs.GetInt("purchased") != 1)
            {
                this.bannerView.Show();
                Debug.Log("Show Banner");
            }
        }
    }

    public void HideBannerAd()
    {
        if (PlayerPrefs.GetInt("purchased") != 1)
        {
            if (this.bannerView != null)
            {
                this.bannerView.Hide();
            }
        }
    }

    public void ShowInterstitialAd()
    {
        if (PlayerPrefs.GetInt("purchased") != 1)
        {
            if (this.interstitial != null)
            {
                if (this.interstitial.IsLoaded())
                {
                    this.interstitial.Show();
                    Debug.Log("Show Interstitial Ad");
                }
            }
        }
    }

    public void ShowVideoReward(Action action)
    {
        this.isWatchedVideoComplete = false;
        this.WatchedVideoCompleteAction = action;
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    #region REQUEST ADS
    private void RequestBanner()
    {
        string adUnitId = "";
#if UNITY_ANDROID
        adUnitId = ConfigManager.Instance.admobConfig.AdmobBannerID_Android;
#elif UNITY_IOS
        adUnitId = ConfigManager.Instance.admobConfig.AdmobBannerID_iOS;
#else
        adUnitId = "unexpected_platform";
#endif
        // Clean up banner ad before creating a new one
        if (this.bannerView != null)
        {
            this.bannerView.Destroy();
        }

        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
    }

    private void RequestInterstitial()
    {
        string adUnitId = "";
#if UNITY_ANDROID
        adUnitId = ConfigManager.Instance.admobConfig.AdmobInterstitialID_Android;
#elif UNITY_IOS
        adUnitId = ConfigManager.Instance.admobConfig.AdmobInterstitialID_iOS;
#else
        adUnitId = "unexpected_platform";
#endif

        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleInterstitialLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleInterstitialOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleInterstitialClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleInterstitialLeftApplication;

        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }

    private void RequestRewardBasedVideo()
    {
        string adUnitId = "";
#if UNITY_ANDROID
        adUnitId = ConfigManager.Instance.admobConfig.AdmobRewardVideoID_Android;
#elif UNITY_IOS
        adUnitId = ConfigManager.Instance.admobConfig.AdmobRewardVideoID_iOS;
#else
        adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }
    #endregion

    #region BANNER AD HANDLER
    public void HandleAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }
    #endregion

    #region INTERSTITIAL AD HANDLER
    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        this.RequestInterstitial();
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLeftApplication event received");
    }
    #endregion

    #region REWARD AD HANDLER
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        if (this.isWatchedVideoComplete == true && this.WatchedVideoCompleteAction != null)
        {
            this.WatchedVideoCompleteAction();
            this.isWatchedVideoComplete = false;
            this.WatchedVideoCompleteAction = null;
        }
        this.RequestRewardBasedVideo();

        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;

        this.isWatchedVideoComplete = true;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
    }
    #endregion
}
