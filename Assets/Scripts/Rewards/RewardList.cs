using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New Rewards", menuName = "Rewards")]
    public class RewardList : ScriptableObject
    {
        public Reward[] rewards;
    }
}
