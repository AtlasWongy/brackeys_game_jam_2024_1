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

        public RewardList rewardList;

        [HideInInspector]
        public Tuple<string, int> rewardsObtained;

        public Sprite sprite;

        public void EventSuccess()
        {
            if (eventType.ToLower().Equals("combat"))
            {
                stats.Health = 0;
            }
            rewardsObtained = AddRewardsByType(rewardList);
        }
        public void EventFailure(){
            rewardsObtained = AddRewardsByType(null);
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

