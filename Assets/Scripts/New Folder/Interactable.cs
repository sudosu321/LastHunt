using System;
using System.ComponentModel;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public String promptMessage ="";

    public bool taskActive=true;
    public string objectCode;
    public AudioSource audioo;
    public bool defPlay=true;
    public void Start()
    {
       
    }
    public void interactableConnect()
    {
        enterState();
    }
    public void baseInteract()
    {
             if(!taskActive)return;
        if(audioo==null)
            audioo=GetComponent<AudioSource>();
        if(audioo!=null && defPlay)audioo.Play();
       
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
    protected virtual void enterState()
    {
        
    }
}
