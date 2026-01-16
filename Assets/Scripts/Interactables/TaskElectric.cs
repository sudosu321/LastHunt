using UnityEngine;

public class TaskElectric : MonoBehaviour
{
    private int noswitch=3;
    public int togglesSwitch=0;
    public Light sun;
    public GameObject lightdir;
    public Material daySkybox;
    public PlasmaFill plasmaFill;

    public Transform doorTransform;
    public float moveSpeed = 5f;
    private float openHeight = 7f;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    private bool isMoving = false;
    private bool isOpen = false;
    public bool forceOpen=false;
    ChangeWire wire;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closedPosition = doorTransform.position ;
        openPosition = closedPosition + Vector3.up * openHeight;
        noswitch=3;
        wire=GetComponent<ChangeWire>();
        plasmaFill = FindAnyObjectByType<PlasmaFill>();
    }
    public void incrementTask()
    {

        togglesSwitch=togglesSwitch+1;
        if (togglesSwitch >= noswitch)
        {
            Debug.Log("Lights on !!");
            RenderSettings.skybox = daySkybox;
            DynamicGI.UpdateEnvironment();
            lightdir.SetActive(true);
            sun.intensity = 0.4f;
            sun.color = Color.white;
            wire.colorAllWires();
            GetComponent<PlayerHold>().electricTaskComplete=true;
            GetComponent<ComputerMain>().computerStart=true;
            openStageDoor();
        }
    }
    void openStageDoor()
    {   
        GetComponent<PlayerHold>().bakeit();
        isMoving=true;
    }
    // Update is called once per frame
    void Update()
    {
        if(isOpen)return;
        if (!isMoving) return;

        Vector3 target = openPosition;

        doorTransform.position = Vector3.MoveTowards(
            doorTransform.position,
            target,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(doorTransform.position, target) < 0.01f)
        {
            doorTransform.position = target;
            isMoving = false;
            isOpen = true;;
        }
    }
}
