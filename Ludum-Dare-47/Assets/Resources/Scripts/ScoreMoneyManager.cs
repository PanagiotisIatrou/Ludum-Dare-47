using UnityEngine;

public class ScoreMoneyManager : MonoBehaviour
{
    public static int score;   

    void Start()
    {
        score = 0;
    }
    public static void AddScore(int i)
    {
        score += i;
    }
    public static int HowMuchScore()
    {
        return score;
    }
}
