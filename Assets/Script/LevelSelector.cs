using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader fader;
    public Button[] levelButtons;

    FirebaseAuth auth;
    DatabaseReference reference;

    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
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
        }        
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
                    UnlockLevel(int.Parse(data.lastLevel));
                }
            }
        });
    }

    public void UnlockLevel(int _last)
    {
        int levelReached = _last;
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }

        }
    }

    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
    }

    public void Home()
    {
        fader.FadeTo("mainMenu");
    }
}
