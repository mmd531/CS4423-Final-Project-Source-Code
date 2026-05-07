using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    public string firstSceneName = "Scene1";

    public void BeginGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(firstSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}