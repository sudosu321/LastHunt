using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class computer : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ComputerMain computerMain;
    public GameObject compui;
    public GameObject mainui;
    public Enemy enemy;
    public GameObject textField;
    void Start()
    {
        
    }
    protected override void Interact()
    {
        
        if (!computerMain.computerStart)
        {
            promptMessage="this computer isnt on";
            return;
        }
        if (!player.SERVER_FEUL_TASK)
        {
            promptMessage="Computer needs to be connected to server";
            return;
        }
        if (!player.isCorruptedServerDestroyed)
        {
            promptMessage="There is a corrupted server that needs to be shut";
            return;
        }
        
        EnterSerialCode();
    }
    void EnterSerialCode()
    {   
        player.GetComponent<PlayerMovement>().enabled=false;
        player.GetComponentInChildren<PlayerLook>().enabled=false;

        OpenComputer();
    }
    public void onOkay()
    {
        if (textField.GetComponent<TMP_InputField>().text.Contains("4AX7"))
        {
            DisableSecurity();
        }
        else
        {
            textField.GetComponent<TMP_InputField>().text="WRONG...";
            enemy.security=true;
            enemy.explicitDiscover=true;
            enemy.pos=transform.position;
        }
    }
    void OpenComputer()
    {
        compui.SetActive(true);
        mainui.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        
    }

    void OnTriggerExit(Collider other)
    {
    }

    public void DisableSecurity()
    {
        enemy.security=false;
        promptMessage="Security Disabled for 60s";
        taskActive=false;
        Invoke("activate",60);
        CloseComputer();
    }
    void activate()
    {
        enemy.security=true;
        promptMessage="super computer that probably controls the robot";
        taskActive=true;
    }
    public void CloseComputer()
    {
        compui.SetActive(false);
        mainui.SetActive(true);
        player.GetComponent<PlayerMovement>().enabled=true;
        player.GetComponentInChildren<PlayerLook>().enabled=true;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
