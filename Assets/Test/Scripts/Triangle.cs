using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class Triangle : MonoBehaviour
    {
        private bool _isInitialized = false;
        private Image[] _images = new Image[3];
        private Vector2[] _positions = new Vector2[3];
        
        private void Awake()
        {
            Vector2 position = transform.position;
            float radius = ((RectTransform)transform).rect.width / 2;
            
            for (int i = 0; i < 3; i++)
            {
                var degree = Mathf.Deg2Rad * (-i * 120 + 90);
                float x = position.x + radius * Mathf.Cos(degree);
                float y = position.y + radius * Mathf.Sin(degree);
                _positions[i] = new Vector2(x, y);
            }
            
            for (int i = 0; i < 3; i++)
            {
                _images[i] = transform.GetChild(i).GetComponent<Image>();
                _images[i].transform.position = _positions[i];
            }
            
            StartCoroutine(Animate());
        }
        
        private IEnumerator Animate()
        {
            int index = 0;
            while (true)
            {
                for (int i = 0; i < 3; i++)
                {
                    _images[i].transform.DOMove(_positions[(i + index) % 3], 0.5f);
                }
                index++;
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
