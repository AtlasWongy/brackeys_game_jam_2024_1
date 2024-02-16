using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Rewards
{
    [Serializable, Inspectable]
    public class Reward
    {
        [Inspectable]
        public string rewardType;

        [Inspectable]
        public int gold;

        [Inspectable]
        public string item;

        [Inspectable]
        public string description;
    }
}

