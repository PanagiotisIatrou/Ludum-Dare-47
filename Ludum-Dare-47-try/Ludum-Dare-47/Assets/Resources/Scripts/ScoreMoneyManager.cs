using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ScoreMoneyManager : MonoBehaviour
{

    private static int score;   
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
