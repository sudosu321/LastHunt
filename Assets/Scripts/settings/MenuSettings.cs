using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public void SetVolume(float value)
    {
        GameSettings.Instance.masterVolume = value;
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }

    public void SetSensitivity(float value)
    {
        GameSettings.Instance.mouseSensitivity = value;
        PlayerPrefs.SetFloat("Sensitivity", value);
    }

    public void Back()
    {
        SceneManager.LoadScene("main_menu");
    }
}
