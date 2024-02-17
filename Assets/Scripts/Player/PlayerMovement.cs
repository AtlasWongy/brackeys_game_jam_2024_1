using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Animator _animator;
        private Enemy.Enemy _enemy;
        private bool _isMoving;
        
        public static PlayerMovement Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _enemy = FindObjectOfType<Enemy.Enemy>();
            if (_enemy != null)
            {
                Instance._isMoving = true;
            }
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
            Instance._isMoving = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other != null)
            {
                Instance._isMoving = false;
            }
            
            if (other.CompareTag("Door"))
            {
                _animator.SetBool("isMoving", false);
                other.GetComponent<Door.Door>().EnterNextScene(1);
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
            _enemy.RemoveSelf();
        }
    }
}

