using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static void GameOver()
    {
        BlackFader.GoToScene("GameOver", UnityEngine.SceneManagement.LoadSceneMode.Single, 1f);
    }
}
