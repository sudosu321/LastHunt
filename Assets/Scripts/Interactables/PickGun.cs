using UnityEngine;
using UnityEngine.UIElements;

public class PickGun : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private GameObject gun;
    //  public PlayerHold player;
    public bool gunHeld;
    public GameObject shootButton;
    void Start()
    {
        promptMessage="MP5 submachine gun";
        canPickup=false;
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
        gunHeld=true;
        if(shootButton!=null)
        shootButton.SetActive(gunHeld);
        player.isPlayerHasItem=true;    
        gun.SetActive(true);
        gun.GetComponent<GunShoot>().updateText();

        transform.SetParent(player.transform);
        gameObject.SetActive(false);
    }
    public void     dropGun()
    {
        promptMessage="MP5 submachine gun";
        transform.SetParent(null); // ✅ unparent FIRST
        player.isPlayerHasItem=false;
        gun.SetActive(false);
        gameObject.SetActive(true);
        
      //  transform.SetParent(null);
        gunHeld=false;
        if(shootButton!=null)
            shootButton.SetActive(gunHeld);
    }
}
