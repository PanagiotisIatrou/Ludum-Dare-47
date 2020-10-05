using UnityEngine;

public class Buttons : MonoBehaviour
{
    public void OnStartButtonListener()
    {
        BlackFader.GoToScene("Scene0", UnityEngine.SceneManagement.LoadSceneMode.Single, 0.5f);
    }
    public void OnHelp2ButtonListener()
    {
        BlackFader.GoToScene("Help2", UnityEngine.SceneManagement.LoadSceneMode.Single, 0.5f);
    }

    public void OnCreditsButtonListener()
    {
        BlackFader.GoToScene("Credits", UnityEngine.SceneManagement.LoadSceneMode.Single, 0.5f);
    }

    public void OnExitButtonListener()
    {
        Application.Quit();
    }
    public void OnMainMenuButtonListener()
    {
        BlackFader.GoToScene("MainMenu", UnityEngine.SceneManagement.LoadSceneMode.Single, 0.5f);
    }
    public void OnHowToPlayListener()
    {
        BlackFader.GoToScene("GamePlay", UnityEngine.SceneManagement.LoadSceneMode.Single, 0.5f);
    }
}
