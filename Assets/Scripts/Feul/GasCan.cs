using UnityEngine;

public class GasCan : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isFilled=false;
    void Start()
    {
        canPickup=true;
        if (isFilled)
        {
            promptMessage="Gas can filled";
        }
        else
        {
            promptMessage="can seems empty";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        if(player.isPlayerHasItem){
            player.drop();
        }
        player.isPlayerHasItem=true;
        player.itemO=gameObject;
        player.itemT=transform;
        player.rb=GetComponent<Rigidbody>();
        player.col=GetComponent<Collider>();
        Pickup(player.playerHand);
    }
}
