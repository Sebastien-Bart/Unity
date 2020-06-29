using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MAINMENUTMP : MonoBehaviour
{
    public void LoadLevel(int idx)
    {
        SceneManager.LoadScene(idx);
    }
}
