

public class Electric : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TaskElectric taskElectric;
    bool thisOneDone=false;
    private ChangeWire wire;
    void Start()
    {
        wire=GetComponent<ChangeWire>();
        promptMessage="switch seems to be off";
    }
    protected override void Interact()
    
    {
        if (thisOneDone)
        {
            return;
        }
        if (taskElectric.plasmaFill.noOfWork >=4)
        {
            if(wire!=null)wire.change();
            taskElectric.incrementTask();
            thisOneDone=true;
            taskActive=false;promptMessage="switch turned on";
        }
        else
        {
            promptMessage="it doesnt have a power source";
            taskActive=false;
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
