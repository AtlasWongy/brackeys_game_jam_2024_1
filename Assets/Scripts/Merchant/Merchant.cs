using System.Collections;
using System.Collections.Generic;
using Singletons;
using UnityEngine;

namespace Merchant
{
    public class Merchant : MonoBehaviour
    {
        private Door.Door _door;

        private void Start()
        {
            _door = FindObjectOfType<Door.Door>();
            GameManager.OnDestroySignal += RemoveSelf;
        }
        
        public void InteractWIthPlayer()
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

