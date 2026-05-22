using UnityEngine;

public class EnviromentSounds : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int playedTimes=0;
    public int freq=10;
    public int playCondition=2;
    public AudioSource audioSource;
    bool hasStartedPlaying=false;
    void Start()
    {
        promptMessage="";
        taskActive=false;
        audioSource= GetComponent<AudioSource>();
    }
    void Update()
    {
        if (audioSource== null)return;
        if (hasStartedPlaying)
        {
            if (audioSource.isPlaying == false)
            {
                player.isMusicPlaying=false;
                    hasStartedPlaying=false;
                    playedTimes++;
            }
        }

    }
    protected override void enterState()
    {
        if(player.isMusicPlaying)return;
                if(playedTimes>freq)return;

        if (audioSource != null)
        {
            if (audioSource.isPlaying == false)
            {
                if (playedTimes % playCondition == 0)
                {
                    audioSource.Play();
                    player.isMusicPlaying=true;
                    hasStartedPlaying=true;
                }
                    
            }
        }
    }
}

