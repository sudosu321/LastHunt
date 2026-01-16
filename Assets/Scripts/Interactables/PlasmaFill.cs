
using UnityEngine;

public class PlasmaFill : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isPicked=false;
    public Material pickedMaterial;
    public int noOfWork=0;
    public Transform hand;
    public Transform holdingItem;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pickUp()
    {
        isPicked=true;
    }
    public void dissappearHoldingItem()
    {
        gameObject.GetComponent<PlayerHold>().dissappearHoldingItem();
    }
}
