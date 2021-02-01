using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
public class SkinShop : MonoBehaviour
{
    FirebaseAuth auth;
    DatabaseReference reference;
    skinBuyIndexandBool a;
    GameObject ItemTemplate;
    GameObject g;
    Button BuyBT;
    [SerializeField] Transform ShopScrollView;
    [SerializeField] public List<SkinShopItem> SkinList;

   
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
        ItemTemplate = ShopScrollView.Find("ShopButton").gameObject;
        int len = SkinList.Count;
        for(int i=0;i<len; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.SetActive(true);
            g.transform.Find("SkinImage").GetComponent<Image>().sprite = SkinList[i].Image;
            g.transform.Find("CoinPrice").GetComponent<Text>().text = SkinList[i].Price.ToString();
            BuyBT = g.transform.Find("Button").GetComponent<Button>();
            BuyBT.interactable = !SkinList[i].IsPurchased;
            BuyBT.AddEventListener(i, OnShopItemBtnClicked);
        }
    }
    
    public void SkinVerileriCek()
    {
        reference.Child("Users").Child(auth.CurrentUser.UserId).Child("Skin").GetValueAsync().ContinueWithOnMainThread(task => {
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
                    SkinVerileriCek();
                }

                else
                {
                    skinBuyIndexandBool data = JsonUtility.FromJson<skinBuyIndexandBool>(snapshot.GetRawJsonValue());
                    a = data;
                    Debug.Log(a.buyStatus[1]);
                }
            }
        });
    }

    public string IsPurched(int index)
    {
        return a.buyStatus[index].ToString();
    }

    public void BosVeriOlustur()
    {
        skinBuyIndexandBool bosveri = new skinBuyIndexandBool();
        for(int i = 0; i < SkinList.Count; i++)
        {
            bosveri.buyStatus[i] = false;
        }
        string bosJson = JsonUtility.ToJson(bosveri);
        reference.Child("Users").Child(auth.CurrentUser.UserId).Child("Skin").SetRawJsonValueAsync(bosJson);
    }

    void OnShopItemBtnClicked(int itemIndex)
    {
        reference.Child("Users").Child(auth.CurrentUser.UserId).Child("Skin").Child("buyStatus").Child(itemIndex.ToString()).SetValueAsync("true");
        SkinList[itemIndex].IsPurchased = true;
        ShopScrollView.GetChild(itemIndex+1).Find("Button").GetComponent<Button>().interactable=false;
    }

    public class skinBuyIndexandBool
    {
        public bool[] buyStatus = new bool[10];
    }

}


