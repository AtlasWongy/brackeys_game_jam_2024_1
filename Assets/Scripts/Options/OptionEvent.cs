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

        public void EventSuccess()
        {
            //NOT VOID, SHOULD RETURN SOMETHING TO THE GAME MANAGER
            if (eventType.ToLower().Equals("combat"))
            {
                stats.Health = 0;
            }
            rewardsObtained = AddRewardsByType(rewardList);
        }
        public void EventFailure(){
            //NOT VOID, SHOULD RETURN SOMETHING TO THE GAME MANAGER
            rewardsObtained = AddRewardsByType(null);
        }

        //public void InvokeEvent()
        //{
        //    //NOTE: ALL FUTURE DICE ROLLS RESOLVED AT PLAYER LEVEL
        //    int diceResult = UnityEngine.Random.Range(1, 11);
            
        //    if (eventType.ToLower().Equals("combat"))
        //    {
        //        if (diceResult <= 6)
        //        {
        //            Debug.LogFormat("{0} selected! You lost! Stats affected: {1} health, {2} wits, {3} guts, {4} heart, {5} good and {6} evil.", optionName, stats.Health, stats.Wits, stats.Guts, stats.Heart, stats.Good, stats.Evil);
        //        }
        //        else
        //        {
        //            Tuple<string, int> rewardsObtained = AddRewardsByType();
        //            Debug.LogFormat("{0} selected! You won! Stats affected: {1} health, {2} wits, {3} guts, {4} heart, {5} good and {6} evil. You gained: {7} gold and {8} items.", optionName, 0, stats.Wits, stats.Guts, stats.Heart, stats.Good, stats.Evil, rewardsObtained.Item2, rewardsObtained.Item1);
        //        }
        //    } 
        //    else
        //    {
        //        Debug.LogFormat("{0} selected! Stats affected: {1} health, {2} wits, {3} guts and {4} heart.", optionName, stats.Health, stats.Wits, stats.Guts, stats.Heart);
        //    }
        //}

        private Tuple<string, int> AddRewardsByType(RewardList rewardsToBeObtained)
        {
            // Variables to store the accumulated values
            string allItems = "";
            int totalGold = 0;

            if (rewardsToBeObtained != null)
            {
                // Iterate over the rewards list
                foreach (Reward reward in rewardsToBeObtained.rewards)
                {
                    // If the type is "item", append the name
                    if (reward.rewardType.ToLower() == "equipment")
                    {
                        allItems += reward.item + ", ";
                    }
                    // If the type is "gold", add the gold value
                    else if (reward.rewardType.ToLower() == "currency")
                    {
                        totalGold += reward.quantity;
                    }
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

