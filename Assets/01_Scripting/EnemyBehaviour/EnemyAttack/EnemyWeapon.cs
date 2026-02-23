using UnityEngine;
public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private SingleMeleeAttack attack;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") attack.PlayerHit();
    }
}