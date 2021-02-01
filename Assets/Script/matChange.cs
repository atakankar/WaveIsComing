using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
public class matChange : MonoBehaviour
{
    FirebaseAuth auth;
    DatabaseReference reference;

    skinBuyIndexandBool a;

    public GameObject StandartTurret;
    public GameObject StandartTurretUpgraded;
    public GameObject MissileLauncher;
    public GameObject LazerBeamer;

    public Material[] KlasikStandartHead;
    public Material[] KlasikStandartBase;
    public Button klasikStandartButton;
    public Button goldStandartButton;

    public Material[] Gold2li;
    public Material[] Gold3lu;


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
            SkinVerileriCek();
        }
    }

    public void ShowButton()
    {
        goldStandartButton.enabled = bool.Parse(IsPurched(0));
    }

   
    public void StandartTurretKlasikSkin()
    {
        StandartTurret.transform.Find("PartToRotate").Find("head").GetComponent<MeshRenderer>().materials = KlasikStandartHead;
        StandartTurret.transform.Find("base").GetComponent<MeshRenderer>().materials = KlasikStandartBase;
        klasikStandartButton.interactable = false;
        goldStandartButton.interactable = true;
    }

    public void StandartTurretGoldSkin()
    {
        StandartTurret.transform.Find("PartToRotate").Find("head").GetComponent<MeshRenderer>().materials = Gold3lu;
        StandartTurret.transform.Find("base").GetComponent<MeshRenderer>().materials = Gold2li;
        klasikStandartButton.interactable = true;
        goldStandartButton.interactable = false;
    }

    public void SkinVerileriCek()
    {
        reference.Child("Users").Child(auth.CurrentUser.UserId).Child("Skin").GetValueAsync().ContinueWithOnMainThread(task => {
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
                    SkinVerileriCek();
                }

                else
                {
                    skinBuyIndexandBool data = JsonUtility.FromJson<skinBuyIndexandBool>(snapshot.GetRawJsonValue());
                    a = data;
                }
            }
        });
    }

    public class skinBuyIndexandBool
    {
        public bool[] buyStatus = new bool[10];
    }

    public string IsPurched(int index)
    {
        return a.buyStatus[index].ToString();
    }
}
