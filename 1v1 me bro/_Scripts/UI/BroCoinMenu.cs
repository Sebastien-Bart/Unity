using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BroCoinMenu : AbstractMenu
{
    public GameObject BuyButtonFullAccess, BuyButtonFifty;

    protected override IEnumerator MoveToActivePosWithFadeIn()
    {
        CheckIfFullAccessAndAdjustButtons();
        Time.timeScale = 1;
        fader.gameObject.SetActive(true);
        yield return StartCoroutine(base.MoveToActivePosWithFadeIn());
    }

    protected override IEnumerator MoveToNotActivePosWithFadeOut()
    {
        yield return StartCoroutine(base.MoveToNotActivePosWithFadeOut());
        fader.gameObject.SetActive(false);
    }

    public void CheckIfFullAccessAndAdjustButtons()
    {
        if (PlayerData.hasFullAccess)
        {
            BuyButtonFullAccess.GetComponent<Image>().color = Color.green;
            BuyButtonFifty.GetComponent<Image>().color = Color.green;

            Button btn1 = BuyButtonFullAccess.GetComponent<Button>();
            btn1.interactable = false;

            Button btn2 = BuyButtonFifty.GetComponent<Button>();
            btn2.interactable = false;
            
            BuyButtonFullAccess.transform.GetChild(0).GetComponent<RectTransform>().anchorMin = new Vector2(0.1f, 0.1f);
            BuyButtonFullAccess.transform.GetChild(0).GetComponent<RectTransform>().anchorMax = new Vector2(0.9f, 0.9f);
            TextMeshProUGUI t = BuyButtonFullAccess.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            if (PlayerPrefs.GetString("lang", "en") == "fr")
                t.text = "Achete\n(merci)";
            else
                t.text = "bought\n(thanks)";
        }
    }

}
