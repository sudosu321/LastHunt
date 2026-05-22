using UnityEngine;

public class CameraBob : MonoBehaviour
{
    [Header("Bobbing Settings")]
    public float bobSpeed = 8f;
    public float bobAmount = 0.05f;
    public float normBob = 8f;
    public float sprintBob = 8f;

    public float normAmount = 0.05f;
    public float sprintAmount = 0.05f;
    public  PlayerMovement player;
    private float defaultY;
    private float timer = 0f;

    void Start()
    {
        defaultY = transform.localPosition.y;
    }

    void Update()
    {
        bobSpeed=player.sprinholdactive?sprintBob:normBob;
        bobAmount=player.sprinholdactive?normAmount:sprintAmount;

        if (player.isMoving)
        {
            timer += Time.deltaTime * bobSpeed;
            float newY = defaultY + Mathf.Sin(timer) * bobAmount;
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                newY,
                transform.localPosition.z
            );
        }
        else
        {
            // Reset smoothly
            timer = 0f;
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                new Vector3(transform.localPosition.x, defaultY, transform.localPosition.z),
                Time.deltaTime * 5f
            );
        }
    }
}
