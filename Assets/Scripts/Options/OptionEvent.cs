using Rewards;
using System;
using UnityEngine;

namespace Options
{
    [CreateAssetMenu(fileName = "New Event", menuName = "Event")]
    public class OptionEvent : ScriptableObject
    {
        public string optionName;
        public string eventType;
        public string description;

        public Stats stats;
        //public int health;
        //public int wits;
        //public int guts;
        //public int heart;
        //public int good;
        //public int evil;

        public RewardList rewardList;
 
    }
}

