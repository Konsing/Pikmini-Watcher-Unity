using System;
using UnityEngine;
using Pikmini;
// change name to Publisher Manager
public class PublisherManager : MonoBehaviour
{
    
    public int groupCount { get; } = 3;
    private IPublisher group1Publisher;
    private IPublisher group2Publisher;
    private IPublisher group3Publisher;
    public void Awake()
    {
        group1Publisher = new Publisher();
        group2Publisher = new Publisher();
        group3Publisher = new Publisher();
    }

    /// <summary>
    /// Sends the the destination to all of the subscribers of a particular
    /// publisher based on group id.
    /// </summary>
    /// <param name="group">The group id of the publisher to activate.</param>
    /// <param name="destination">The destination to be sent.</param>
    public void SendMessageWithPublisher(int group, Vector3 destination)
    {
        switch (group)
        {
            case 1:
                group1Publisher.Notify(destination);
                break;
            case 2:
                group2Publisher.Notify(destination);
                break;
            case 3:
                group3Publisher.Notify(destination);
                break;
            default:
                Debug.Log("Invalid group for SMWP function");
                break;
        }
    }

    /// <summary>
    /// Subscribes to a publisher based on group id. The callback is executed
    /// when the publisher of the group sends a message.
    /// </summary>
    /// <param name="group">The group id.</param>
    /// <param name="callback">The callback to call when a message is sent.
    /// </param>
    public void SubscribeToGroup(int group, Action<Vector3> callback)
    {
        switch (group)
        {
            case 1:
                group1Publisher.Subscribe(callback);
                break;
            case 2:
                group2Publisher.Subscribe(callback);
                break;
            case 3:
                group3Publisher.Subscribe(callback);
                break;
            default:
                Debug.Log("Invalid group for STG function");
                break;
        }
    }

    /// <summary>
    /// Unsubscribes a callback from a group publiser.
    /// </summary>
    /// <param name="group">The group id.</param>
    /// <param name="callback">The callback to remove from the subscriber list.
    /// </param>
    public void UnsubscribeFromGroup(int group, Action<Vector3> callback)
    {
        switch (group)
        {
            case 1:
                group1Publisher.Unsubscribe(callback);
                break;
            case 2:
                group2Publisher.Unsubscribe(callback);
                break;
            case 3:
                group3Publisher.Unsubscribe(callback);
                break;
            default:
                Debug.Log("Invalid group for UFG function");
                break;
        }
    }
}
