using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public byte defSpeed = 8;
    public byte sprintSpeed = 12;
    byte speed;
    Vector3 velocity;
    Vector2 moveInput;
    public bool sprinting;
    public AudioSource sound;
    public bool sprinholdactive=false;
    public bool isMoving=false;
    public int maxSprintTime=10;
    public float maxsprintStamina=100f;
    public float currentStamina = 100f;
    public float dranRate=10f;
    public float regenRate=5f;
    public float lessDark;
    public float moreDark;
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
         if (GameSettings.Instance != null)
        {
            int val = GameSettings.Instance.difficulty;

            if (val == 0)
            {
                sprintSpeed=30;
                defSpeed=20;
                RenderSettings.fogDensity =lessDark; 
                DynamicGI.UpdateEnvironment();
            }
            else if (val == 1)
            {
                RenderSettings.fogDensity =lessDark; 
                DynamicGI.UpdateEnvironment();
            }
            else if(val == 2)
            {   
                RenderSettings.fogDensity = moreDark; 
                DynamicGI.UpdateEnvironment();
            }
        }
        if (GameSettings.Instance != null)AudioListener.volume = GameSettings.Instance.masterVolume;
        speed = defSpeed;
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
    bool drained=false;
    public float threshold=20f;
    void drain()
    {
        currentStamina-=dranRate*Time.deltaTime;
        if (currentStamina < 0)
        {
            currentStamina=0;
        }
        if(currentStamina<threshold)drained=true;
    }
    void regen()
    {
        currentStamina+=regenRate*Time.deltaTime;
        if (currentStamina >maxsprintStamina)
        {
            currentStamina=maxsprintStamina;
        }
        if(currentStamina>threshold)drained=false;
    }
    void Update()
    {
        Vector3 move;
        if (sprinholdactive || sprinting)
        {
            if (currentStamina < threshold)
            {
                sound.pitch=(1);
                speed = defSpeed;
                move = transform.forward;
            }
            else
            {
                sound.pitch=(1.5f);
                speed = sprintSpeed;
                drain();
                move = transform.forward;
            }
            
        }
        else if(!sprinholdactive && !sprinting)
        {
            sound.pitch=(1);
            speed = defSpeed;
            regen();
            move = transform.right * moveInput.x +transform.forward * moveInput.y;
        }
        else
        {
            sound.pitch=(1);
            speed = defSpeed;
            regen();
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
