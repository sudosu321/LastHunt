using System.Collections;
using UnityEngine;

public class Drawer : Interactable
{
    private float openDistance = 0.3f;    // how far it slides out on X
    public float slideSpeed = 1f;

    private Vector3 closedPos;
    private Vector3 openPos;
    private bool isOpen = false;
    private bool isMoving = false;

    void Start()
    {
        closedPos = transform.localPosition;
        openPos = closedPos + new Vector3(0f, 0f, openDistance);
    }

    protected override void Interact()
    {
        if (isMoving) return;
        StartCoroutine(SlideDrawer(isOpen ? closedPos : openPos));
        isOpen = !isOpen;
        promptMessage = isOpen ? "Close Drawer" : "Open Drawer";
    }

    IEnumerator SlideDrawer(Vector3 target)
    {
        isMoving = true;
        while (Vector3.Distance(transform.localPosition, target) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * slideSpeed);
            yield return null;
        }
        transform.localPosition = target;
        isMoving = false;
    }
}