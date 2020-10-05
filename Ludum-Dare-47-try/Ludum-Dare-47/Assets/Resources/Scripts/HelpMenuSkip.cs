using UnityEngine;

public class HelpMenuSkip : MonoBehaviour
{
    private bool skipped = false;

    private void Update()
    {
        if (!skipped && (Input.GetMouseButtonDown(0) || Input.anyKey))
        {
            skipped = true;
            BlackFader.GoToScene("Scene0", UnityEngine.SceneManagement.LoadSceneMode.Single, 0.5f);
        }
    }
}
