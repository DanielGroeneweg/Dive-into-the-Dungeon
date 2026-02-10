using System;
using UnityEngine;
public abstract class GameEvent<T> : ScriptableObject
    where T : GameEventData
{
    private event Action<GameEventData> listeners;

    public void Raise(GameEventData data)
    {
        listeners?.Invoke(data);
    }

    public void Register(Action<GameEventData> listener)
    {
        listeners += listener;
    }

    public void Unregister(Action<GameEventData> listener)
    {
        listeners -= listener;
    }
}