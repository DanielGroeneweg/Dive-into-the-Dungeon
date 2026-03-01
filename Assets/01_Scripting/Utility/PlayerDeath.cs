using UnityEngine.Events;
using UnityEngine;
public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private UnityEvent OnDeath;
    private void OnEnable()
    {
        GameManager.Instance.LinkGameOverEvent(OnPlayerDeath);
    }
    private void OnDisable()
    {
        GameManager.Instance.UnlinkGameOverEvent(OnPlayerDeath);
    }
    private void OnPlayerDeath(GameOverEventData data)
    {
        OnDeath?.Invoke();
    }
}