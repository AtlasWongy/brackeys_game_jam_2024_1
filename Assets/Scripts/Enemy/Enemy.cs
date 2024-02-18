using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        private Door.Door _door;

        private void Start()
        {
            _door = FindObjectOfType<Door.Door>();
        }

        public void AttackPlayer()
        {
            GetComponent<Animator>().Play("Goblin_Attack");
            StartCoroutine(WaitForAnimationEnd());
        }

        public void RemoveSelf()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            GameManager.Instance.EnableButtons();
            GameManager.Instance.UpdateUIStatText();
            _door.DropDoor();
        }
        
        IEnumerator WaitForAnimationEnd()
        {
            yield return new WaitForSeconds(1.25f);
           RemoveSelf();
        }
    }
}

