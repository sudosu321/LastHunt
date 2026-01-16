using UnityEngine;

public class WirePickable : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canPickup=true;
        promptMessage="A broken peice of wire";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        if (player.isPlayerHasItem)
        {
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
