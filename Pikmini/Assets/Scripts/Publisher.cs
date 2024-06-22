using System;
using System.Collections.Generic;
using Pikmini;
using UnityEngine;

public class Publisher : IPublisher
{
    private HashSet<Action<Vector3>> notifiers;

    public Publisher()
    {
        notifiers = new HashSet<Action<Vector3>>();
    }

    public void Subscribe(Action<Vector3> notifier)
    {
        if (notifier != null)
        {
            notifiers.Add(notifier);
        }
    }

    public void Unsubscribe(Action<Vector3> notifier)
    {
        if (notifier != null)
        {
            notifiers.Remove(notifier);
        }
    }

    public void Notify(Vector3 destination)
    {
        // Use a for loop to iterate over the notifiers
        foreach (var notifier in notifiers)
        {
            notifier?.Invoke(destination);
        }
    }
}
