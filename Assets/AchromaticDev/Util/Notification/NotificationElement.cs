using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace AchromaticDev.Util.Notification
{
    public class NotificationElement : MonoBehaviour
    {
        [SerializeField] Text MessageText;

        internal int index
        {
            get {
                return _index;
            }
            set {
                _index = value;
                MoveToIndex();
            }
        }

        private int _index = -1;

        private RectTransform rectTransform;
        private LinkedListNode<NotificationElement> _node;
        private NotificationManager manager;

        public NotificationElement Initialize(string message, LinkedListNode<NotificationElement> node, int index)
        {
            _index = index;
            _node = node;

            MessageText.text = message;
            manager = NotificationManager.Instance;
            rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = manager.Settings.NotificationSize;
            rectTransform.anchoredPosition = new Vector2(0, -manager.Settings.NotificationSize.y * index - manager.Settings.SpaceBetween * index);
            return this;
        }

        public NotificationElement Show()
        {
            gameObject.SetActive(true);
            rectTransform.DOAnchorPosX(rectTransform.sizeDelta.x, manager.Settings.AnimationDuration).SetEase(Ease.OutBack);
            StartCoroutine(HideAfterDelay());
            return this;
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(manager.Settings.DisplayDuration);
            Hide();
        }

        public NotificationElement Hide()
        {
            rectTransform.DOAnchorPosX(0, manager.Settings.AnimationDuration).SetEase(Ease.InBack);
            StartCoroutine(DestroyAfterDelay(manager.Settings.AnimationDuration));
            return this;
        }

        private IEnumerator DestroyAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy();
        }

        public NotificationElement Destroy()
        {
            DOTween.Kill(rectTransform);

            Destroy(gameObject);
            for (var node = _node.Next; node != null; node = node.Next)
            {
                node.Value.index--;
            }
            
            _node.List?.Remove(_node);
            return this;
        }

        private void MoveToIndex()
        {
            var notificationSettings = NotificationManager.Instance.Settings;
            rectTransform.DOAnchorPosY(-notificationSettings.NotificationSize.y * index - notificationSettings.SpaceBetween * index, manager.Settings.AnimationDuration).SetEase(Ease.OutBack);
        }
    }
}
