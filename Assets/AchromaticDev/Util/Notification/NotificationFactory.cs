using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using AchromaticDev.Extensions;

namespace AchromaticDev.Util.Notification
{
    public class NotificationFactory : MonoBehaviour
    {
        [SerializeField] GameObject NotificationPrefab;
        [SerializeField] RectTransform NotificationContainer;
        LinkedList<NotificationElement> NotificationQueue = new LinkedList<NotificationElement>();

        public NotificationElement CreateNotification(string message)
        {
            var notificationObject = Instantiate(NotificationPrefab, NotificationContainer);

            return notificationObject
                .GetComponent<NotificationElement>()
                .Initialize(
                    message,
                    NotificationQueue
                        .AddLast(
                        notificationObject
                            .GetComponent<NotificationElement>()
                    ),
                    NotificationQueue.Count
                )
                .Show();
        }

        private void ShowNotification(NotificationElement notification)
        {
            StartCoroutine(ShowNotificationCoroutine(notification));
        }

        private IEnumerator ShowNotificationCoroutine(NotificationElement notification)
        {
            yield return new WaitForSeconds(3f);
            notification.Hide();
            yield return new WaitForSeconds(0.5f);
            NotificationQueue.Remove(notification);
            notification.Destroy();
        }

        private IEnumerator DestroyNotification(NotificationElement notification)
        {
            yield return new WaitForSeconds(1.5f);
            notification.Hide();
            yield return new WaitForSeconds(0.5f);
            notification.Destroy();
        }
    }
}
