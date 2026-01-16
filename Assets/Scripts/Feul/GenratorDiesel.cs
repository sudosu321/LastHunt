
public class GenratorDiesel : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool feulTankDone=false;
    void Start()
    {
        taskActive=true;
        promptMessage="the feul tank";
    }
    protected override void Interact()
    {
        if (player.isPlayerHasItem)
        {
            if (player.gunPick.gunHeld)
            {
                promptMessage="It needs diesel";
                return;
            }
            if (player.itemT.name.Contains("MotorOil"))
            {
                if (player.itemO.GetComponent<GasCan>().isFilled)
                {
                    promptMessage="Genrator refilled";
                    player.itemO.GetComponent<GasCan>().isFilled=false;
                    player.itemO.GetComponent<GasCan>().promptMessage="gas can empty";
                    feulTankDone=true;
                    taskActive=false;
                    return;
                }
            }
            else
            {
                promptMessage="It needs diesel";
                return;
            }
        }
        else
        {
            promptMessage="feul tank seems empty";
            return;
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
