using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public PlayerLook lookController;
    public float mouseSensitivity = 1f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!lookController.desktop_platform)//android platform
        {
            return;
        }
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        Vector2 mouseDelta = new Vector2(mouseX, mouseY);

        if (mouseDelta.sqrMagnitude > 0f)
        {
            lookController.SetLookInput(mouseDelta);//for pc mouse only
        }
    }
}
