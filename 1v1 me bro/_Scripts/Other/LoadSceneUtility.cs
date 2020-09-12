using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneUtility
{
    public static void LoadLevelAsyncWithFade(Image blackFadeQuitEnter, int sceneBuildIdx)
    {
        blackFadeQuitEnter.gameObject.SetActive(true);
        blackFadeQuitEnter.StartCoroutine(LoadLevelAsyncCoroutine(blackFadeQuitEnter, sceneBuildIdx));
    }

    public static void FadeOnLevelLoaded(Image blackFadeQuitEnter)
    {
        blackFadeQuitEnter.gameObject.SetActive(true);
        blackFadeQuitEnter.StartCoroutine(FadeOnLevelLoadedCoroutine(blackFadeQuitEnter));
    }


    // les niveaux chargent quasi instantanement
    private static IEnumerator LoadLevelAsyncCoroutine(Image blackFadeQuitEnter, int sceneBuildIdx)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneBuildIdx);
        asyncOperation.allowSceneActivation = false;
        yield return blackFadeQuitEnter.StartCoroutine(Fading(blackFadeQuitEnter, new Color(0.1372549f, 0.1215686f, 0.1254902f, 1)));
        asyncOperation.allowSceneActivation = true;
    }
    
    private static IEnumerator FadeOnLevelLoadedCoroutine(Image blackFadeQuitEnter)
    {
        AbstractMenu.Unpause();
        blackFadeQuitEnter.color = new Color(0.1372549f, 0.1215686f, 0.1254902f, 1);
        blackFadeQuitEnter.gameObject.SetActive(true);
        yield return blackFadeQuitEnter.StartCoroutine(Fading(blackFadeQuitEnter, new Color(0, 0, 0, 0)));
        blackFadeQuitEnter.gameObject.SetActive(false);
    }


    private static IEnumerator Fading(Image blackFadeQuitEnter, Color goal)
    {
        while (blackFadeQuitEnter.color != goal)
        {
            Vector4 step = Vector4.MoveTowards(blackFadeQuitEnter.color, goal, 2 * Time.unscaledDeltaTime);
            blackFadeQuitEnter.color = step;
            yield return null;
        }
    }

}
