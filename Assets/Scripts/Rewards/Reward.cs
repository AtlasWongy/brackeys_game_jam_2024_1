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
        public string rewardType;

        public string item;

        public int quantity;

        public string description;
    }

    //public interface Item
    //{
    //    string GetItemDetails();
    //}
    
    //public class Gold : Item
    //{
    //    public int Amount { get; set; }

    //    public string GetItemDetails()
    //    {
    //        return Amount.ToString();
    //    }
    //}

    //public class Equipment : Item
    //{
    //    public string Type { get; set; }

    //    public string GetItemDetails()
    //    {
    //        return Type;
    //    }
    //}
}

