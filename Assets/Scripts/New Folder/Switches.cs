using UnityEngine;

public class Switches : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int switch_code=1;
    void Start()
    {
        canPickup=true;
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
        player.isSwitchHeld=true;
        player.switch_code=switch_code;
    }
    public void destruct()
    {
        player.isSwitchHeld=false;
        player.switch_code=-1;
        player.isPlayerHasItem=false;
        Destroy(player.itemO);
    }
}
