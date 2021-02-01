using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    FirebaseAuth auth;
    DatabaseReference reference;

    [Header("UI Elements")]
    public Text userNameText;
    public Text userCoinText;
    public Text UserLevelText;
    public Image LevelBar;

    public static int coin;

    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }
    void Start()
    {

        if (auth.CurrentUser == null)
        {
            Debug.Log("UserFailed");
        }
        else
        {
            reference = FirebaseDatabase.DefaultInstance.RootReference;
            VerileriCek();
        }
    }

    public void VerileriCek()
    {
        reference.Child("Users").Child(auth.CurrentUser.UserId).Child("UserStatus").GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                BosVeriOlustur();
                Debug.Log("Database Hata");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.GetRawJsonValue() == null)
                {
                    Debug.Log("Boş");
                    BosVeriOlustur();
                    VerileriCek();
                }

                else
                {
                    //
                    Debug.Log(snapshot.GetRawJsonValue());
                    UserValues data = JsonUtility.FromJson<UserValues>(snapshot.GetRawJsonValue());
                    userNameText.text = data.UserName;
                    LevelSistem(data.exp);
                    userCoinText.text = data.UserCoin;
                }
            }
        });
    }

 
    void LevelSistem(string _exp)
    {
        float exp = float.Parse(_exp);
        if(exp<100)
        {
            LevelBar.fillAmount = exp / 100;
            UserLevelText.text = "1";
        }
        else if (100<=exp && exp < 200)
        {
            LevelBar.fillAmount = ((exp-100) / 100);
            UserLevelText.text = "2";
        }
        else if (200 <= exp && exp < 300)
        {
            LevelBar.fillAmount = (exp - 200) / 100;
            UserLevelText.text = "3";
        }
        else
        {
            LevelBar.fillAmount = 1;
            UserLevelText.text = "MAX";
        }
    }


    public void BosVeriOlustur()
    {
        UserValues bosveri = new UserValues
        {
            UserID = auth.CurrentUser.UserId.ToString(),
            UserName = auth.CurrentUser.DisplayName.ToString(),
            UserCoin = "0",
            lastLevel = "1",
            exp = "1",
            lastDailyRewardIndex = "0",
            lastDailyCailmDate = DateTime.Now.ToString(),
            UserQuestIndex = "0",
        };

        string bosJson = JsonUtility.ToJson(bosveri);
        reference.Child("Users").Child(auth.CurrentUser.UserId).Child("UserStatus").SetRawJsonValueAsync(bosJson);
    }
   
}
