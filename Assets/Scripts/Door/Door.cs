using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Door
{
    public class Door : MonoBehaviour
    {
        private PlayerMovement _playerMovement;

        private void Start()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
        }

        public void DropDoor()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            _playerMovement.TriggerMovement();
        }

        public void EnterNextScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}

