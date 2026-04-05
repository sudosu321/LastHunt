using UnityEngine;

public class PausePlay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject controls;
    public GameObject pauseIcon;
    public GameObject playIcon;
    PlayerHold player;
    public idcard idd;
    int i=1;
    void Start()
    {
        player=GetComponent<PlayerHold>();
    }
    public void handle()
    {
        if (player.isIdCardOpened)
        {
            idd.close();
            return;
        }
        if (player.isNoteOpened)
        {
            GetComponent<ExitNote>().OnClick();
            return;
        }
        i++;
        if (i % 2 == 0)
        {
            pause();
        }
        else
        {
            play();
        }
    }
    public void pause()
    {   
        if(controls!=null)
        controls.SetActive(false);
        Time.timeScale=0f;
        pauseIcon.SetActive(false);
        playIcon.SetActive(true);
        AudioListener.pause=true;
    }   
     public void play()
    {
        if(controls!=null)
        controls.SetActive(true);
        Time.timeScale=1f;
        pauseIcon.SetActive(true);
        playIcon.SetActive(false);
        AudioListener.pause=false;

        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
