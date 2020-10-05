using UnityEngine;

public class HelpMenuSkip : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.anyKey)
        {
            if (BlackFader.ready)
                BlackFader.GoToScene("Scene0", UnityEngine.SceneManagement.LoadSceneMode.Single, 0.5f);
        }
    }
}
