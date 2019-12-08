using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPower : MonoBehaviour
{

    public GameObject player;

    public void redSelected()
    {
        player.GetComponent<SpriteRenderer>().sprite = transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite;
        PlayerController.color = "red";
    }

    public void yellowSelected()
    {
        player.GetComponent<SpriteRenderer>().sprite = transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<Image>().sprite;
        PlayerController.color = "yellow";
    }

    public void blueSelected()
    {
        player.GetComponent<SpriteRenderer>().sprite = transform.GetChild(0).GetChild(2).GetChild(0).gameObject.GetComponent<Image>().sprite;
        PlayerController.color = "blue";
    }

}
