using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Update is called once per frame
    void Update()
    {
        text.SetText("Money: " + ScoreMoneyManager.HowMuchMoney().ToString());
    }
}
