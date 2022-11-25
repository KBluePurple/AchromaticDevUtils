using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class TextAnimation : MonoBehaviour
    {
        public float duration = 1f;
        public float delay = 0.5f;
        public Ease inEase = Ease.Linear;
        public Ease outEase = Ease.Linear;
        
        private Image image;
        private bool isAnimating;
        
        private void Start()
        {
            image = GetComponent<Image>();
            image.fillAmount = 0;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShowText();
            }
        }

        public void ShowText()
        {
            if (isAnimating)
            {
                return;
            }
            StartCoroutine(ShowTextCoroutine());
        }
        
        private IEnumerator ShowTextCoroutine()
        {
            isAnimating = true;
            image.fillOrigin = 0;
            image.DOFillAmount(1, duration).SetEase(inEase);
            yield return new WaitForSeconds(duration + delay);
            image.fillOrigin = 1;
            image.DOFillAmount(0, duration).SetEase(outEase);
            yield return new WaitForSeconds(duration);
            isAnimating = false;
        }
    }
}
