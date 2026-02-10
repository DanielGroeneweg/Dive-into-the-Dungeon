using UnityEngine;
using UnityEngine.Events;
public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent<GameEventData> gameEvent;
    [SerializeField] private UnityEvent response;

    private void OnEnable()
    {
        gameEvent.Register(OnEventRaised);
    }

    private void OnDisable()
    {
        gameEvent.Unregister(OnEventRaised);
    }

    private void OnEventRaised(GameEventData data)
    {
        response.Invoke();
    }
}