using System;
using System.Collections;
using System.Collections.Generic;
using NPC;
using Singletons;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Animator _animator;
        private Enemy.Enemy _enemy;
        private NpcInteraction _npcInteraction;
        private Merchant.Merchant _merchant;
        private bool _isMoving;
        
        public static PlayerMovement Instance { get; private set; }
        public bool _playerWins;
        
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
            _npcInteraction = FindObjectOfType<NpcInteraction>();
            _merchant = FindObjectOfType<Merchant.Merchant>();
            
            if (_enemy != null)
            {
                _playerWins = GameManager.Instance.playerWinsEncounter;
                Instance._isMoving = true;
            } 
            else if (_npcInteraction != null)
            {
                Instance._isMoving = true;
            }
            else if (_merchant != null)
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
            Debug.Log($"The player win the encounter: {_playerWins} from ${other.name}");
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
                if (_playerWins)
                {
                    _animator.SetTrigger("startAttacking");
                    StartCoroutine(WaitForAnimationEnd());
                }
                else
                {
                    _animator.SetBool("isMoving", false);
                    _enemy.AttackPlayer();
                }
            }
            else if (other.CompareTag("NPC"))
            {
                _animator.SetBool("isMoving", false);
                _npcInteraction.InteractWithPlayer();
            }
            else if (other.CompareTag("Merchant"))
            {
                _animator.SetBool("isMoving", false);
                _merchant.InteractWIthPlayer();
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

