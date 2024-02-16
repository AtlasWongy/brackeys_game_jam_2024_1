using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Animator _animator;
        private bool _isMoving = false;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if (_isMoving)
            {
                var movementDirection = new Vector2(1.0f, 0.0f).normalized;
                transform.Translate(movementDirection * (1.0f * Time.deltaTime));
                _animator.SetBool("isMoving",  true);
            }
        }

        public void TriggerMovement()
        {
            _isMoving = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Time for scene transition");
            if (other.CompareTag("Door"))
            {
                _isMoving = false;
                _animator.SetBool("isMoving", false);
            }
        }
    }
}

