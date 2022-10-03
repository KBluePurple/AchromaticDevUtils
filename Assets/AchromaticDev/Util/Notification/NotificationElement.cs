using System.Collections;
using System.Collections.Generic;
using AchromaticDev.Util.Pooling;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace AchromaticDev.Util.Notification
{
    public class NotificationElement : MonoBehaviour
    {
        [SerializeField] Text messageText;

        private int _index = -1;

        private RectTransform _rectTransform;
        private LinkedListNode<NotificationElement> _node;
        private Notification _parant;

        internal int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                MoveToIndex();
            }
        }

        public NotificationElement Initialize(Notification parant, string message, LinkedListNode<NotificationElement> node, int index)
        {
            _index = index;
            _node = node;

            messageText.text = message;
            _parant = parant;
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.sizeDelta = _parant.settings.notificationSize;
            _rectTransform.anchoredPosition = new Vector2(0,
                -_parant.settings.notificationSize.y * index - _parant.settings.spaceBetween * index);
            return this;
        }

        public NotificationElement Show()
        {
            gameObject.SetActive(true);
            _rectTransform.DOAnchorPosX(_parant.settings.isLeft ? _rectTransform.sizeDelta.x : -_rectTransform.sizeDelta.x, _parant.settings.animationDuration)
                .SetEase(_parant.settings.inEase);
            StartCoroutine(HideAfterDelay());
            return this;
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(_parant.settings.displayDuration);
            Hide();
        }

        public NotificationElement Hide()
        {
            _rectTransform.DOAnchorPosX(0, _parant.settings.animationDuration)
                .SetEase(_parant.settings.outEase);
            StartCoroutine(DestroyAfterDelay(_parant.settings.animationDuration));
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
            PoolManager.Destroy(gameObject);
            for (var node = _node.Next; node != null; node = node.Next)
            {
                node.Value.Index--;
            }

            _node.List?.Remove(_node);
            return this;
        }

        private void MoveToIndex()
        {
            var notificationSettings = _parant.settings;
            _rectTransform
                .DOAnchorPosY(
                    -notificationSettings.notificationSize.y * Index - notificationSettings.spaceBetween * Index,
                    _parant.settings.animationDuration).SetEase(Ease.OutBack);
        }
    }
}