using UnityEngine;

public class Ammo : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GunShoot gun;
    public int addAmount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
       gun.bulletCount=gun.bulletCount+10;
       Destroy(gameObject);
    }
}
