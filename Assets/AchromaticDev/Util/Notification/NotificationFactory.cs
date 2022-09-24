using System.Collections;
using System;
using UnityEngine;

namespace AchromaticDev.Util.Notification
{
    public class NotificationFactory : MonoBehaviour
    {
        [SerializeField] GameObject NotificationPrefab;
        [SerializeField] RectTransform NotificationContainer;

        public void CreateNotification(string message)
        {
            var notificationObject = Instantiate(NotificationPrefab, NotificationContainer);

            StartCoroutine(
                DestroyNotification(
                    notificationObject.GetComponent<NotificationElement>()
                        .SetMessage(message)
                        .Show()
                )
            );
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
