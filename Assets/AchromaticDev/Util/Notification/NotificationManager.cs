using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AchromaticDev.Util.Notification
{
    [RequireComponent(typeof(NotificationFactory))]
    public class NotificationManager : MonoBehaviour
    {
        private NotificationFactory NotificationFactory;

        private void Awake()
        {
            NotificationFactory = GetComponent<NotificationFactory>();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            NotificationFactory.CreateNotification("Hello World!");
        }
    }
}
