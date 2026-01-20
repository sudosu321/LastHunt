using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public byte defSpeed = 8;
    public byte sprintSpeed = 12;
    byte speed;
    Vector3 velocity;
    Vector2 moveInput;
    public Enemy enemy;
    public bool sprinting;
    public AudioSource sound;
    public bool sprinholdactive=false;
    void Start()
    {
        if (GameSettings.Instance != null)AudioListener.volume = GameSettings.Instance.masterVolume;
        speed = defSpeed;
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    public void SprintToggle()
    {
        sprinting = !sprinting;
        if (sprinting )
            speed = sprintSpeed;
        else
            speed = defSpeed;
    }
    public float gravity = -20f;
    float verticalVelocity;

    void Update()
    {
        Vector3 move;
        if (sprinholdactive)
        {
            move = transform.forward;
        }
        else
        {
            move = transform.right * moveInput.x +transform.forward * moveInput.y;

        }
        move = move.normalized * speed;

        // ✅ Apply gravity
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0)
                verticalVelocity = -2f; // keeps player grounded on slopes
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;
        controller.Move(move  * Time.deltaTime);
        if (controller.velocity.sqrMagnitude > 5)
        {
            if(sound.isPlaying==false)sound.Play();
        }
        else
        {

            if(sound.isPlaying==true)sound.Stop();   
        }
    }
}
