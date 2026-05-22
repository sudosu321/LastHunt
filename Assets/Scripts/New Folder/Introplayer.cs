using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroPlayer : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public bool intro=true;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.Play();
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        if (intro)
        {
        SceneManager.LoadScene("main_game");
            
        }
        else
        {
        SceneManager.LoadScene("main_menu");
            
        }
    }
}