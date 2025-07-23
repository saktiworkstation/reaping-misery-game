using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
#if UNITY_IOS
    private string gameId = "4848352";
#else
    private string gameId = "4848353";
#endif

    private string interstitialAdId = "video";
    private string rewardedAdId = "rewardedVideo";
    private string bannerAdId = "banner";

    Action onRewardedAdSuccess;

    void Start()
    {
        Advertisement.Initialize(gameId, true);
        LoadInterstitialAd();
        LoadRewardedAd();
        LoadBannerAd();
    }

    public void LoadInterstitialAd()
    {
        Advertisement.Load(interstitialAdId, this);
    }

    public void LoadRewardedAd()
    {
        Advertisement.Load(rewardedAdId, this);
    }

    public void LoadBannerAd()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(bannerAdId);
        StartCoroutine(ShowBannerWhenReady());
    }

    public void ShowBanner()
    {
        if (!Advertisement.isInitialized)
        {
            Debug.Log("Advertisement not initialized yet!");
            return;
        }

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(bannerAdId);
    }

    IEnumerator ShowBannerWhenReady()
    {
        yield return new WaitForSeconds(1f);
        Advertisement.Banner.Show(bannerAdId);
    }

    public void PlayAd()
    {
        Advertisement.Show(interstitialAdId, this);
    }

    public void PlayRewardedAds(Action onSuccess)
    {
        onRewardedAdSuccess = onSuccess;
        Advertisement.Show(rewardedAdId, this);
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    // Implementasi dari IUnityAdsLoadListener
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad Loaded: " + placementId);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Ad Failed to Load: {placementId} - Error: {error.ToString()} - Message: {message}");
    }

    // Implementasi dari IUnityAdsShowListener
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Ad Show Failed: {placementId} - Error: {error.ToString()} - Message: {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Ad Started: " + placementId);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Ad Clicked: " + placementId);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Ad Completed: " + placementId);

        if (placementId == rewardedAdId && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            onRewardedAdSuccess?.Invoke();
        }

        // Load ulang ad setelah ditampilkan
        if (placementId == rewardedAdId)
            LoadRewardedAd();
        else if (placementId == interstitialAdId)
            LoadInterstitialAd();
    }
}
