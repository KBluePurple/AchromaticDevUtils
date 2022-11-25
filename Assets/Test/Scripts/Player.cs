using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Test
{
    public class Player : MonoBehaviour
    {
        public ParticleSystem jumpParticles;
        public ParticleSystem walkParticles;
        
        public LayerMask groundLayer;

        private Rigidbody2D rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            float x = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(x * 5, rb.velocity.y);
            rb.rotation = rb.velocity.x * 5;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, 5);
                jumpParticles.Play();
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (!walkParticles.isPlaying)
                    walkParticles.Play();
            }
            else
            {
                walkParticles.Stop();
            }

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (1.5f - 1) * Time.deltaTime;
            }

            Vector3 position = transform.position;
            Debug.DrawRay(position, Vector2.down * 0.55f, Color.red);
            var hit = Physics2D.Raycast(position, Vector2.down, 0.55f, groundLayer);
            if (hit.collider == null)
            {
                walkParticles.Stop();
            }
        }
    }
}