using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
[RequireComponent(typeof(ParticleSystem))]
public class SpellInstantHitParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    public void StartCountdown()
    {
        StartCoroutine(DestroyAfterCountdown());
    }
    private IEnumerator DestroyAfterCountdown()
    {
        if (particles == null)
        {
            Destroy(gameObject);
            yield break;
        }

        yield return new WaitForSeconds(particles.main.duration);

        Destroy(gameObject);
    }
}