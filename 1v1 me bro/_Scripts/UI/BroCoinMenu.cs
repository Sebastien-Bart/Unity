using System.Collections;
using UnityEngine;

public class BroCoinMenu : AbstractMenu
{

    protected override IEnumerator MoveToActivePosWithFadeIn()
    {
        Time.timeScale = 1;
        fader.gameObject.SetActive(true);
        yield return StartCoroutine(base.MoveToActivePosWithFadeIn());
    }

    protected override IEnumerator MoveToNotActivePosWithFadeOut()
    {
        yield return StartCoroutine(base.MoveToNotActivePosWithFadeOut());
        fader.gameObject.SetActive(false);
    }

}
