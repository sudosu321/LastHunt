using UnityEngine;

public class KeyCard : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int KEY=-1;//0 - red , 1 - yellow , 2 - blue
    void Start()
    {
        
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
        player.key_held=true;
        player.key_code=KEY;
    }
}
