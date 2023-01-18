using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

public class AdMobManager : MonoBehaviour
{
    MyObject myChar;
    [SerializeField]
    private RewardedAd rewardBasedVideo;

    public GameObject Clear_AD_Btn;
    public GameObject Fail_AD_Btn;
    public GameObject Resurrection_AD_Btn;

    public string appId_Android;
    public string appId_iOS;


    public string bannerId_Android;
    public string bannerId_iOS;

    public string interstitialId_Android;
    public string interstitialId_iOS;

    //public string rewardVideoId_Android;
    //Real보상
    const string rewardVideoId_Android = "ca-app-pub-1549075837919908/7455723777";

    //테스트보상
    //const string rewardVideoId_Android = "ca-app-pub-3940256099942544/5224354917";
    public string rewardVideoId_iOS;
    public int interstitialTime;

    private int AdNum;

    private bool adComplete;

    //public GameManager instance;
    //public TitleManager titleManager;
    private void Awake()
    {
        myChar = MyObject.MyChar;
        Init();
        //titleManager = TitleManager.instance;

    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "LobbiScene")
        {
            if (myChar.AD_Enought_Check)
            {
                if (rewardBasedVideo.IsLoaded())
                {
                    myChar.AD_Enought_Check = false;
                    rewardBasedVideo.Show();
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "StageScene")
        {
            if (myChar.AD_Enought_Check)
            {
                if (rewardBasedVideo.IsLoaded())
                {
                    myChar.AD_Enought_Check = false;
                    //rewardBasedVideo.Show();
                }
            }
        }
    }
    public void Init() // 이것도뭔지모르겟다
    {
        //#if UNITY_ANDROID
        //        MobileAds.Initialize(appId_Android);
        //#elif UNITY_IOS
        //        MobileAds.Initialize(appId_iOS);
        //#endif       

        StartCoroutine(LateInit());
    }

    IEnumerator LateInit()
    {
        yield return new WaitForSeconds(0.6f);

        //BannerInit();
        //InterstitialInit();
        RewardVideoInit();
        yield return new WaitForSeconds(1f);
        //Banner();

        StartCoroutine(SecondUpdate());
    }

    WaitForSeconds secondDelay = new WaitForSeconds(1);
    public int secondCount;
    IEnumerator SecondUpdate()
    {
        secondCount = 0;
        while (true)
        {
            secondCount++;
            yield return secondDelay;
        }
    }
    private BannerView bannerView;
    public void BannerInit()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        bannerView = new BannerView(bannerId_Android, AdSize.SmartBanner, AdPosition.Bottom);
#elif UNITY_IOS
        bannerView = new BannerView(bannerId_iOS, AdSize.SmartBanner, AdPosition.Bottom);
#endif
    }
    public void Banner()
    {
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }
    public void DestroyBanner()
    {
        bannerView.Destroy();
    }

    private InterstitialAd interstitial;
    private void InterstitialInit()
    {
        LoadInterstitial();
    }

    void LoadInterstitial()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        interstitial = new InterstitialAd(interstitialId_Android);
#elif UNITY_IOS
        interstitial = new InterstitialAd(interstitialId_iOS);
#endif
        // Called when an ad request has successfully loaded.
        interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        //interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad; 이거는 지금못찾겠다
        // Called when an ad is shown.
        interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        //interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    public void Interstitial()
    {
        secondCount += 5;
        if (interstitial.IsLoaded())
        {
            if (secondCount > interstitialTime)
            {
                interstitial.Show();
                secondCount = 0;
            }
        }
        else
        {
            LoadInterstitial();
        }
    }


    //private RewardBasedVideoAd rewardBasedVideo;
    //private RewardedAd rewardBasedVideo;
    void RewardVideoInit()
    {
        //rewardBasedVideo = RewardBasedVideoAd.Instance;
        //AdRequest request = new AdRequest.Builder().Build();
#if UNITY_ANDROID || UNITY_EDITOR
        rewardBasedVideo = new RewardedAd(rewardVideoId_Android);
#elif UNITY_IOS
        rewardBasedVideo = new RewardedAd(rewardVideoId_iOS);
#endif



        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        //rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        rewardBasedVideo.OnAdFailedToShow += HandleOnAdFailedToLoad;
        // Called when the user should be rewarded for watching a video.
        //rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        rewardBasedVideo.OnUserEarnedReward += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        //rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        AdRequest request = new AdRequest.Builder().Build();

#if UNITY_ANDROID || UNITY_EDITOR
        rewardBasedVideo.LoadAd(request);
#elif UNITY_IOS
        rewardBasedVideo.LoadAd(request);
#endif
    }

    public void LoadRewardVideo()
    {
        Debug.Log("광고 : " + rewardBasedVideo.IsLoaded());
        AdRequest request = new AdRequest.Builder().Build();
#if UNITY_ANDROID || UNITY_EDITOR
        rewardBasedVideo.LoadAd(request);
        //rewardBasedVideo.LoadAd(request, rewardVideoId_Android);
#elif UNITY_IOS
        //rewardBasedVideo.LoadAd(request, rewardVideoId_iOS);
        rewardBasedVideo.LoadAd(request);
#endif
    }
    public void RewardVideo(int num)
    {
        // 광고제거 구매하면 이런식
        if (myChar.ADClearCheck)
        {
            switch (num)
            {
                case 0:     //슬롯머신
                    SlotMachineManager.Instance.StartSlot(0);
                    break;
                case 1:     //클리어 배수보상;
                    Clear_AD_Btn.SetActive(false);
                    myChar.MultipleCheck = true;
                    myChar.Gain_Multiple = 2;
                    GameManager.Instance.ClearPlusEffect();
                    FirebaseManager.firebaseManager.ResultWatchAD("Clear");
                    break;
                case 2:     //실패 배수보상
                    Fail_AD_Btn.SetActive(false);
                    myChar.MultipleCheck = true;
                    myChar.Gain_Multiple = 2;
                    GameManager.Instance.FailPlusEffect();
                    FirebaseManager.firebaseManager.ResultWatchAD("Fail");
                    break;
                case 3:
                    GameManager.Instance.AdMerchin.GetComponent<BossGiftBoxScript>().OpenBox();
                    GameManager.Instance.AdMerchin.GetComponent<BossGiftBoxScript>().WindowCheck = true;
                    if (!myChar.Tutorial)
                    {
                        StartCoroutine(RoomManager.Instance.GiftBoxOpen_DoorInvisible());
                    }
                    FirebaseManager.firebaseManager.IngameTreasureChestOpen("AD_GiftBox", 0);
                    break;
                case 10:    //Admerchin보상
                    GameManager.Instance.ADUse();
                    break;
                case 11:    //부활
                    GameManager.Instance.ResurrectionBtn(0);
                    Resurrection_AD_Btn.SetActive(false);
                    break;
                case 50:    //대장간광고
                    LobbiManager.instance.ForgeSkip_Btn(0);
                    break;
                case 100:   //장비뽑기
                    LobbiManager.instance.AdCostCheck(100);
                    FirebaseManager.firebaseManager.ShopProductPurchase("AD_Equipment");
                    //LobbiManager.instance.GachaGemCheck(11);
                    break;
                case 200:   //보석뽑기
                    LobbiManager.instance.AdCostCheck(200);
                    FirebaseManager.firebaseManager.ShopProductPurchase("AD_Dia");
                    break;
                case 201:   //soulspark뽑기
                    LobbiManager.instance.AdCostCheck(201);
                    FirebaseManager.firebaseManager.ShopProductPurchase("AD_SoulFlame");
                    break;
                case 202:   //heroheart뽑기
                    LobbiManager.instance.AdCostCheck(202);
                    FirebaseManager.firebaseManager.ShopProductPurchase("AD_HeroToken");
                    break;
            }
        }
        else
        {
            if (rewardBasedVideo.IsLoaded())
            {
                AdNum = num;
                rewardBasedVideo.Show();
                adComplete = false;
                Debug.Log("스타트 : " + adComplete);
            }
            else
            {
                LoadRewardVideo();
                AdNum = num;
                if (SceneManager.GetActiveScene().name == "LobbiScene")
                {
                    myChar.AD_Enought_Check = true;
                    myChar.AD_Road_Time = 7f;
                    //StartCoroutine(LobbiManager.instance.Not_Enough_AD());
                }
                else if (SceneManager.GetActiveScene().name == "StageScene")
                {
                    myChar.AD_Enought_Check = true;
                    myChar.AD_Road_Time = 7f;
                }
            }
        }
        //if (rewardBasedVideo.IsLoaded())
        //{
        //    AdNum = num;
        //    rewardBasedVideo.Show();
        //}
        //else
        //{
        //    LoadRewardVideo();
        //    Debug.Log(22);
        //}
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.AdError);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        LoadInterstitial();
        myChar.AD_Enought_Check = false;
        LoadRewardVideo();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    //광고 로드됐을때
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
        myChar.AD_FailedToLoad = false;
    }

    //광고 로드 안됐을때
    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        if (SceneManager.GetActiveScene().name == "LobbiScene")
        {
            //StartCoroutine(LobbiManager.instance.Not_Enough_AD());
        }
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.LoadAdError);
        myChar.AD_FailedToLoad = true;
        myChar.AD_Num = AdNum;
    }

    //광고 열렸을때
    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        Debug.Log("close");
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
        Debug.Log("adComplete : " + adComplete);
        if (adComplete)
        {
            // 여기서 보상을 줘야함
            switch (AdNum)
            {
                case 0:     //슬롯머신
                    SlotMachineManager.Instance.StartSlot(0);
                    break;
                case 1:     //클리어 배수보상;
                    Clear_AD_Btn.SetActive(false);
                    myChar.MultipleCheck = true;
                    myChar.Gain_Multiple = 2;
                    GameManager.Instance.ClearPlusEffect();
                    FirebaseManager.firebaseManager.ResultWatchAD("Clear");
                    break;
                case 2:     //실패 배수보상
                    Fail_AD_Btn.SetActive(false);
                    myChar.MultipleCheck = true;
                    myChar.Gain_Multiple = 2;
                    GameManager.Instance.FailPlusEffect();
                    FirebaseManager.firebaseManager.ResultWatchAD("Fail");
                    break;
                case 3:
                    GameManager.Instance.AdMerchin.GetComponent<BossGiftBoxScript>().OpenBox();
                    GameManager.Instance.AdMerchin.GetComponent<BossGiftBoxScript>().WindowCheck = true;
                    if (!myChar.Tutorial)
                    {
                        StartCoroutine(RoomManager.Instance.GiftBoxOpen_DoorInvisible());
                    }
                    FirebaseManager.firebaseManager.IngameTreasureChestOpen("AD_GiftBox", 0);
                    break;
                case 10:    //Admerchin보상
                    GameManager.Instance.ADUse();
                    break;
                case 11:    //부활
                    GameManager.Instance.ResurrectionBtn(0);
                    Resurrection_AD_Btn.SetActive(false);
                    break;
                case 50:    //대장간광고
                    LobbiManager.instance.ForgeSkip_Btn(0);
                    break;
                case 100:   //장비뽑기
                    LobbiManager.instance.AdCostCheck(100);
                    FirebaseManager.firebaseManager.ShopProductPurchase("AD_Equipment");
                    break;
                case 200:   //보석뽑기
                    LobbiManager.instance.AdCostCheck(200);
                    FirebaseManager.firebaseManager.ShopProductPurchase("AD_Dia");
                    break;
                case 201:   //soulspark뽑기
                    LobbiManager.instance.AdCostCheck(201);
                    FirebaseManager.firebaseManager.ShopProductPurchase("AD_SoulFlame");
                    break;
                case 202:   //heroheart뽑기
                    LobbiManager.instance.AdCostCheck(202);
                    FirebaseManager.firebaseManager.ShopProductPurchase("AD_HeroToken");
                    break;
            }
            LoadRewardVideo();
        }
        //adComplete = false;
    }
    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        // 여기서는 광고 완료되었는지 만 체크
        adComplete = true;
        myChar.AD_Enought_Check = false;

        Debug.Log("reward : " + adComplete);
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

}

