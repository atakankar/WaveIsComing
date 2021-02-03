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

public class CompleteLevel : MonoBehaviour
{
    FirebaseAuth auth;
    DatabaseReference reference;

    public string menuSceneName = "MainMenu";
    public SceneFader sceneFader;


    public string nextLevel ;
    public int levelToUnlock ;

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
        }
    }

    public void Continue()
    {
        
        LevelGuncelleme();
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo("LevelSelect");
    }

    public void Menu()
    {
        LevelGuncelleme();
        sceneFader.FadeTo(menuSceneName);
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
                    if (int.Parse(data.lastLevel) < levelToUnlock)

                    {
                        data.lastLevel = levelToUnlock.ToString();
                        reference.Child("Users").Child(auth.CurrentUser.UserId).Child("UserStatus").Child("lastLevel").SetValueAsync(data.lastLevel);
                    }

                    data.exp = (int.Parse(data.exp) + 10).ToString();
                    reference.Child("Users").Child(auth.CurrentUser.UserId).Child("UserStatus").Child("exp").SetValueAsync(data.exp);

                }
            }
        });
    }

    public void LevelGuncelleme()
    {
        VerileriCek();
    }
}
    

