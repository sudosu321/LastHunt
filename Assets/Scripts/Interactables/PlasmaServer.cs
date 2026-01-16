using UnityEngine;

public class PlasmaServer : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform cube1;
    public Material transparentMaterial;
    public Material green;
    private PlasmaFill plasmaFill;//player
    bool alreadyWorks=false;
    void Awake()
    {
        plasmaFill = FindAnyObjectByType<PlasmaFill>();
    }

    void Start()
    {   
        canPickup=false;
        Renderer renderer = gameObject.GetComponent<Renderer>();
        
        if (renderer==null) {
            alreadyWorks=true;
            promptMessage="Synthesiser works";
            taskActive=false;
            
        }
        else if (renderer.sharedMaterial == transparentMaterial)
        {
            promptMessage="synthesiser needs plasma";
        }
        else
        {
            alreadyWorks=true;
            promptMessage="Synthesiser works";
            taskActive=false;
        }
    }

    // Update is called once per framez
    void Update()
    {
        
    }
    protected override void Interact()
    {
        if (plasmaFill == null) 
        {   
            promptMessage="playerfill null";
            return;
        }
        if (alreadyWorks)
        {
            promptMessage="Synthesiser works";
            taskActive=false;
            return;
        }
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if(renderer==null) return;
        if (plasmaFill.isPicked || alreadyWorks)
        {
            if(alreadyWorks)return;
            renderer.material=plasmaFill.pickedMaterial;
            taskActive=false;
            promptMessage="Synthesiser works";
            plasmaFill.noOfWork++;
            removeBlockFromHand();
            Renderer renderer1 = cube1.GetComponent<Renderer>();
            renderer1.material=green;
            plasmaFill.isPicked=false;
            alreadyWorks=true;
            if (gameObject.GetComponent<ChangeWire>() != null)
            {
                gameObject.GetComponent<ChangeWire>().change();
            }
        }
        else
        {
            promptMessage="find the plasma cube";
        }
    }
    public void removeBlockFromHand()
    {
        plasmaFill.dissappearHoldingItem();
    }
}
