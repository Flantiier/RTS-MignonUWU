using UnityEngine;
using System.Collections.Generic;
using Scripts.Events;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    /// <summary>
    /// Raising all the listeners responses
    /// </summary>
    public void Raise()
    {
        if (listeners.Count <= 0)
            return;

        foreach (GameEventListener listener in listeners)
            listener.Raise();
    }

    /// <summary>
    /// Register a listener to the GameEvent
    /// </summary>
    public void RegisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
            return;

        listeners.Add(listener);
    }

    /// <summary>
    /// Unregister a listener to the GameEvent
    /// </summary>
    public void UnregisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            return;

        listeners.Remove(listener);
    }
}
