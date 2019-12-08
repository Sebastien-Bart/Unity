using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTextScript : MonoBehaviour
{

    private Text winTxt;

    // Start is called before the first frame update
    void Start()
    {
        winTxt = GetComponent<Text>();
        int nbThrows = PlayerController.nbThrows;
        if(nbThrows == 1)
        {
            winTxt.text += " "+ nbThrows + " seul coup ! Waow !";
        }
        else
        {
            winTxt.text += " " +nbThrows + " coups !";
        }
    }

}
