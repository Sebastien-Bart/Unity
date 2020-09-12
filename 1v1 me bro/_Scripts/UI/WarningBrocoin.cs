using System.Collections;
using UnityEngine;

public class WarningBrocoin : MonoBehaviour
{

    public GameObject blackFade;
    public GameObject warning;

    private Animator blackFadeAnim;
    private Animator warningAnim;

    private bool quitRunning = false;

    private void Start()
    {
        blackFadeAnim = blackFade.GetComponent<Animator>();
        warningAnim = warning.GetComponent<Animator>();
    }

    public void ShowWarning()
    {
        warning.SetActive(true);
        blackFade.SetActive(true);
    }

    public void QuitWarning()
    {
        if (!quitRunning && !warningAnim.GetCurrentAnimatorStateInfo(0).IsName("warningBrocoin"))
        {
            AudioManagerMenu.am.PlaySound("WarningOut");
            quitRunning = true;
            warningAnim.Play("warningBrocoinOut");
            blackFadeAnim.Play("BlackFadeWarningOut");
            StartCoroutine("QuitAndDeactivate");
        }
    }

    private IEnumerator QuitAndDeactivate()
    {
        yield return null;
        while (blackFadeAnim.GetCurrentAnimatorStateInfo(0).IsName("BlackFadeWarningOut"))
            yield return null;
        warning.SetActive(false);
        blackFade.SetActive(false);
        quitRunning = false;
    }

}
