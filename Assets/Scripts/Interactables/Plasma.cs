using Unity.VisualScripting;
using UnityEngine;

public class Plasma : Interactable
{
    public PlasmaFill plasmaFill;
    
    //public PlasmaServer plasmaServer;
    void Start()
    {
        canPickup=true;
        if (plasmaFill.isPicked)
        {
            promptMessage="cannot pick";
            
        }
        else
        {
            promptMessage="plasma cube";
            Debug.Log("works");
        }
    }

    // Update is called once per frame
    void Update()
    {
         if (player.gunPick.gunHeld)
        {
            promptMessage="gun held";
            return;
        }
        if (plasmaFill.isPicked)
        {
            promptMessage="cannot pick";
            taskActive=false;
        }
        else
        {
            promptMessage="plasma cube";
            taskActive=true;
        }
    }
    protected override void Interact()
    {
        if (player.isPlayerHasItem || player.gunPick.gunHeld)
        {
            exchange();
        }
        plasmaFill.pickUp();
        player.rb=GetComponent<Rigidbody>();
        player.col=GetComponent<Collider>();
        
        Renderer renderer = gameObject.GetComponent<Renderer>();
        plasmaFill.pickedMaterial=renderer.sharedMaterial;
        Pickup(plasmaFill.hand);
        //Destroy(gameObject);
    }
    void exchange()
    {
        player.drop();

    }
    public override void updatePick()
    {
        plasmaFill.isPicked=false;
    }
}
