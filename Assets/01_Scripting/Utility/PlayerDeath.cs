using UnityEngine.Events;
using UnityEngine;
public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private UnityEvent OnDeath;
    private void OnEnable()
    {
        EventBusManager.Instance.GameOverEvent.Register(OnPlayerDeath);
    }
    private void OnDisable()
    {
        EventBusManager.Instance.GameOverEvent.Unregister(OnPlayerDeath);
    }
    private void OnPlayerDeath(GameOverEventData data)
    {
        OnDeath?.Invoke();
    }
}