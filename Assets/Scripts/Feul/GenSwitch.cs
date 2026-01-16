using UnityEngine;

public class GenSwitch : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GenratorDiesel Genrator;
    public GameObject lights;
    public GameObject enemy;
    public GameObject showPiece;
    public AudioSource gen;
    void Start()
    {
        promptMessage="generator start button";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        if (!Genrator.feulTankDone)
        {
            promptMessage="feul tank empty";
            return;
        }
        promptMessage="Genrator started, lights are on";
        if(!gen.isPlaying)gen.Play();
        player.DIESEL_TASK=true;
        taskActive=false;
        switchOnLightsI();
    }
    void switchOnLightsI()
    {
        lights.SetActive(true);
        Destroy(showPiece);
        Invoke("spawn",10);

    }
    void spawn()
    {
        enemy.SetActive(true);
    }
}
