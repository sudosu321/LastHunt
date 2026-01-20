using UnityEngine;

public class PlaceWire : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public WirePickable wire;
    public GameObject placeholderWire;
    public GameObject actualWire;
    void Start()
    {
        promptMessage="this wire is broken";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        if (!player.isPlayerHasItem)
        {
            promptMessage="wire is broken";
            return;
        }
        if (player.isPlayerHasItem)
        {
            if (player.itemT != null)
            {
                if (player.itemT.name.Contains("pickable"))
                {
                    promptMessage="circuit complete";
                    placeholderWire.SetActive(true);
                    player.dropItem();
                    Destroy(actualWire);
                    player.wireTaskComplete=true;
                    taskActive=false;
                }
                else
                {
                    promptMessage="needs a wire to connect";
                }
            }
            else
            {
                promptMessage="needs a wire to connect";
            }
        }

    }
}
