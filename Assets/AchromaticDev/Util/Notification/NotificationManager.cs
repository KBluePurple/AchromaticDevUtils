using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace AchromaticDev.Util.Notification
{
    [Serializable]
    public class NotificationSettings
    {
        [FormerlySerializedAs("MaxNotifications")] [Header("Notification Settings")]
        public int maxNotifications = 5;
        [FormerlySerializedAs("DisplayDuration")] public float displayDuration = 3f;

        [FormerlySerializedAs("AnimationDuration")] [Header("Animation Settings")]
        public float animationDuration = 1.5f;
        [FormerlySerializedAs("InEase")] public Ease inEase = Ease.InBack;
        [FormerlySerializedAs("OutEase")] public Ease outEase = Ease.OutBack;

        [FormerlySerializedAs("SpaceBetween")] [Header("Layout Settings")]
        public float spaceBetween = 50f;
        [FormerlySerializedAs("NotificationSize")] public Vector2 notificationSize = new Vector2(500, 100);
    }


    [ExecuteAlways, RequireComponent(typeof(NotificationFactory))]
    public class NotificationManager : MonoSingleton<NotificationManager>
    {
        [FormerlySerializedAs("Settings")] public NotificationSettings settings;

        [FormerlySerializedAs("NotificationPrefab")] [SerializeField] GameObject notificationPrefab;
        [FormerlySerializedAs("NotificationContainer")] [SerializeField] RectTransform notificationContainer;

        private LinkedList<NotificationElement> _notificationQueue = new LinkedList<NotificationElement>();
        private NotificationFactory _notificationFactory;

        private void Awake()
        {
            _notificationFactory = GetComponent<NotificationFactory>();
        }

        private void Update()
        {
            if (!Application.isPlaying)
            {
                notificationPrefab.GetComponent<RectTransform>().sizeDelta = settings.notificationSize;
                var rectTransform = notificationContainer.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(settings.notificationSize.x, settings.notificationSize.y * settings.maxNotifications + settings.spaceBetween * (settings.maxNotifications - 1));
            }
        }

        public void ShowNotification(string message)
        {
            if (_notificationQueue.Count >= settings.maxNotifications)
            {
                _notificationQueue.First.Value.Index = -1;
                _notificationQueue.RemoveFirst();

                foreach (var notification in _notificationQueue)
                {
                    notification.Index--;
                }
            }

            var notificationObject = Instantiate(notificationPrefab, notificationContainer);
            Debug.Log($"NotificationQueue.Count: {_notificationQueue.Count}");
            notificationObject
                .GetComponent<NotificationElement>()
                .Initialize(
                    message,
                    _notificationQueue
                        .AddLast(
                        notificationObject
                            .GetComponent<NotificationElement>()
                    ),
                    _notificationQueue.Count - 1
                )
                .Show();
        }
    }
}
