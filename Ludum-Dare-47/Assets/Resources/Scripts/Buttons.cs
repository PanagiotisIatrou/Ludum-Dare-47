using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    //public Camera camera;
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
