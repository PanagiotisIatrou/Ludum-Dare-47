using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Buttons : MonoBehaviour
{
<<<<<<< Updated upstream
    //public Camera camera;
=======
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        if (scoreText != null)
            scoreText.SetText("Score: " + ScoreMoneyManager.score);
    }

>>>>>>> Stashed changes
    public void OnStartButtonListener()
    {
        BlackFader.GoToScene("Scene0", UnityEngine.SceneManagement.LoadSceneMode.Single, 1f);

    }

    public void OnCreditsButtonListener()
    {
        BlackFader.GoToScene("Credits", UnityEngine.SceneManagement.LoadSceneMode.Single, 1f);
    }

    public void OnExitButtonListener()
    {
        Application.Quit();
    }
    public void OnMainMenuButtonListener()
    {
        BlackFader.GoToScene("MainMenu", UnityEngine.SceneManagement.LoadSceneMode.Single, 1f);
    }
    public void OnHowToPlayListener()
    {
        BlackFader.GoToScene("GamePlay", UnityEngine.SceneManagement.LoadSceneMode.Single, 1f);
    }
}
