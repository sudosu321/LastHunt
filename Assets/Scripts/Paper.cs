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
        "The main electric supply is done by plasma reactors , the plasma cubes needs to be filled with time to keep the reactors working , failure of any one would result in power sortage.Without power the electric panels wont get supply and electric panels will not work ~~sam ",
        "The servers in the server room needs to be repaired .Servers are responsible for the security system and research systems to work , however we had a virus attack last week .Need to fix by tommorrow , cutting the supply to the infected server would do the trick ~~Sam "
        ,"Security System control [confidential] , the security robot we've been working has malfunctioned, it sees every entity as threat ,it killed 12 humans till now, only way to survive it is to drain it of power so we are abandoning the facility .It can also be turned off by the serial key written on it through the computer . But its impossible to get close to that f*cking machine  ~~sam "
        ,"Main door system , for securtiy reasons the door is locked and can only be unlocked if the building's secuirty is off ,it can be turned off by entering the security code into the computer in the lab , the security code must be the serial code in the robot"
        ,"I am tired , i am tired of tring to escape this prison , when it went out of control they just locked me with this thing and ran away , i just dont know what to do anymore        ~sam"
        ,"the panda robot is a security machine that malfunctioned during an experiment ,the building was abandoned so it could finally be drained off its power , it has weak eyes but hears everything, sprinting would make you even more visible , be carefull when you use a gun , its coming for you !"
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