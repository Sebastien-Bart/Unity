using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootController : MonoBehaviour
{
    public shootController otherShootController;

    public Transform player;
    public GameObject bulletPrefab;
    public ParticleSystem shoot_particle;

    public float fireDelay = 3;
    public bool goodShot = false; // true si countdown fini

    private bool _canShoot = true;
    public bool canShoot
    {
        get => _canShoot;
        set { _canShoot = value; }
    }

    public void Shoot()
    {
        if (canShoot && !InGameMenu.paused)
        {
            Camera.main.GetComponent<CameraShake>().AskShake();
            canShoot = false;
            player.GetComponent<AnimationController>().PlayShootAnimation(goodShot);
            StartCoroutine("WaitForNextShootAvailable");
            InstanciateBullet(goodShot);
            // test
            if (goodShot)
            {
                otherShootController.goodShot = false;
            }
        }
    }

    private IEnumerator WaitForNextShootAvailable()
    {
        player.GetComponent<AnimationController>().StartPlayingReload();
        float counter = 0;
        while (counter < fireDelay)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        canShoot = true;
        player.GetComponent<AnimationController>().StopPlayingReload();
    }

    private void InstanciateBullet(bool goodShot)
    {
        GameObject bullet = Object.Instantiate(bulletPrefab);
        bullet.GetComponent<bulletController>().SetPositionAndDirection(player, goodShot, shoot_particle);
    }
}
