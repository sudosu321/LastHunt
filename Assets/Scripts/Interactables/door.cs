using UnityEngine;

public class door : Interactable
{
    public Transform doorTransform;
    public float moveSpeed = 5f;
    public float openHeight = 3f;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    private bool isMoving = false;
    private bool isOpen = false;
    public bool forceOpen=false;
    void Start()
    {
        promptMessage = "door button";
        closedPosition = doorTransform.position ;
        openPosition = closedPosition + Vector3.up * openHeight;
    }

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

    protected override void Interact()
    {
        if (!forceOpen)
        {
            if (isMoving) return;
            if (!player.A1_FEUL_TASK)
            {
                promptMessage="electricity not available";
                return;
            }
            promptMessage="";
            isMoving = true;
            taskActive=false;
            player.bakeit();

            return;
        }
        player.bakeit();
        promptMessage="";
            isMoving = true;
            taskActive=false;
            return;
    }
}
