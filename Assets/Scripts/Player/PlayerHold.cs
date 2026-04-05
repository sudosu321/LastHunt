using System;
using System.Linq.Expressions;
using Unity.AI.Navigation;
using UnityEngine;

public class PlayerHold : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isPlayerHasItem=false;
    public GameObject itemO;
    public Transform itemT;
    public Rigidbody rb;
    public Collider col;
    public bool gunDropped=true;
    public PickGun gunPick;
    public bool A1_FEUL_TASK=false;
    public bool SERVER_FEUL_TASK=false;

    public bool electricTaskComplete=false;
    public bool wireTaskComplete=false;

    public GameObject dropButton;
    public Transform playerHand;
    public bool DIESEL_TASK=false;
    public NavMeshSurface nav;
    public bool isCorruptedServerDestroyed=false;
    public int key_code=-1;
    public bool key_held=false;
    public bool isServerFeulFilled=false;

    public bool isSwitchHeld=false;
    public int switch_code=-1;
    public bool isMusicPlaying=false;
    public bool isTorchOn=false;
    public GameObject torchIcon;
    public GameObject torchLight;
    public GameObject torchItem;
    public GameObject paperObject;
    public bool isNoteOpened=false;
    public bool isIdCardOpened=false;

    public bool ispc=false;
        void Start()
    {
        
        electricTaskComplete=false;
    }
    public void bakeit()
    {
        Invoke("baker",5);
    }
    void baker()
    {
        nav.BuildNavMesh();
    }
    public void toggleTorch()
    {
        isTorchOn=!isTorchOn;
    }
    // Update is called once per frame
    void Update()
    {
       
        if (isPlayerHasItem)
        {
            dropButton.SetActive(true);
        }
        else
        {
            dropButton.SetActive(false);
            
        }
        if (isPlayerHasItem)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                drop();
            }
        }
    }
    void plasmaOnDrop()
    {
        GetComponent<PlasmaFill>().isPicked=false;
        GetComponent<PlasmaFill>().pickedMaterial=null;

    }
    public void dissappearHoldingItem()
    {
        isPlayerHasItem=false;
        Destroy(itemO);
    }
    public void drop()
    {
        if (gunPick.gunHeld)
        {
            gunPick.dropGun();
            return;
        }
        else
        {
            dropItem();       
            plasmaOnDrop();
        }
        
    }
    public void dropItem()
    {
        key_held=false;
        isSwitchHeld=false;
        key_code=-1;
        switch_code=-1;
        if (gunPick.gunHeld)
        {       
            gunPick.dropGun();
            return;
        }
        if (!isPlayerHasItem || itemT == null) return;

        itemT.SetParent(null);

        Rigidbody rb = itemT.GetComponent<Rigidbody>();
        Collider col = itemT.GetComponent<Collider>();

        rb.isKinematic = false;
        rb.useGravity = true;
        col.enabled = true;

        isPlayerHasItem = false;
    }

}
