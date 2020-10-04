using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ScoreMoneyManager : MonoBehaviour
{
    private static int score;
    private static int money;
        
    void Start()
    {
        score = 0;
        money = 0;
    }


    public static void AddScore(int i)
    {
        score += i;
    }

    public static int HowMuchScore()
    {
        return score;
    }


    public static void AddMoney(int i)
    {
        money += i;
    }

    public static int HowMuchMoney()
    {
        return money;
    }

    public static void RemoveMoney(int i)
    {
        money -= i;
    }

}
