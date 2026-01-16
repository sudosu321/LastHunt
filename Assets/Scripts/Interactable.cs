using System;
using System.ComponentModel;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public String promptMessage ="";
    public bool taskActive=true;
    public void baseInteract()
    {
        Interact(); 
    }
    public bool canPickup = false;
    protected Rigidbody rb;
    protected Collider col;
    public PlayerHold player;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        
    }

    public virtual void Pickup(Transform playerhand)
    {
        //if(player.gunPick.gunHeld){promptMessage="cant hold multiple things";return;}
        if(!canPickup)return ;
        if (rb == null) return;
        if (col == null) return;
        
        rb.isKinematic = true;
        rb.useGravity = false;
        col.enabled =false;

        player.isPlayerHasItem=true;
        player.itemO=gameObject;
        player.itemT=transform;
        transform.SetParent(playerhand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    public virtual void destroyObj(GameObject obj)
    {
        Destroy(obj);
    }
    public virtual void OnDrop()
    {
        updatePick();    
    }
    public virtual void updatePick()
    {
        
    }
    protected virtual void Interact()
    {
        
    }
}
