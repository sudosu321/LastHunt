using UnityEngine;
using System.Collections;
using Unity.AI.Navigation;

public class doorM : Interactable
{
    public Transform doorTransform;
    public float moveSpeed = 5f;
    public float openHeight = 8f;       
    public float autoCloseDelay = 1f;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    private bool isMoving = false;
    private bool isOpen = false;
    private Coroutine autoCloseRoutine;

    void Start()
    {
        promptMessage = "Mechanical door button";
        closedPosition = doorTransform.position;
        openPosition = closedPosition + Vector3.up * openHeight;
    }

    void Update()
    {
        if (!isMoving) return;

        Vector3 target = isOpen ? closedPosition : openPosition;

        doorTransform.position = Vector3.MoveTowards(
            doorTransform.position,
            target,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(doorTransform.position, target) < 0.01f)
        {
            doorTransform.position = target;
            isMoving = false;
                player.nav.BuildNavMesh();
            isOpen = !isOpen;

            // Start auto close AFTER opening
            if (isOpen)
            {
                if (autoCloseRoutine != null)
                    StopCoroutine(autoCloseRoutine);

                autoCloseRoutine = StartCoroutine(AutoCloseDoor());
            }
        }
    }

    protected override void Interact()
    {
        if (isMoving) return;

        // Cancel pending auto-close if player interacts again
        if (autoCloseRoutine != null)
        {
            StopCoroutine(autoCloseRoutine);
            autoCloseRoutine = null;
        }

        isMoving = true;
        promptMessage = "Mechanical door button";
    }

    IEnumerator AutoCloseDoor()
    {
        yield return new WaitForSeconds(autoCloseDelay);

        if (!isMoving && isOpen)
        {
            isMoving = true;
        }
    }
}
