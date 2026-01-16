using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float defSpeed = 8f;
    public float sprintSpeed = 12f;
    public float crouchSpeed = 0.5f;
    float speed;

    public float crouchTimer = 0.01f;

    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    bool lerpCrouch=false;
    Vector3 velocity;
    Vector2 moveInput;
    public Enemy enemy;
    public bool isGrounded;
    public bool sprinting;
    public bool crouching;
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

    public void Jump()
    {
        if (!isGrounded) return;
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public void SprintToggle()
    {
        sprinting = !sprinting;
        if (sprinting && !crouching)
            speed = sprintSpeed;
        else
            speed = defSpeed;
    }

    public void CrouchToggle()
    {
        crouching = !crouching;
        crouchTimer=0;
        lerpCrouch=true;
        if (crouching)
        {
            sprinting = false;
            speed = crouchSpeed;
        }
        else
        {
            speed = defSpeed;
        }
    }
    void checkSpeed()
    {
        if (sprinting)
        {
            speed=sprintSpeed;
            sound.pitch=1.4f;
        }
        else if (crouching)
        {
            speed=crouchSpeed;
            sound.pitch=0.7f;
        }
        else
        {
            speed=defSpeed;
            sound.pitch=1f;

        }
    }
    void Update()
    {
        checkSpeed();
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        Vector3 move;

        if (sprinholdactive)
        {
            // Force forward movement
            move = transform.forward;
        }
        else
        {
            move = transform.right * moveInput.x +
                transform.forward * moveInput.y;

        }
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (lerpCrouch)
        {
            crouchTimer+=Time.deltaTime;
            float p = crouchTimer/1;
            p*=p;
            if (crouching)
            {
                controller.height= Mathf.Lerp(controller.height,1,p);
            }
            else
            {
                controller.height= Mathf.Lerp(controller.height,2,p);
            }
            if ( p >1)
            {
                lerpCrouch=false;
                crouchTimer=0f;
            }
        }
        bool isMoving = moveInput.magnitude > 0.1f;

        if (isGrounded && isMoving)
        {
            if (!sound.isPlaying)
                sound.Play();
        }
        else
        {
            if (sound.isPlaying)
                sound.Stop();
        }
    }
}
