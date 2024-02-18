using Rewards;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Options
{
    [CreateAssetMenu(fileName = "New Event", menuName = "Event")]
    public class OptionEvent : ScriptableObject
    {
        
        public enum EventOutCome
        {
            Success,
            Failure
        }
        
        public string optionName;
        public string eventType;
        public string description;
        [HideInInspector]
        public Stats stats;
        public Stats[] statList;
        public RewardList[] rewardList;
        [HideInInspector]
        public Tuple<string, int> rewardsObtained;
        public Sprite sprite;
        public AnimationClip[] animationClipList;
        public RuntimeAnimatorController runTimeAnimatorController;
        public float animationSpeed;
        public bool loop = true;
        public string[] dialog;
        public EventOutCome eventOutCome;

        public string? merchantType;

        public void EventSuccess()
        {
            stats = statList[0];
            rewardsObtained = AddRewardsByType(rewardList[0]);
            eventOutCome = EventOutCome.Success;
        }
        public void EventFailure()
        {
            stats = statList[1];
            rewardsObtained = AddRewardsByType(rewardList[1]);
            eventOutCome = EventOutCome.Failure;
        }

        private Tuple<string, int> AddRewardsByType(RewardList rewardsToBeObtained)
        {
            string allItems = "";
            int totalGold = 0;

            if (rewardsToBeObtained != null)
            {
                foreach (Reward reward in rewardsToBeObtained.rewards)
                {
                    if (reward.rewardType.ToLower() == "equipment")
                    {
                        allItems += reward.item + ", ";
                    }
                    else if (reward.rewardType.ToLower() == "currency")
                    {
                        totalGold += reward.quantity;
                    }
                    else if (reward.rewardType.ToLower() == "merchant"){
                        merchantType = reward.description;
                    }
                }
            }
            
            if (!string.IsNullOrEmpty(allItems))
            {
                allItems = allItems.Remove(allItems.Length - 2);
            }

            if (allItems.Equals(""))
            {
                allItems = "0";
            }

            return new Tuple<string, int>(allItems, totalGold);
        }
    }
}

