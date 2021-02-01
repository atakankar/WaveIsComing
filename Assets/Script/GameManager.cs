using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;

    public GameObject gameOverUI;
    public GameObject gameWinUI;


    void Start()
    {
        GameIsOver = false;
    }
    void Update()
    {
        //Oyun bitti mi kontrolü
        if (GameIsOver)
            return;

        if (Input.GetKeyDown("e"))
        {            
            EndGame();
        }


        //kalan canların kontrolü
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    //Oyunu bitirme
    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        GameIsOver = true;
        gameWinUI.SetActive(true);
    }
}
