using UnityEngine;

public class cuttableWire : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject actualWire;
    void Start()
    {
        promptMessage="seems i can cut this wire";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        if (!player.isPlayerHasItem)
        {
            promptMessage="i need to cut this wire";
            return;
        }
        if (player.isPlayerHasItem)
        {
            if (player.itemT != null)
            {
                if (player.itemT.name.Contains("Plier"))
                {
                    promptMessage="wire cut";
                    Destroy(actualWire);
                    taskActive=false;
                    player.isCorruptedServerDestroyed=true;
                }
                else
                {
                    promptMessage="i need to cut this wire";
                }
            }
            else
            {
                promptMessage="i need to cut this wire";
            }
        }

    }
}
