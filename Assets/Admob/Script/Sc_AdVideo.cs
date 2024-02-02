using System;
using System.Collections;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class Sc_AdVideo : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    /// private float deltaTime = 0.0f;
    private static string outputMessage = string.Empty;

    string appId = "";
    string adVideo = "";


    [SerializeField] private bool TESTADS = true;

    [Header("ANDROID")]
    public string a_appId = "";
    public string a_adVideo = "";

    [Header("IOS")]
    public string i_appId = "";
    public string i_adVideo = "";

    public static string OutputMessage
    {
        set { outputMessage = value; }
    }
    public static Sc_AdVideo instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    public void Start()
    {
        if (TESTADS)
        {
#if UNITY_ANDROID
            appId = "ca-app-pub-3940256099942544~3347511713";
            adVideo = "ca-app-pub-3940256099942544/1712485313";
#endif
#if UNITY_IOS || UNITY_IPHONE
            appId = "ca-app-pub-3940256099942544~3347511713";
            adVideo = "ca-app-pub-3940256099942544/2934735716";
#endif
        }
        else
        {
#if UNITY_ANDROID
            appId = a_appId;
            adVideo = a_adVideo;
#endif
#if UNITY_IOS || UNITY_IPHONE
            appId = i_appId;
            adVideo = i_adVideo;
#endif
        }

        MobileAds.SetiOSAppPauseOnBackground(true);
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
        this.CreateAndLoadRewardedAd();
    }

    public void CreateAndLoadRewardedAd()
    {
        // Create new rewarded ad instance.
        this.rewardedAd = new RewardedAd(adVideo);

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

        // Create an empty ad request.
        AdRequest request = this.CreateAdRequest();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    #region RewardedAd callback handlers



    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");

        //ShowVideo();
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: " + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
       // PlayerController._instanse._isDeActiveDie = false;
        if (PlayerController._instanse._isDeActiveDie)
            PlayerController._instanse.SuccessRievive();
        else
            PlayerController._instanse.Die();


        this.CreateAndLoadRewardedAd();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
            + amount.ToString() + " " + type);

        PlayerController._instanse._isDeActiveDie = true;
        //Add Your code :)

        this.CreateAndLoadRewardedAd();
    }

    #endregion

    private AdRequest CreateAdRequest()
    {
        /*return new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)
            .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
            .AddKeyword("game")
            .SetGender(Gender.Male)
            .SetBirthday(new DateTime(1985, 1, 1))
            .TagForChildDirectedTreatment(false)
            .AddExtra("color_bg", "9B30FF")
            .Build();*/

        return new AdRequest.Builder().Build();
    }

    public void ShowVideo()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            MonoBehaviour.print("Rewarded ad is not ready yet");
        }
    }
}
