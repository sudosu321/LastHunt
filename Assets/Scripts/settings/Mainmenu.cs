using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        GameSettings.Instance.debugMode = false;
        SceneManager.LoadScene("main_game");
    }

    public void DebugPlay()
    {
        GameSettings.Instance.debugMode = true;
        SceneManager.LoadScene("main_game");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("settings");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
