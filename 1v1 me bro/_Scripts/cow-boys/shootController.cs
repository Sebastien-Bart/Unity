using System.Collections;
using UnityEngine;

public class shootController : MonoBehaviour
{
    public shootController otherShootController;

    public Transform player;
    public GameObject bulletPrefab;
    public ParticleSystem shoot_particle;

    public float fireDelay = 3;
    public bool goodShot = false; // true si countdown fini

    public bool canShoot = true;

    public void Shoot()
    {
        if (canShoot && !AbstractMenu.Paused)
        {
            if (goodShot)
                StartCoroutine(WaitAndDisableOtherShooter());
            Camera.main.GetComponent<CameraShake>().AskShake();
            canShoot = false;
            player.GetComponent<AnimationController>().PlayShootAnimation(goodShot);
            StartCoroutine("WaitForNextShootAvailable"); 
            AudioManagerForOneGame.am.PlaySound("shoot");
            InstanciateBullet(goodShot);
        }
    }

    private IEnumerator WaitAndDisableOtherShooter()
    {
        yield return new WaitForSeconds(0.1f);
        otherShootController.canShoot = false;
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
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<bulletController>().SetPositionAndDirection(player, goodShot, shoot_particle);
    }
}
