using UnityEngine;
using TMPro;

public class ScoreGameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        Time.timeScale = 1f;
        scoreText.SetText("Score: " + ScoreMoneyManager.score);
        ScoreMoneyManager.score = 0;
    }
}
