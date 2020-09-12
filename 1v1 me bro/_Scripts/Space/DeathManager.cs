using System.Collections;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [Header("Particles")]
    public ParticleSystem deathParticle;
    public ParticleSystem respawnParticle;
    public ParticleSystem prepareRespawnParticle;

    [Header("Settings")]
    public float respawnTime = 3f;

    public void Kill(GameObject starship)
    {
        Camera.main.GetComponent<CameraShake>().AskShake();
        AudioManagerForOneGame.am.PlaySound("Explosion");
        StarshipController controller = starship.transform.GetComponent<StarshipController>();
        GivaHalfStar(controller);
        ParticleSystem deathP = Instantiate(deathParticle, starship.transform.position, Quaternion.identity);
        deathP.Play();
        starship.SetActive(false);
        StartCoroutine(Respawn(starship, controller.spawnPos, controller.spawnRot));
    }

    private void GivaHalfStar(StarshipController killed)
    {
        StarshipController other = killed.otherStarship.GetComponent<StarshipController>();
        int toGive;
        if (killed.pointManager.points % 2 != 0)
            toGive = (killed.pointManager.points + 1) / 2;
        else
            toGive = killed.pointManager.points / 2;
        other.pointManager.AddPoints(toGive);
        killed.pointManager.RemovePoints(toGive);
    }

    public IEnumerator Respawn(GameObject starship, Vector2 spawnPos, Quaternion spawnRot)
    {
        starship.transform.position = spawnPos;
        starship.transform.rotation = spawnRot;
        yield return new WaitForSeconds(1f);
        ParticleSystem prepareRespawnP = Instantiate(prepareRespawnParticle, starship.transform.position, Quaternion.identity);
        ParticleSystem respawnP = Instantiate(respawnParticle, starship.transform.position, Quaternion.identity);
        ParticleSystem.MainModule mainPrep = prepareRespawnP.main;
        ParticleSystem.MainModule mainResp = respawnP.main;
        if (spawnPos.x < 0)
        {
            mainPrep.startColor = new Color(0.6f, 0.9f, 1f, 1f);
            mainResp.startColor = new Color(0.6f, 0.9f, 1f, 1f);
        }
        else
        {
            mainPrep.startColor = new Color(1f, 0.6f, 0.6f, 1f);
            mainResp.startColor = new Color(1f, 0.6f, 0.6f, 1f);
        }
        prepareRespawnP.Play();
        yield return new WaitForSeconds(respawnTime - 1f);
        prepareRespawnP.Stop();
        respawnP.Play();
        AudioManagerForOneGame.am.PlaySound("Spawn");
        starship.SetActive(true);
    }

}
