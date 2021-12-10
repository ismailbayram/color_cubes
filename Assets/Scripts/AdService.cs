using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdService : MonoBehaviour, IUnityAdsListener  {
    public Button rewardButton;

    #if UNITY_IOS
    private string gameId = "3928640";
    #elif UNITY_ANDROID
    private string gameId = "3928641";
    #endif
    private bool testMode = false;

    private string nonRewardedPlacement = "video"; 
    private string rewardedPlacement = "rewardedVideo"; 
    private string bannerPlacement = "top_banner"; 

    void Start () {
        if (this.rewardButton != null) {
            this.rewardButton.onClick.AddListener(this.DisplayRewardedAd);
            // this.rewardButton.gameObject.SetActive(false);
        }
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        StartCoroutine(ShowBannerWhenReady());
    }

    public void DisplayNonRewardedAd() {
        Advertisement.Show(this.nonRewardedPlacement);
    }

    public void DisplayRewardedAd() {
        Advertisement.Banner.Hide();
        Advertisement.Show(this.rewardedPlacement);
    }

    IEnumerator ShowBannerWhenReady () {
        while (!Advertisement.IsReady(this.bannerPlacement)) {
            yield return new WaitForSeconds (0.5f);
        }
        Advertisement.Banner.Show(this.bannerPlacement);
    }

    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished) {
            if (!this.rewardButton)
                return;
            GameManager.instance.RewardPlayer();
            this.rewardButton.transform.gameObject.SetActive(false);
            StartCoroutine(ShowBannerWhenReady());
        } else if (showResult == ShowResult.Skipped) {
            Debug.Log("User skipped the ad.");
        } else if (showResult == ShowResult.Failed) {
            Debug.LogWarning ("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady (string placementId) {
        if (placementId == this.rewardedPlacement && this.rewardButton != null) {
            // this.rewardButton.gameObject.SetActive(true);
            this.rewardButton.interactable = true;
        }
    }

    public void OnUnityAdsDidError(string message) {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId) {
        // Optional actions to take when the end-users triggers an ad.
    } 
}
