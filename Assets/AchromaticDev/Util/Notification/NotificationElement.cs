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
        
        private int index
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

        public NotificationElement Initialize(string message, LinkedListNode<NotificationElement> node, int index)
        {
            _index = index;
            _node = node;

            MessageText.text = message;
            rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, -50 * index);
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
            for (var node = _node.Next; node != null; node = node.Next)
            {
                node.Value.index--;
            }
            return this;
        }
        private void MoveToIndex()
        {

        }
    }
}
