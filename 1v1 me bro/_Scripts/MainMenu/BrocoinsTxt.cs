using TMPro;
using UnityEngine;

public class BrocoinsTxt : MonoBehaviour
{
    public static bool needUpdate = false;
    public static bool txtIsFullAccess = false;

    private TextMeshProUGUI brocoinsTxt;

    private void Start()
    {
        brocoinsTxt = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!PlayerData.hasFullAccess)
        {
            if (needUpdate)
            {
                brocoinsTxt.text = PlayerData.nbBrocoins.ToString();
                needUpdate = false;
            }
        }
        else if (!txtIsFullAccess)
        {
            brocoinsTxt.text = "8";
            brocoinsTxt.GetComponent<RectTransform>().Rotate(new Vector3(0f, 0f, 90f));
            brocoinsTxt.font = TMP_Settings.defaultFontAsset;
            txtIsFullAccess = true;
        }
    }

}
