using System.Collections;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    public Sprite[] sprites;

    public shootController[] shootControllers;
    public WinMenu winMenu;
    public AnimationController otherPlayerAnim;

    public Transform HP_holder;
    public Resetter resetter;
    public ParticleSystem hurt_particle;
    public ParticleSystem death_particle;

    private static float waitTimeToWinScreen = 3f;
    [SerializeField] [Range(0, 5)] private int hp = 5;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = Camera.main.GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            bulletController bulletController = collision.gameObject.GetComponent<bulletController>();
            if (!CompareTag(bulletController.shooter.tag))
            {
                TakeOneDamage(collision);
            }
        }
    }

    private void TakeOneDamage(Collider2D collision)
    {
        audioManager.PlaySound("touched");
        otherPlayerAnim.PlayRoundVictoryAnimation();
        hp--;
        HP_holder.GetComponent<SpriteRenderer>().sprite = sprites[hp];
        Destroy(collision.gameObject);
        hurt_particle.Play();
        if (hp == 0)
            Death();
        else
        {
            transform.GetComponent<AnimationController>().PlayHurtAnimation();
            StartCoroutine(resetter.Reset());
        }
    }

    private void Death()
    {
        foreach (var item in shootControllers)
        {
            item.StopAllCoroutines();
            item.canShoot = false;
        }
        audioManager.PlaySound("death");
        death_particle.Play();
        transform.GetComponent<AnimationController>().PlayDead();
        StartCoroutine(WaitAndWinScreeen());
    }

    private IEnumerator WaitAndWinScreeen()
    {
        yield return new WaitForSeconds(waitTimeToWinScreen);
        if (transform.position.x > 0)
        {
            winMenu.SetWinner("left");
        }
        else
        {
            winMenu.SetWinner("right");
        }
        winMenu.ActivateMenu();
    }
}
