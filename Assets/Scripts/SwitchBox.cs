using UnityEngine;

public class SwitchBox : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject switch1;
    public GameObject switch2;

    public GameObject switch3;
    public byte switchesTotal=0;

    void Start()
    {
        promptMessage="box has missing plugs";
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected override void Interact()
    {
        if (switchesTotal == 3)
        {
            switchesTotal++;
            taskActive=false;
            promptMessage="electricity on";
            return;
        }
        if (!player.isPlayerHasItem) return;

        if (player.itemT == null) return;

        if (!player.itemT.name.Contains("switch"))
        {
            switchesTotal++;
            switch (player.itemO.GetComponent<Switches>().switch_code)
            {
                case 1:
                    switch1.SetActive(true);
                    break;
                case 2:
                    switch2.SetActive(true);
                    break;
                case 3:
                    switch3.SetActive(true);
                    break;
            }
            player.itemO.GetComponent<Switches>().destruct();
            if (switchesTotal == 3)
            {
                taskActive=true;
                promptMessage="box repaired";
            }
    
        }
    }
}

