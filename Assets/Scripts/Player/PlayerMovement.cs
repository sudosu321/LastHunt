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
    public bool isMoving=false;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;

        if (GameSettings.Instance != null)AudioListener.volume = GameSettings.Instance.masterVolume;
        speed = defSpeed;
        if (GameSettings.Instance != null)
            {
                bool sett= GameSettings.Instance.debugMode;
                if (sett)
                {
                    sprintSpeed=30;
                    defSpeed=20;
                }
            }
            sound.pitch=(1f);
        
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    public void SprintToggle()
    {
        sprinting = !sprinting;
        if (sprinting)
        {
            speed = sprintSpeed;
            sound.pitch=(1.5f);
        }

        else
        {
            speed = defSpeed;
            sound.pitch=(1);
            
        }
    }
    public float gravity = -20f;
    float verticalVelocity;

    void Update()
    {
        Vector3 move;
        if (sprinholdactive || sprinting)
        {
            sound.pitch=(1.5f);
            speed = sprintSpeed;
            move = transform.forward;
        }
        else if(!sprinholdactive && !sprinting)
        {
            sound.pitch=(1);
            speed = defSpeed;
            move = transform.right * moveInput.x +transform.forward * moveInput.y;
        }
        else
        {
            sound.pitch=(1);
            speed = defSpeed;
            move = transform.right * moveInput.x +transform.forward * moveInput.y;
        }
        move = move.normalized * speed;
        isMoving=move.sqrMagnitude>0.01f;
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0)
                verticalVelocity = -2f; 
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
