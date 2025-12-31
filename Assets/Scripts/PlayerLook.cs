using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Camera cam;
    public float xRotation=0f;
    public float xSensi=30f;
    public float ySensi=30f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        xRotation-=(mouseY  *Time.deltaTime) *ySensi;
        xRotation=Mathf.Clamp(xRotation,-80f,80f);
        cam.transform.localRotation= Quaternion.Euler(xRotation,0,0);
        transform.Rotate(Vector3.up * (mouseX*Time.deltaTime)*xSensi);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
