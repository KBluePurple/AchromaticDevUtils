using System;
using System.Collections.Generic;
using AchromaticDev.Util.Pooling;
using DG.Tweening;
using UnityEngine;

namespace AchromaticDev.Util.Notification
{
    [Serializable]
    public class NotificationSettings
    {
        [Header("Notification Settings")] public int maxNotifications = 5;
        public float displayDuration = 1f;

        [Header("Animation Settings")] public float animationDuration = 0.5f;
        public Ease inEase = Ease.OutBack;
        public Ease outEase = Ease.InBack;

        [Header("Layout Settings")] public float spaceBetween = 20f;
        public Vector2 notificationSize = new Vector2(500, 100);
        public bool isLeft = true;
    }

    [ExecuteAlways]
    public class Notification : MonoBehaviour
    {
        public NotificationSettings settings;

        [SerializeField] GameObject notificationPrefab;

        private readonly LinkedList<NotificationElement> _notificationQueue = new LinkedList<NotificationElement>();
        private RectTransform _notificationContainer;

        private void Awake()
        {
            _notificationContainer = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (!Application.isPlaying)
            {
                if (_notificationContainer == null)
                    notificationPrefab.GetComponent<RectTransform>().sizeDelta = settings.notificationSize;
                _notificationContainer.sizeDelta = new Vector2(settings.notificationSize.x,
                    settings.notificationSize.y * settings.maxNotifications +
                    settings.spaceBetween * (settings.maxNotifications - 1));
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ShowNotification("Test Notification");
                }
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

            var notificationObject = PoolManager.Instantiate(notificationPrefab, _notificationContainer);
            notificationObject
                .GetComponent<NotificationElement>()
                .Initialize(
                    this,
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