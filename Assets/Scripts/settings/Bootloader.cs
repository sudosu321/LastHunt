using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLoader : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("main_menu");
    }
}
