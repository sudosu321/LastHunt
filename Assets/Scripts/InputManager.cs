using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput input;
    public PlayerInput.OnFootActions onFoot;

    PlayerMovement movement;
    PlayerLook look;
    MouseLook mlook;

    void Awake()
    {
        input = new PlayerInput();
        onFoot = input.OnFoot;

        movement = GetComponent<PlayerMovement>();
        look = GetComponentInChildren<PlayerLook>();
        mlook = GetComponentInChildren<MouseLook>();


        // BUTTONS
        onFoot.jump.performed += _ => movement.Jump();
        onFoot.Sprint.performed += _ => movement.SprintToggle();
        onFoot.Crouch.performed += _ => movement.CrouchToggle();
    }
    void LateUpdate()
    {
        //mlook.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Update()
    {
        // -------------------
        // MOVEMENT
        // -------------------
        Vector2 moveInput = onFoot.movement.ReadValue<Vector2>();
        movement.SetMoveInput(moveInput);

        // -------------------
        // LOOK (right side only)
        // -------------------  
        if (look.desktop_platform)
        {
            return;
        }
            //return

        Vector2 lookDelta = Vector2.zero;

        Touchscreen ts = Touchscreen.current;
        if (ts != null)
        {
            foreach (var touch in ts.touches)
            {
                if (!touch.press.isPressed)
                    continue;

                Vector2 pos = touch.position.ReadValue();

                if (pos.x >= Screen.width * 0.5f)
                {
                    // Read delta from THIS finger
                    lookDelta = touch.delta.ReadValue(); 
                    break; // Only take first right-half finger
                }
            }
        }

        look.SetLookInput(lookDelta);//for touchescreen
        
    }
}
