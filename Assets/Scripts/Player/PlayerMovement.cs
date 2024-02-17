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
            if (other != null)
            {
                _isMoving = false;
            }
            
            if (other.CompareTag("Door"))
            {
                _animator.SetBool("isMoving", false);
            } 
            else if (other.CompareTag("Enemy"))
            {
                _animator.SetTrigger("startAttacking");
                StartCoroutine(WaitForAnimationEnd());
            }
        }

        IEnumerator WaitForAnimationEnd()
        {
            yield return new WaitForSeconds(1.25f);
            _animator.SetTrigger("stopAttacking");
            _animator.SetBool("isMoving", false);
        }

        public void AttackTheEnemy()
        {
            
        }
        
    }
}

