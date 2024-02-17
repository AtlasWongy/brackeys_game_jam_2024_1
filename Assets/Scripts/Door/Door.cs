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
        public void DropDoor()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        public void EnterNextScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}

