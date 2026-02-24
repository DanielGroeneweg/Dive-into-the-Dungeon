using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Threading;
public class ExplodeSelf : Attack
{
    [SerializeField] private float radius;
    [SerializeField] private float chargeTime;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Color targetColor;
    [SerializeField] private UnityEvent explode;
    public override void DoAttack()
    {
        StartCoroutine(ChargeThenExplode());
    }
    private IEnumerator ChargeThenExplode()
    {
        float timePassed = 0;
        Color colorPerSecond = (targetColor - _renderer.material.color) / chargeTime;
        Color startColor = _renderer.material.color;
        while (timePassed < chargeTime)
        {
            timePassed += Time.deltaTime;

            Color newColor = timePassed >= chargeTime ? targetColor : startColor + colorPerSecond * timePassed;
            _renderer.material.color = new Color(newColor.r, newColor.g, newColor.b, 1);

            yield return null;
        }

        Explode();
    }
    private void Explode()
    {
        // Deal Damage
        if ((Locator.instance.Player.position - transform.position).magnitude <= radius)
        {
            DamagePlayerEventData data = new DamagePlayerEventData(damage, gameObject);
            EventBusManager.Instance.DamagePlayerEvent.Raise(data);
        }

        // Summon explosion
        Instantiate(explosion, transform.position, Quaternion.identity);

        // Destroy enemy
        explode?.Invoke();
        Destroy(gameObject);
    }
}