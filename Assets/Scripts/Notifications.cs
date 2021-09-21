using System;
using Unity.Notifications.Android;
using UnityEngine;

internal class Notifications: MonoBehaviour
{
    private const string ChannelID = "default";

    private void Awake()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = ChannelID,
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        AndroidNotificationCenter.CancelAllNotifications();
    }

    private void OnApplicationPause(bool _)
    {
        AndroidNotificationCenter.CancelAllNotifications();
        var now = DateTime.Now;
        var notification = new AndroidNotification
        {
            Title = "Hey there!",
            Text = "Do you want to jump?",
            FireTime = new DateTime(now.Year, now.Month, now.Day, 18, 0, 0).AddDays(1),
            RepeatInterval = new TimeSpan(1, 0, 0, 0)
        };
        AndroidNotificationCenter.SendNotification(notification, ChannelID);
    }
}