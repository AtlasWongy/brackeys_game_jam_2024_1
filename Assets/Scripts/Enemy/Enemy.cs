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

        public void RemoveSelf()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _door.DropDoor();
        }
    }
}

