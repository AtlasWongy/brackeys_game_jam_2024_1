using System.Collections;
using System.Collections.Generic;
using Rewards;
using UnityEngine;

namespace Options
{
    [CreateAssetMenu(fileName = "New Event", menuName = "Event")]
    public class OptionEvent : ScriptableObject
    {
        public string optionName;
        public string eventType;
        public string description;

        public int health;
        public int wits;
        public int guts;
        public int heart;

        public Reward[] Reward;
    }
}

