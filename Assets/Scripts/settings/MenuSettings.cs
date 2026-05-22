using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.GlobalIllumination;

public class SettingsMenu : MonoBehaviour
{
     public TMP_Dropdown gr;
   

    void Start()
    {
        if (GameSettings.Instance != null)
        {
            gr.value=GameSettings.Instance.graphics;
        }
    }
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
    public void setGraphics()
    {
         int val = gr.value;
        // 0 = Practice, 1 = Easy, 2 = Extreme
         PlayerPrefs.SetInt("Graphics", val);
        GameSettings.Instance.graphics = val;
    }
}
