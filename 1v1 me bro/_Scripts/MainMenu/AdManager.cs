using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{

    private void Start()
    {
        Advertisement.Initialize("3786115", true);
        Advertisement.AddListener(this);
    }

    public void AskAd()
    {
        StartCoroutine(PlayAd());
    }

    private IEnumerator PlayAd()
    {
        while (!Advertisement.IsReady("rewardedVideo"))
            yield return null;
        Advertisement.Show("rewardedVideo");
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            PlayerData.nbBrocoins += 1;
            PlayerData.SaveBrocoinsAndAccess();
            AudioManagerForOneGame.am.PlaySound("OneBrocoin");
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    private void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

}
