using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    private PlayerMotor playerMotor ;
    private PlayerLook playerLook;
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        playerLook = GetComponent<PlayerLook>();
        playerMotor =   GetComponent<PlayerMotor>();
        onFoot.jump.performed+=ctx=>playerMotor.jump();

        onFoot.Crouch.performed += ctx=>playerMotor.Crouch();
        onFoot.Sprint.performed += ctx=>playerMotor.Sprint();

    }
    void LateUpdate()
    {
        playerLook.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    void FixedUpdate()
    {
        playerMotor.ProccesMove(onFoot.movement.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }

}
