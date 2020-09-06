using TMPro;
using UnityEngine;

public class BrocoinsTxt : MonoBehaviour
{

    private TextMeshProUGUI brocoinsTxt;
    private int brocoins;

    private void Start()
    {
        brocoinsTxt = gameObject.GetComponent<TextMeshProUGUI>();
        brocoins = PlayerPrefs.GetInt("brocoins", 0);
        brocoinsTxt.text = brocoins.ToString();
    }

    private void Update()
    {
        int newBroCoins = PlayerPrefs.GetInt("brocoins", 0);
        if (newBroCoins != brocoins)
        {
            UpdateBrocoins(newBroCoins);
        }
    }

    private void UpdateBrocoins(int newBroCoins)
    {
        brocoins = newBroCoins;
        brocoinsTxt.text = brocoins.ToString();
    }

}
