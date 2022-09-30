using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.Serialization;

namespace AchromaticDev.Util.Notification
{
    public class NotificationElement : MonoBehaviour
    {
        [SerializeField] Text messageText;

        private int _index = -1;

        private RectTransform _rectTransform;
        private LinkedListNode<NotificationElement> _node;
        private NotificationManager _manager;

        internal int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
                MoveToIndex();
            }
        }

        public NotificationElement Initialize(string message, LinkedListNode<NotificationElement> node, int index)
        {
            _index = index;
            _node = node;

            messageText.text = message;
            _manager = NotificationManager.Instance;
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.sizeDelta = _manager.settings.notificationSize;
            _rectTransform.anchoredPosition = new Vector2(0, -_manager.settings.notificationSize.y * index - _manager.settings.spaceBetween * index);
            return this;
        }

        public NotificationElement Show()
        {
            gameObject.SetActive(true);
            _rectTransform.DOAnchorPosX(_rectTransform.sizeDelta.x, _manager.settings.animationDuration)
                .SetEase(_manager.settings.inEase);
            StartCoroutine(HideAfterDelay());
            return this;
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(_manager.settings.displayDuration);
            Hide();
        }

        public NotificationElement Hide()
        {
            _rectTransform.DOAnchorPosX(0, _manager.settings.animationDuration)
                .SetEase(_manager.settings.outEase);
            StartCoroutine(DestroyAfterDelay(_manager.settings.animationDuration));
            return this;
        }

        private IEnumerator DestroyAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy();
        }

        public NotificationElement Destroy()
        {
            DOTween.Kill(_rectTransform);

            Destroy(gameObject);
            for (var node = _node.Next; node != null; node = node.Next)
            {
                node.Value.Index--;
            }
            
            _node.List?.Remove(_node);
            return this;
        }

        private void MoveToIndex()
        {
            var notificationSettings = NotificationManager.Instance.settings;
            _rectTransform.DOAnchorPosY(-notificationSettings.notificationSize.y * Index - notificationSettings.spaceBetween * Index, _manager.settings.animationDuration).SetEase(Ease.OutBack);
        }
    }
}
