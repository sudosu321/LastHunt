using UnityEngine;

public class Ammo : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GunShoot gun;
    public int addAmount=15;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
       gun.bulletCount=gun.bulletCount+addAmount;
       gun.updateText();
       Destroy(gameObject);
    }
}
