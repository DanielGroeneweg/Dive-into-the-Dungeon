using System;
using UnityEngine;
public abstract class GameEvent<T> : ScriptableObject
    where T : GameEventData
{
    private event Action<T> listeners;

    public void Raise(T data)
    {
        listeners?.Invoke(data);
    }

    public void Register(Action<T> listener)
    {
        listeners += listener;
    }

    public void Unregister(Action<T> listener)
    {
        listeners -= listener;
    }
}