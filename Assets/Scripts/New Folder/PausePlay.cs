using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePlay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject controls;
    public GameObject pauseIcon;
    public GameObject playIcon;
    PlayerHold player;
    public PlayerLook look;

    public idcard idd;
    public Slider vol;
    public Slider sensi;
    public PlayerLook playerLook;
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
        look.enabled=false;
    }   
     public void play()
    {
        look.enabled=true;

        if(controls!=null)
        controls.SetActive(true);
        Time.timeScale=1f;
        pauseIcon.SetActive(true);
        playIcon.SetActive(false);
        AudioListener.pause=false;

        
    }
    public void onVolChange()
    {
        AudioListener.volume = vol.value;
    }
    public void onSensiChange()
    {
        playerLook.sensitivity=sensi.value/10;
    }
    public void exit()
    {
        Time.timeScale=1f;
        AudioListener.pause=false;
        
        SceneManager.LoadScene("main_menu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
