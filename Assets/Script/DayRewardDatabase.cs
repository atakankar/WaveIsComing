using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardsDB", menuName = "Daily Reward System/Rewards Database" )]
public class DayRewardDatabase : ScriptableObject
{
    public int[] rewards;

    public int rewardsCount
    {
        get { return rewards.Length; }
    }

    public int GetReward(int index)
    {
        return rewards[index];
    }
}
