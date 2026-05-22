using UnityEngine;

public class DOOR_OPENER : MonoBehaviour
{
    public Transform doorTransform;
    public float moveSpeed = 5f;
    public float openHeight = 3f;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    private bool isMoving = false;
    private bool isOpen = false;
    public bool forceOpen=false;
    public AudioSource doorAudio;
    void Start()
    {
        if (doorTransform.GetComponent<AudioSource>() != null)
        {
            doorAudio=doorTransform.GetComponent<AudioSource>();
        }
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

    public void toggle()
    {
        if(isOpen)return;
        doorTransform.GetComponent<AudioSource>().Play();
            isMoving = true;
            return;
    }
}
