using UnityEngine;
using System.Collections;
public class SpellWall : MonoBehaviour
{
    public void StartCountdown(float time) { StartCoroutine(DestroyOnSpellEnd(time)); }
    private IEnumerator DestroyOnSpellEnd(float time)
    {
        Debug.Log($"Waiting for {time} seconds");
        yield return new WaitForSeconds(time);
        Debug.Log("Wall Destroyed!");
        Destroy(gameObject);
    }
}