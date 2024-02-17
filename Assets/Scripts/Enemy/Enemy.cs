using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public void RemoveSelf()
        {
            Destroy(this);
        }
    }
}

