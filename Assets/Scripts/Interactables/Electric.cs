

using Unity.VisualScripting;
using UnityEngine;

public class Electric : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TaskElectric taskElectric;
    bool thisOneDone=false;
    private ChangeWire wire;
    public int powerBoxId=0;
    public bool[] powerboxState= new bool[4];
    void Start()
    {
        powerboxState[0]=false;
        powerboxState[1]=false;
        powerboxState[2]=false;
        powerboxState[3]=false;


        wire=GetComponent<ChangeWire>();
        promptMessage="switch seems to be off";
    }
    protected override void Interact()
    
    {
        if (thisOneDone)
        {
            return;
        }
        if (powerboxState[powerBoxId])
        {
            if(wire!=null)wire.change();
            taskElectric.incrementTask();
            thisOneDone=true;
            taskActive=false;
            promptMessage="switch turned on";
        
        }
        else
        {
            promptMessage="it doesnt have a power source";
        }
        if (GetComponent<AudioSource>()!= null)
        {
            GetComponent<AudioSource>().Play();
        }
    }
    void Update()
    {
       if (taskElectric.plasmaFill.noOfWork ==4)
        {
            if (!thisOneDone)
            {
                taskActive=true;
            }
        }
    }
}
