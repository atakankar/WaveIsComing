using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public Text LivesText;

    void Update()
    {
        LivesText.text = "Lives:" + PlayerStats.Lives.ToString();
    }
}
