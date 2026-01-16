using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    public Transform playerBody;
    public float sensitivity ;
    public float maxLookAngle = 80f;
    public bool desktop_platform;
    float xRotation;
    Vector2 lookInput;
    void Start()
    {
        if (GameSettings.Instance != null)sensitivity= GameSettings.Instance.mouseSensitivity/10;
        if (Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.OSXPlayer ||
            Application.platform == RuntimePlatform.LinuxPlayer)
        {
            //Debug.Log("Desktop platform");
            desktop_platform=true;
        }
        else
        {
            //Debug.Log("Android platform");

            desktop_platform=false;
        }
    }
    public void SetLookInput(Vector2 input)
    {
        lookInput = input;
       // Debug.Log(input.x);
    }

    void Update()
    {
        //for touchscreen only
        if (lookInput == Vector2.zero) return;

        float yaw = lookInput.x * sensitivity;
        float pitch = lookInput.y * sensitivity;

        xRotation -= pitch;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * yaw);

        lookInput = Vector2.zero;
    }
}
