using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace AchromaticDev.Util.Notification
{
    public class NotificationElement : MonoBehaviour
    {
        [SerializeField] Text MessageText;

        private RectTransform rectTransform;

        public NotificationElement SetMessage(string message)
        {
            rectTransform = GetComponent<RectTransform>();
            MessageText.text = message;
            return this;
        }

        public NotificationElement Show()
        {
            gameObject.SetActive(true);
            Debug.Log($"{rectTransform == null}");
            rectTransform.DOAnchorPosX(rectTransform.sizeDelta.x, 0.5f).SetEase(Ease.OutBack);
            return this;
        }

        public NotificationElement Hide()
        {
            rectTransform.DOAnchorPosX(0, 0.5f).SetEase(Ease.InBack);
            return this;
        }

        public NotificationElement Destroy()
        {
            Destroy(gameObject);
            return this;
        }
    }
}
