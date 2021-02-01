using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [Header("Overleys")]
    public GameObject playOvelay;
    public GameObject SkinSelectOvelay;
    public GameObject ShopOvelay;
    public GameObject DayRewardsOvelay;
    public GameObject MissionOvelay;

    public void ShowPlayOverlay()
    {
        playOvelay.SetActive(true);
        SkinSelectOvelay.SetActive(false);
        ShopOvelay.SetActive(false);
        DayRewardsOvelay.SetActive(false);
        MissionOvelay.SetActive(false);
    }

    public void ShowSkinSelectOverlay()
    {
        playOvelay.SetActive(false);
        SkinSelectOvelay.SetActive(true);
        ShopOvelay.SetActive(false);
        DayRewardsOvelay.SetActive(false);
        MissionOvelay.SetActive(false);
    }

    public void ShowShopOverley()
    {
        playOvelay.SetActive(false);
        SkinSelectOvelay.SetActive(false);
        ShopOvelay.SetActive(true);
        DayRewardsOvelay.SetActive(false);
        MissionOvelay.SetActive(false);
    }

    public void ShowDayRewardsOverlay()
    {
        playOvelay.SetActive(false);
        SkinSelectOvelay.SetActive(false);
        ShopOvelay.SetActive(false);
        DayRewardsOvelay.SetActive(true);
        MissionOvelay.SetActive(false);
    }

    public void ShowMissionOverlay()
    {
        playOvelay.SetActive(false);
        SkinSelectOvelay.SetActive(false);
        ShopOvelay.SetActive(false);
        DayRewardsOvelay.SetActive(false);
        MissionOvelay.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
