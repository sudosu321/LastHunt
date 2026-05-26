using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AmmoAd : MonoBehaviour
{
    // Test ID - use this first to confirm it works!
    // Replace with your real ID after testing:
    // ca-app-pub-8271564870750045/1802206839
    private string adUnitId = "ca-app-pub-3940256099942544/5224354917"; // test ID

    private RewardedAd rewardedAd;

    public GunShoot ammoHandler;

    void Start()
    {
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob Initialized!");
            LoadAd();
        });
    }

    void LoadAd()
    {
        // Clean up old ad
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading rewarded ad...");

        var adRequest = new AdRequest();

        RewardedAd.Load(adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.Log("LOAD FAILED: " + error);
                return;
            }

            Debug.Log("AD LOADED!");
            rewardedAd = ad;

            RegisterEvents(rewardedAd);
        });
    }

    void RegisterEvents(RewardedAd ad)
    {
        ad.OnAdFullScreenContentOpened += () => Debug.Log("AD OPENED");
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("AD CLOSED - Reloading...");
            LoadAd();
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.Log("AD FAILED: " + error);
            LoadAd();
        };
    }

    // BUTTON PRESS
    public void WatchAdForAmmo()
    {
        Debug.Log("BUTTON PRESSED");

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                ammoHandler.bulletCount += 1;
                ammoHandler.updateText();
                Debug.Log("1 AMMO ADDED!");
            });
        }
        else
        {
            Debug.Log("AD NOT READY - Reloading...");
            LoadAd();
        }
    }
}