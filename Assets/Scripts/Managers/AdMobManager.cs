using GoogleMobileAds;
using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdMobManager : MonoBehaviour
{
#if UNITY_ANDROID
    string adUnitId = "ca-app-pub-4348469999914971/7491934449";
#else
    string adUnitId = "unused";
#endif

    BannerView bannerView;

    private void Awake()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) => { });

    }

    void CreateBannerView()
    {
        print("Creating banner view");

        if (bannerView != null)
        {
            DestroyAd();
        }

        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Center);
    }

    public void LoadAd()
    {
        if (bannerView == null)
        {
            CreateBannerView();
        }

        AdRequest adRequest = new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();

        print("Loading banner Ad.");
        bannerView.LoadAd(adRequest);
    }

    public void DestroyAd()
    {
        if (bannerView != null)
        {
            print("Destroying banner Ad.");
            bannerView.Destroy();
            bannerView = null;
        }
    }
}
