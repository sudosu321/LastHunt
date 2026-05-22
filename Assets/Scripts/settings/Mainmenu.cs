using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        GameSettings.Instance.debugMode = false;
        intro();
    }
    public void intro()
    {
        SceneManager.LoadScene("menu_selector");
    }

    public void credits()
    {
       SceneManager.LoadScene("credits");

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
