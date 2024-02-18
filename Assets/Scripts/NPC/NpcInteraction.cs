using System;
using System.Collections;
using System.Collections.Generic;
using Singletons;
using Unity.VisualScripting;
using UnityEngine;

namespace NPC
{
    public class NpcInteraction : MonoBehaviour
    {
        private Door.Door _door;

        private void Start()
        {
            _door = FindObjectOfType<Door.Door>();
            GameManager.OnDestroySignal += RemoveSelf;
        }
        
        public void InteractWithPlayer()
        {
            GameManager.Instance.HandleDialog();
        }

        private void RemoveSelf()
        {
            Destroy(gameObject);
        }
        

        private void OnDestroy()
        {
            _door.DropDoor();
            GameManager.OnDestroySignal -= RemoveSelf;
        }
    }
}

