using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;

public class QuestGiver : MonoBehaviour
{
    public Quest[] quest;
    public int lastQuestIndex;
    public int userCoin;
    public int userExp;
    public int LastLevel;

    FirebaseAuth auth;
    DatabaseReference reference;

    public Text titleText;
    public Text descriptionText;
    public Text CoinText;
    public Text XPtext;

    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    private void Start()
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

        titleText.text = quest[lastQuestIndex].Title;
        descriptionText.text = quest[lastQuestIndex].Description;
        CoinText.text = "+"+ quest[lastQuestIndex].CoinReward.ToString();
        XPtext.text = "+" + quest[lastQuestIndex].expReward.ToString();
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
                    lastQuestIndex = int.Parse(data.UserQuestIndex);
                    userCoin = int.Parse(data.UserCoin);
                    userExp = int.Parse(data.exp);
                    LastLevel = int.Parse(data.lastLevel);
                }
            }
        });
    }

    public void QuestClaim()
    {

    }

}
