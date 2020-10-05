using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Update()
    {
        text.SetText("Score: "+ScoreMoneyManager.HowMuchScore().ToString());
    }
}
