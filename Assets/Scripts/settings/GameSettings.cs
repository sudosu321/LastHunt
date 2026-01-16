using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;

    [Header("Game Modes")]
    public bool debugMode = false;

    [Header("Audio")]
    [Range(0f, 1f)]
    public float masterVolume = 1f;

    [Header("Controls")]
    [Range(0.1f, 10f)]
    public float mouseSensitivity = 2f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
