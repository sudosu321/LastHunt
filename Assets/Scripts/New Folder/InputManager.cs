using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInteract player;
    PlayerInput input;
    public PlayerInput.OnFootActions onFoot;
    PlayerMovement movement;
    PlayerLook look;
    PlayerHold hold;
    MouseLook mlook;
    PausePlay pp;
    public GunShoot gun;

    void Awake()
    {
        input = new PlayerInput();
        onFoot = input.OnFoot;
        player=GetComponent<PlayerInteract>();
        movement = GetComponent<PlayerMovement>();
        look = GetComponentInChildren<PlayerLook>();
        mlook = GetComponentInChildren<MouseLook>();
        hold=GetComponent<PlayerHold>();
        pp=GetComponent<PausePlay>();

        // BUTTONS
        onFoot.Sprint.performed += _ => movement.SprintToggle();
        onFoot.Interact.performed += _ => player.Use();
        onFoot.escape.performed += _ => pp.handle();
        onFoot.fire.performed += _ => gun.OnFire();



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
