using System.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;

public class RedPlugElecPanel : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject placeholder;
    public GameObject plug;
    public GameObject wire;
    public GameObject display;
    public GameObject enemy;
    private bool panelFixed=false;
    public Material greenMat;
    public NavMeshSurface navMeshSurface;
    public Transform door;
    public Transform doorTransform;
    public float moveSpeed = 1f;
    public float openHeight = 7f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    public AudioSource pandaWakes;
    private bool isMoving = false;
    private bool isOpen = false;
    void Start()
    {
        taskActive=true;
        promptMessage="electric panel";
        closedPosition = doorTransform.position ;
        openPosition = closedPosition + Vector3.up * openHeight;
    }
    void bakke()
    {
        navMeshSurface.BuildNavMesh();
    }
    void movedoor()
    {
        isMoving=true;
        
    }
    void doAfterPanelOn()
    {
        Invoke("movedoor",1);
        Invoke("bakke",8);
        navMeshSurface.BuildNavMesh();
        wire.GetComponent<Renderer>().material=greenMat;
        Invoke("spawn",4);
    }   
    void spawn()
    {
        pandaWakes.Play();
        Destroy(display);
        enemy.SetActive(true);
        Animator anim = enemy.GetComponentInChildren<Animator>();

        anim.Rebind();     // resets animator
        anim.Update(0f);   // forces evaluation

        anim.SetBool("isWalking", true);
        anim.SetBool("isRunning", false);
    }
    protected override void Interact()
    {
        if (panelFixed)
        {
            promptMessage="Switch on";
            taskActive=false;
            doAfterPanelOn();
            return;
        }
        if (player.isPlayerHasItem)
        {
            if (player.gunPick.gunHeld)
            {
                promptMessage="there's a missing plug";
                return;
            }
            if (player.itemT.name.Contains("missing"))
            {        
                promptMessage="panel fixed";
                placeholder.SetActive(true);
                player.drop();
                panelFixed=true;
                Destroy(plug);
                
                return;
            }
            else
            {
                promptMessage="there's a missing plug";
                return;
            }
        }
        else
        {
            promptMessage="there's a missing plug";
            return;
        }
        
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
