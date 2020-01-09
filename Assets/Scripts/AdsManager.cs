using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// add some library
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class AdsManager : MonoBehaviour
{
    // public Text rewardText;

    private int rewardCount = 0;

    // inisialization GameId
    private string GameId = "3425136";

    string placementId_video = "video";
    string placementId_rewardVideo = "rewardedVideo";

    private bool testMode = true;

    void start()
    {
        Debug.Log("hai");
        Debug.Log(GameId);
        // open dashboard from service (beside inspector)
        // GameId —> Operate tab of the Developer Dashboard —> Monetization —> Placements
        if (Application.platform == RuntimePlatform.Android)
        {
            GameId = "3425136";
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            GameId = "3425137";
        }
    Advertisement.Initialize(GameId, true);
    Debug.Log(GameId);
        //Advertisment.Initialize(GameId, testMode);
    }

    public void ShowAd()
    {
        if(!Advertisement.isInitialized)
        {
            Advertisement.Initialize(GameId);
        }
        Advertisement.Initialize(GameId, true);
        // StartCoroutine(WaitForAd());
        Advertisement.Show(placementId_video);
        Debug.Log("Show rwd"+GameId);
    }


    void AdFinished(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            // Reward the player
            var currCoins = PlayerPrefs.GetInt("Coin");
            currCoins = currCoins+50;
            PlayerPrefs.SetInt("Coin", currCoins);
        }
    }
}
