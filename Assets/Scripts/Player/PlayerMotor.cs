using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created\
    private CharacterController controller ;
    private Vector3 playerVelocity;
    public float defSpeed=8f;
    public float sprintSpeed = 14f;
    public float crouchSpeed = 3f;

    public float speed = 8f;
    public bool isGrounded=false;
    public bool lerpCrouch=false;
    public bool sprinting=false;
    public bool crouching=false;

    public float gravity = -9.8f;
    public float jumpHeight = 1f;
    public float crouchTimer = 0.2f;
    private Vector2 moveInput;
    void Start()
    {
        controller=GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 move =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;

        controller.Move(move * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        isGrounded=controller.isGrounded;
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
        if (crouching)
        {
            speed=crouchSpeed;
        }
        
        else if ( !crouching && !sprinting)
        {
            speed=defSpeed;
        }
    }
    public void Crouch()
    {
        crouching= !crouching;
        crouchTimer=0;
        lerpCrouch=true;
    }
    public void Sprint()
    {
        sprinting = ! sprinting;
        if (sprinting)
            speed= sprintSpeed ;
        else if(!crouching && !sprinting)
            speed = defSpeed;
    }
    public void ProccesMove(Vector2 input)
    {
        Vector3 moveDirection=Vector3.zero;
        moveDirection.x=input.x;
        moveDirection.z=input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y+=gravity*Time.deltaTime;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y=-2f;
        }
        controller.Move(playerVelocity*Time.deltaTime);
    }
    public void jump()
    {
        if (isGrounded)
        {
            playerVelocity.y=Mathf.Sqrt(jumpHeight*-1.2f*gravity );
        }
    }
}
