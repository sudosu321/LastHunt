using TMPro;
using UnityEngine;

public class Paper : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    public GameObject controlUI;
    public GameObject noteUI;
    public TextMeshProUGUI text;
    public int code;
    string[] hints =new string[]
    {
        "The main electric supply is done by plasma reactors , the plasma cubes needs to be filled with time to keep the reactors working , failure of any one would result in power sortage.Without power the electric panels wont get supply and electric panels will not work ~~john(a worker from past)",
        "The servers in the server room needs to be repaired .Servers are responsible for the security system and research systems to work , however we had a virus attack last week .Need to fix by tommorrow , cutting the supply to the infected server would do the trick ~~john "
        ,"Security System control [confidential] , the security robot we've been working has malfunctioned, it sees every entity as threat ,it killed 12 humans till now, only way to survive it is to drain it of power so we are abandoning the facility .It can also be turned off by the serial key written on it through the computer . But its impossible to get close to that f*cking machine  - john "
        ,"Main door system , for security reasons the door is locked and can only be unlocked if the building's secuirty is off ,it can be turned off by entering the security code into the computer in the lab , the security code must be the serial code printed on the robot"
        ,"for anyone who reads my note , you need to escape , you can hide in the sealed chambers ,one of them is here but hiding wont work , run...  ~john"
        ,"Abondoned Lab\nThis place was abandoned after a security robot went out of control and started killing everyone ,its been years and it lost its power and is sleep , dont wake it up"
        ,"you found a exit huh ? dont you ever think you will be free , it remembers your face"
    };
    void Start()
    {
        
    }
    protected override void Interact()
    {
        gameObject.SetActive(false);
        gameObject.transform.SetParent(player.transform);
        gameObject.transform.SetPositionAndRotation(player.playerHand.transform.position,player.playerHand.transform.rotation);
        player.paperObject=gameObject;
        controlUI.SetActive(false);
        noteUI.SetActive(true);
        text.SetText(hints[code]);
        player.isNoteOpened=true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}