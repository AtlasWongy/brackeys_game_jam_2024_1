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

        public void InvokeEvent()
        {
            int diceResult = Random.Range(1, 11);
            
            if (eventType.ToLower().Equals("combat"))
            {
                if (diceResult <= 6)
                {
                    Debug.LogFormat("{0} selected! You lost! Stats affected: {1} health, {2} wits, {3} guts and {4} heart.", optionName, health, wits, guts, heart);
                }
                else
                {
                    Debug.LogFormat("{0} selected! You won! Stats affected: {1} health, {2} wits, {3} guts and {4} heart.", optionName, 0, wits, guts, heart);
                }
            } 
            else
            {
                Debug.LogFormat("{0} selected! Stats affected: {1} health, {2} wits, {3} guts and {4} heart.", optionName, health, wits, guts, heart);
            }
        }
    }
}

