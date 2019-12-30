using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFocusManagement : MonoBehaviour
{

    private int idxOfChildActuallyFocus;

    void Start()
    {
        transform.GetChild(0).GetComponent<PlayerController>().setFocus(true);
        idxOfChildActuallyFocus = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            transform.GetChild(idxOfChildActuallyFocus).GetComponent<PlayerController>().setFocus(false);
            idxOfChildActuallyFocus = (idxOfChildActuallyFocus + 1) % transform.childCount;
            transform.GetChild(idxOfChildActuallyFocus).GetComponent<PlayerController>().setFocus(true);
        }
    }
}
