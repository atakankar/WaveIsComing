using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;

public class DayRewards : MonoBehaviour
{

    [SerializeField] GameObject rewardActiveCanvas;
    [SerializeField] GameObject noMoreReward;
    [SerializeField] DayRewardDatabase rewardDB;
    [SerializeField] Text rewardAmount;
    [SerializeField] double nextRewardDelay = 20f;

    private int nextRewardIndex;
    private int playerCoin;
    private DateTime lastClaimDay;


    FirebaseAuth auth;
    DatabaseReference reference;
    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    private void Update()
    {
        ClaimDate();
    }

    public void Start()
    {

        if (auth.CurrentUser == null)
        {
            Debug.Log("UserFailed");
        }
        else
        {
            reference = FirebaseDatabase.DefaultInstance.RootReference;
            VerileriCek();
            ClaimDate();
        }
    }

    void ActivateReward()
    {
        noMoreReward.SetActive(false);
        rewardActiveCanvas.SetActive(true);
        rewardAmount.text = "+"+rewardDB.GetReward(nextRewardIndex).ToString();
    }

    void DesactivateReward()
    {
        noMoreReward.SetActive(true);
        rewardActiveCanvas.SetActive(false);
    }

    public void VerileriCek()
    {
        reference.Child("Users").Child(auth.CurrentUser.UserId).Child("UserStatus").GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                Debug.Log("Database Hata");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.GetRawJsonValue() == null)
                {
                    Debug.Log("Boş");
                    VerileriCek();
                }

                else
                {
                    //
                    Debug.Log(snapshot.GetRawJsonValue());
                    UserValues data = JsonUtility.FromJson<UserValues>(snapshot.GetRawJsonValue());
                    nextRewardIndex = int.Parse(data.lastDailyRewardIndex);
                    playerCoin = int.Parse(data.UserCoin);
                    lastClaimDay = DateTime.Parse(data.lastDailyCailmDate);
                }
            }
        });
    }

    public void ClaimReward()
    {
        int reward = rewardDB.GetReward(nextRewardIndex);
        playerCoin += reward;
        nextRewardIndex++;
        if( nextRewardIndex >= rewardDB.rewardsCount)
        {
            nextRewardIndex = 0;
        }
        reference.Child("Users").Child(auth.CurrentUser.UserId).Child("UserStatus").Child("UserCoin").SetValueAsync(playerCoin);
        reference.Child("Users").Child(auth.CurrentUser.UserId).Child("UserStatus").Child("lastDailyRewardIndex").SetValueAsync(nextRewardIndex);
        reference.Child("Users").Child(auth.CurrentUser.UserId).Child("UserStatus").Child("lastDailyCailmDate").SetValueAsync(DateTime.Now.ToString());
        DesactivateReward();
        VerileriCek();
    }

    public void ClaimDate()
    {
        DateTime curruntDateTime = DateTime.Now;

        double elapsedSeconds = (curruntDateTime - lastClaimDay).TotalSeconds;
        if (elapsedSeconds >= nextRewardDelay)
        {
            ActivateReward();
        }
        else
        {
            DesactivateReward();
        }
    }

}
