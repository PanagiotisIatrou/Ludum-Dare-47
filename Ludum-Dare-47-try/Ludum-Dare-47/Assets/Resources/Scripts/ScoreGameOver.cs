using UnityEngine;
using TMPro;

public class ScoreGameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.SetText("Score: " + ScoreMoneyManager.score);
    }
}
