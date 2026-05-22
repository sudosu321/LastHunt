using UnityEngine;

public class OpenDoor : Interactable
{
    private float openAngle = 85f;     // How far the door opens
    public float openSpeed = 2f;      // Speed of opening
    private bool opened = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    void Update()
    {
        if (opened)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                openRotation,
                Time.deltaTime * openSpeed
            );
        }
    }

    // Call this to open the door
    protected override void Interact()
    {
        if (opened)
        {
            return;
        }
        Open();
        if (player != null)
        {player.bakeit();
            
        }
        
        taskActive=false;
    }
    public void Open()
    {
        opened = true;
    }
}
