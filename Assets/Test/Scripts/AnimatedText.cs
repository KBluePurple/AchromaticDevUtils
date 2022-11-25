using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class AnimatedText : MonoBehaviour
    {
        private Image image;
        private void Awake()
        {
            image = GetComponent<Image>();
        }

        private void Start()
        {
            image.DOFillAmount(1f, 1f).From(0);
        }
    }
}
