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

        public int health;
        public int wits;
        public int guts;
        public int heart;

        public RewardList rewardList;

        public void InvokeEvent()
        {
            int diceResult = UnityEngine.Random.Range(1, 11);
            
            if (eventType.ToLower().Equals("combat"))
            {
                if (diceResult <= 6)
                {
                    Debug.LogFormat("{0} selected! You lost! Stats affected: {1} health, {2} wits, {3} guts and {4} heart.", optionName, health, wits, guts, heart);
                }
                else
                {
                    Tuple<string, int> rewardsObtained = AddRewardsByType();
                    Debug.LogFormat("{0} selected! You won! Stats affected: {1} health, {2} wits, {3} guts and {4} heart. You gained: {5} gold and {6} items.", optionName, 0, wits, guts, heart, rewardsObtained.Item2, rewardsObtained.Item1);
                }
            } 
            else
            {
                Debug.LogFormat("{0} selected! Stats affected: {1} health, {2} wits, {3} guts and {4} heart.", optionName, health, wits, guts, heart);
            }
        }

        private Tuple<string, int> AddRewardsByType()
        {
            // Variables to store the accumulated values
            string allItems = "";
            int totalGold = 0;

            // Iterate over the rewards list
            foreach (Reward reward in rewardList.rewards)
            {
                // If the type is "item", append the name
                if (reward.rewardType == "item")
                {
                    allItems += reward.item + ", ";
                }
                // If the type is "gold", add the gold value
                else if (reward.rewardType == "gold")
                {
                    totalGold += reward.gold;
                }
            }

            // Remove the last comma and space from the item names
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

