using UnityEngine;

public class GunLock : MonoBehaviour
{  
    public Transform cameraTransform;
    public float followSpeed = 20f;

    void LateUpdate()
    {
        // Match rotation exactly
        Quaternion targetRotation = cameraTransform.rotation;

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            followSpeed * Time.deltaTime
        );
    }
}