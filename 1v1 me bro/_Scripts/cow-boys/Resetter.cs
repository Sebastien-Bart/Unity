using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resetter : MonoBehaviour
{
    public CountdownManagement countdown;
    public MonoBehaviour[] toReset;
    public shootController[] shootControllers;

    [Range(1f, 6f)] public float fadeSpeed = 3f;
    public Image fader;

    private bool running = false;

    public IEnumerator Reset()
    {
        if (!running)
        {
            running = true;
            StartCoroutine(countdown.MoveUp());
            //cant shoot
            foreach (shootController sc in shootControllers)
            {
                sc.canShoot = false;
                sc.StopAllCoroutines();
            }
            // FADE IN
            float counter = 0f;
            while (fader.color.a != 1f)
            {
                counter += Time.deltaTime;
                fader.color = new Color(fader.color.r, fader.color.g, fader.color.b,
                                         Mathf.Clamp(counter / fadeSpeed, 0, 1));
                yield return null;
            }
            // reset
            foreach (MonoBehaviour script in toReset)
            {
                script.SendMessage("Reset");
            }
            // FADE OUT
            counter = 0f;
            while (fader.color.a != 0f)
            {
                counter += Time.deltaTime;
                fader.color = new Color(fader.color.r, fader.color.g, fader.color.b,
                                         1 - Mathf.Clamp(counter / fadeSpeed, 0, 1));
                yield return null;
            }
            // restart a round
            StartCoroutine(countdown.MoveDownAndStartCountDown());
            foreach (shootController sc in shootControllers)
            {
                sc.canShoot = true;
            }
        }
        running = false;
    }

}
