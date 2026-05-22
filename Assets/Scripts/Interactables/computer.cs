using System.Collections;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class computer : Interactable
{
    public ComputerMain computerMain;
    public GameObject compui;
    public GameObject mainui;
    public Enemy[] enemy;
    public Enemy dummy;

    public GameObject textField;
    public int delaysec = 60;
    public DOOR_OPENER door;
    public bool bypass = true;
    
    private TMP_InputField inputField;
    public AudioSource not;
    public AudioSource ok;
    public NavMeshSurface navMeshSurface;
    public    GameObject GLASS;
    public AudioSource BLAST;
    void Start()
    {
        inputField = textField.GetComponent<TMP_InputField>();
    }

    protected override void Interact()
    {
        if (bypass)
        {
            EnterSerialCode();
            return;
        }
        if (!computerMain.computerStart)
        {
            promptMessage = "This computer isn't on";
            return;
        }
        if (!player.SERVER_FEUL_TASK)
        {
            promptMessage = "Computer needs to be connected to server";
            return;
        }
        if (!player.isCorruptedServerDestroyed)
        {
            promptMessage = "There is a corrupted server that needs to be shut down";
            return;
        }

        EnterSerialCode();
    }

    void EnterSerialCode()
    {
        player.GetComponent<PlayerInteract>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponentInChildren<PlayerLook>().enabled = false;
        OpenComputer();
    }

    void OpenComputer()
    {
        compui.SetActive(true);
        mainui.SetActive(false);
        this.enabled = false; // prevent re-triggering Interact()

        inputField.text = "";
        StartCoroutine(FocusInputField());
    }

    IEnumerator FocusInputField()
    {
        yield return null; // wait one frame for UI to initialize
        inputField.ActivateInputField();
        inputField.Select();
    }

    public void onOkay()
    {
        if (inputField.text.Contains("4AX7"))
        {
            DisableSecurity(0);
        }
        else if (inputField.text.Contains("2710"))
        {
            DisableSecurity(1);
        }
        else
        {
            not.Play();
            inputField.text = "";
            inputField.placeholder.GetComponent<TMP_Text>().text = "WRONG CODE, RE-ENTER";
            enemy[0].security = true;
            enemy[0].explicitDiscover = true;
            enemy[0].pos = transform.position;

            // re-focus so player can try again
            StartCoroutine(FocusInputField());
        }
    }
    void changeMessage()
    {
        promptMessage = "SuperComputer that probably controls the robot";
        
    }
    public void DisableSecurity(int i)
    {
       
        if (GLASS != null)
        {
             BLAST.Play();
            Destroy(GLASS);
        
        }
        ok.Play();
        door.toggle();
        Invoke("navBuild",5);
        enemy[i].security = false;
        promptMessage = "Security Disabled for " + delaysec + "s";
        Invoke("changeMessage",2);
        dummy.enabled=true;
        if (i == 0)
        {
            Invoke("ReactivateSecurity0", delaysec);
            
        }
        if (i == 1)
        {
            Invoke("ReactivateSecurity1", delaysec);
            
        }
        CloseComputer();
    }
    public void navBuild()
    {
        navMeshSurface.BuildNavMesh();
    }
    void ReactivateSecurity0()
    {
        enemy[0].security = true;
        promptMessage = "Super computer that probably controls the robot";
        taskActive = true;
    }
    void ReactivateSecurity1()
    {
        enemy[1].security = true;
        promptMessage = "Super computer that probably controls the robot";
        taskActive = true;
    }

    public void CloseComputer()
    {
        compui.SetActive(false);
        mainui.SetActive(true);
         player.GetComponent<PlayerInteract>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponentInChildren<PlayerLook>().enabled = true;
        this.enabled = true; // re-enable interactions
    }

    void Update()
    {
        // close computer with Escape key
        if (compui.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseComputer();
        }
    }
}   